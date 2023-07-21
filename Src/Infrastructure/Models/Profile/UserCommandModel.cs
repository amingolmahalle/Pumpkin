using Framework.Contracts.Response;
using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Contracts.Commands.Profile;
using Pumpkin.Domain.Entities.Profile;
using Pumpkin.Domain.Events.DomainEvents.Profile;
using Pumpkin.Domain.Framework.Events;
using Pumpkin.Domain.Framework.Models;
using Pumpkin.Domain.Models.Profile;
using Pumpkin.Domain.Repositories.Profile;

namespace Pumpkin.Infrastructure.Models.Profile;

public class UserCommandModel : CommandModelBase, IUserCommandModel
{
    private readonly IUserCommandRepository _userCommandRepository;

    public UserCommandModel(
        IMessagePublisher publisher,
        IHttpContextAccessor accessor,
        IUserCommandRepository userCommandRepository) : base(userCommandRepository, publisher, accessor)
    {
        _userCommandRepository = userCommandRepository;
    }

    public async Task<DataResponse<Guid>> CreateOrGrabCustomerAsync(CreateOrGrabCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await _userCommandRepository.FindCustomerAsync(c => c.MobileNumber == command.MobileNumber, cancellationToken);
        var isNew = customer == null;

        customer ??= new User().Create(
            command.FirstName,
            command.LastName,
            command.MobileNumber,
            command.NationalCode,
            command.Address,
            command.Gender);

        customer.Apply(new NewCustomerCreated
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            MobileNumber = command.MobileNumber,
            NationalCode = command.NationalCode,
            Address = command.Address,
            Gender = command.Gender,
            Payload = new Dictionary<string, object>
            {
                { "CustomerId", customer.Id.Value },
            }
        });

        if (isNew)
            await _userCommandRepository.AddAsync(customer, cancellationToken);
        else
            _userCommandRepository.Modify(customer);

        await HandleTransactionAsync(command, customer, cancellationToken);

        return DataResponse<Guid>.Instance(customer.Id);
    }
}