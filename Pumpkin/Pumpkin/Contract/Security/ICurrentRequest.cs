using System.Collections;

namespace Pumpkin.Contract.Security
{
    public interface ICurrentRequest
    {
        string CorrelationId { get; set; }

        GatewayType Gateway { get; set; }

        string UserSessionId { get; set; }

        string UserId { get; set; }

        string UserName { get; set; }

        // KeyValueObject[] UserGroups { get; }

        Hashtable Headers { get; }

        AuthenticationType AuthenticationType { get; set; }

        // List<string> GetClaimValues(string claimType);

        // bool HasAccess(string resourceName);

        // void From(ICurrentRequest request);
    }
}