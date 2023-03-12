using Framework.Contracts.Response;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Domain.Framework.Services.Requests;

namespace Pumpkin.Domain.Contracts.Commands.Profile;

public class CreateOrGrabCustomerCommand : CommandBase, IApplicationCommand<EmptyResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MobileNumber { get; set; }
    public string NationalCode { get; set; }
    public string Address { get; set; }
    public bool? Gender { get; set; }
}