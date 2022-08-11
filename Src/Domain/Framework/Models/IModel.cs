using Pumpkin.Domain.Framework.Logging;

namespace Pumpkin.Domain.Framework.Models;

public interface IModel
{
    ILog Logger { get; }
}