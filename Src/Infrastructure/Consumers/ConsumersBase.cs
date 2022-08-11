using Pumpkin.Domain.Consumers;
using Pumpkin.Domain.Framework.Events;

namespace Pumpkin.Infrastructure.Consumers;

public abstract class ConsumersBase : EventConsumer
{
    public string Route { get; } = Globals.Events.Routes.WeatherRoute;
}