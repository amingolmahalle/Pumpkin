using Domain.Framework.Logging;

namespace Domain.Framework.Models;

public interface IModel
{
    ILog Logger { get; }
}