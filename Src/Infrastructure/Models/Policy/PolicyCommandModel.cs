using Framework.Contracts.Response;
using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Contracts.Commands.Policy;
using Pumpkin.Domain.Framework.Events;
using Pumpkin.Domain.Framework.Models;
using Pumpkin.Domain.Framework.Repositories;
using Pumpkin.Domain.Framework.Serialization;
using Pumpkin.Domain.Models.Policy;
using Pumpkin.Domain.Repositories.Policy;

namespace Pumpkin.Infrastructure.Models.Policy;

public class PolicyCommandModel : CommandModelBase, IPolicyCommandModel
{
    private readonly IPolicyCommandRepository _policyCommandRepository;

    public PolicyCommandModel(ICommandRepositoryBase repository, IMessagePublisher publisher, IHttpContextAccessor accessor, ISerializer serializer,
        IPolicyCommandRepository policyCommandRepository) : base(repository, publisher, accessor, serializer)
    {
        _policyCommandRepository = policyCommandRepository;
    }

    public async Task<EmptyResponse> PolicyRegisterAsync(PolicyRegisterCommand command, CancellationToken cancellationToken)
    {
       // await _policyCommandRepository.RegisterAsync(command, cancellationToken);

        return EmptyResponse.Instance();
    }
}