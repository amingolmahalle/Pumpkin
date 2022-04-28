using System.Collections;

namespace Pumpkin.Contract.Security;

public interface ICurrentRequest
{
    string CorrelationId { get; set; }

    GatewayType Gateway { get; set; }

    string UserSessionId { get; set; }

    int UserId { get; set; }

    string UserName { get; set; }

    Hashtable Headers { get; }

    AuthenticationType AuthenticationType { get; set; }

    string GetHeader(string key);

    bool HasHeader(string key);

    T GetEnumHeader<T>(string key) where T : struct;

    double? GetDoubleHeader(string key);
}