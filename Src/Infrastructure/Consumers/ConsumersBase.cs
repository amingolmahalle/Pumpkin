using Domain.Framework.Events;
using Pumpkin.Domain.Consumers;

namespace Pumpkin.Infrastructure.Consumers;

public abstract class ConsumersBase : EventConsumer
{
    public string Route { get; } = Globals.Events.Routes.WeatherRoute;
}