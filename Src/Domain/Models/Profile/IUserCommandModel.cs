using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Commands.Profile;
using Pumpkin.Domain.Framework.Models;

namespace Pumpkin.Domain.Models.Profile;

public interface IUserCommandModel : ICommandModel
{
    Task<DataResponse<Guid>> CreateOrGrabCustomerAsync(CreateOrGrabCustomerCommand command, CancellationToken cancellationToken);
}