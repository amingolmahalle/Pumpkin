using System.Text;
using System.Timers;
using Domain.Framework.Logging;
using Domain.Framework.MessageHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Framework.MessageHandlers.RabbitMq;

public class RabbitMqBusHandler : BusHandler
{
    private readonly System.Timers.Timer _timer;
    private readonly IConnection _connection;
    private readonly IMessagePublisher _publisher;
    public ILog Logger { get; }
    private readonly List<KeyValuePair<DateTime, BusEvent>> _queue;

    public RabbitMqBusHandler(IConfiguration configuration, IMessagePublisher publisher, IHttpContextAccessor accessor)
    {
        _publisher = publisher;
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMq:HostName"],
            Port = int.Parse(configuration["RabbitMq:Port"]),
            UserName = configuration["RabbitMq:UserName"],
            Password = configuration["RabbitMq:Password"],
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();

        Logger = LogManager.GetLogger<RabbitMqBusHandler>();

        _queue = new List<KeyValuePair<DateTime, BusEvent>>();
        _timer = new(1000);
        _timer.Elapsed += TimerOnElapsed;
        _timer.Start();
    }

    private void TimerOnElapsed(object sender, ElapsedEventArgs e)
    {
        _timer.Stop();
        var events = _queue.Where(x => x.Key <= DateTime.UtcNow).ToList();
        if (!events.Any())
        {
            _timer.Start();
            return;
        }

        foreach (var @event in events)
        {
            _publisher.Publish(@event.Value);
            _queue.Remove(@event);
        }

        _timer.Start();
    }

    public override void StartListening()
    {
        foreach (var consumer in Consumers.GroupBy(x => x.BusName))
            StartListening(consumer.First().Route, consumer.Key);
    }

    private void StartListening(string route, string exchangeName)
    {
        var busName = route.ToUpper() + "__" + exchangeName;

        Logger.Info($"Setting up QUEUE for Listener. BusName={busName},event-management =rabbitmq-handling");

        var channel = _connection.CreateModel();
        if (channel == null)
            throw new Exception("NULL CHANNEL");

        channel.QueueDeclare(busName, true, false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(channel);
        byte[] body;
        string message;
        BusEvent @event;
        consumer.Received += async (__, args) =>
        {
            Logger.Info($"New message received. Exchange= {args.Exchange}, ConsumerTag= {args.ConsumerTag}, RoutingKey= {args.RoutingKey}, event-management= rabbitmq-messages");

            var ch = __ as AsyncEventingBasicConsumer;
            if (ch == null)
                throw new Exception("NULL CHANNEL");
            body = args?.Body.ToArray() ?? throw new ArgumentNullException("args.Body.ToArray()");
            if (body == null)
                throw new Exception("NULL BODY");

            message = Encoding.UTF8.GetString(body);
            @event = JsonConvert.DeserializeObject<BusEvent>(message);
            if (@event == null)
                throw new Exception("NULL EVENT");
            var eventConsumers = Consumers.Where(x => x.BusName == args.Exchange && x.EventType == @event.EventType)
                .ToList();

            try
            {
                if (eventConsumers.Count == 0)
                {
                    Logger.Info(
                        $"New message has no consumer. Exchange= {args.Exchange}, ConsumerTag= {args.ConsumerTag}, RoutingKey= {args.RoutingKey}, event-management= rabbitmq-messages");

                    ch.Model.BasicAck(args.DeliveryTag, false);
                    await Task.Yield();
                    return;
                }

                var failedConsumers = new List<IEventConsumer>();
                foreach (var eventConsumer in eventConsumers)
                {
                    if (@event.Retry > 1 && @event.RetryType != eventConsumer.GetType().ToString())
                        continue;

                    try
                    {
                        Logger.Info(
                            $"Passing message to consumer. Exchange= {args.Exchange}, ConsumerTag= {args.ConsumerTag}, RoutingKey= {args.RoutingKey}, MessageBody= {message}, MessageRetry= {@event.Retry.ToString()}, EventConsumer= {eventConsumer.GetType().ToString()} event-management= rabbitmq-messages");

                        if (!await eventConsumer.Handle(@event.Retry, @event.Payload))
                            throw new Exception("Not Done");
                    }
                    catch (Exception exception)
                    {
                        failedConsumers.Add(eventConsumer);

                        Logger.Error(
                            $"Consumer failed to run. Exchange= {args.Exchange}, ConsumerTag= {args.ConsumerTag}, RoutingKey= {args.RoutingKey}, MessageBody= {message}, MessageRetry= {@event.Retry.ToString()}, EventConsumer= {eventConsumer.GetType().ToString()} event-management= rabbitmq-messages");
                    }
                }

                ch.Model.BasicAck(args.DeliveryTag, false);
                if (failedConsumers.Count > 0)
                {
                    @event.Retry++;
                    foreach (var failedConsumer in failedConsumers)
                    {
                        if (@event.Retry > failedConsumer.MaxRetry || failedConsumer.RetryWait == TimeSpan.Zero)
                            continue;

                        @event.RetryType = failedConsumer.GetType().ToString();
                        _queue.Add(new KeyValuePair<DateTime, BusEvent>(DateTime.UtcNow
                            .Add(failedConsumer.RetryWait * @event.Retry), @event));
                    }
                }
            }
            catch (Exception exception)
            {
                Logger.Error(
                    $"Message passing failed. Exchange= {args.Exchange}, ConsumerTag= {args.ConsumerTag}, RoutingKey= {args.RoutingKey}, MessageBody= {message}, event-management= rabbitmq-messages");
            }
        };

        channel.BasicConsume(queue: busName, autoAck: false, consumer: consumer);
    }
}