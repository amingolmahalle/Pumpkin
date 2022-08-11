using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Helpers;

namespace Pumpkin.Domain.Framework.Events;

public abstract class BusHandler
{
    protected List<IEventConsumer> Consumers { get; }

    protected BusHandler()
    {
        Consumers = new List<IEventConsumer>();
    }

    public void ExtractConsumers(ServiceProvider provider, string nameSpace)
    {
        var typesToRegister = AssemblyScanner.AllTypes(nameSpace, "(Domain\\.Consumers){1}")
            .Where(it => it.IsInterface && typeof(IEventConsumer).IsAssignableFrom(it));

        foreach (var item in typesToRegister)
        {
            var consumer = (IEventConsumer) provider.GetService(item);
            if (!consumer.Enabled)
                continue;

            if (Consumers.Contains(consumer))
                continue;

            Consumers.Add(consumer);
        }
    }

    public abstract void StartListening();
}