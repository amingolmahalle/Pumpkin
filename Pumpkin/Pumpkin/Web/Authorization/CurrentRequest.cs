using System;
using System.Collections;
using Pumpkin.Contract.Security;

namespace Pumpkin.Web.Authorization
{
    public class CurrentRequest : ICurrentRequest
    {
        public CurrentRequest()
        {
            Headers = new Hashtable();
        }

        public string CorrelationId { get; set; }

        public GatewayType Gateway { get; set; }

        public string UserSessionId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public Hashtable Headers { get; set; }

        public AuthenticationType AuthenticationType { get; set; }

        public string Groups { get; set; }

        // public void From(ICurrentRequest request)
        // {
        //     if (request == null)
        //         return;
        //
        //     CorrelationId = request.CorrelationId;
        //     Gateway = request.Gateway;
        //     UserSessionId = request.UserSessionId;
        //     UserId = request.UserId;
        //     UserName = request.UserName;
        //     Headers = (Hashtable) request.Headers.Clone();
        //     AuthenticationType = request.AuthenticationType;
        // }

        public string GetHeader(string key)
        {
            return Headers.ContainsKey(key) ? Headers[key].ToString() : string.Empty;
        }

        public bool HasHeader(string key)
        {
            return Headers.ContainsKey(key);
        }

        public T GetEnumHeader<T>(string key) where T : struct
        {
            var val = GetHeader(key);
            if (string.IsNullOrEmpty(val))
                return default(T);

            Enum.TryParse(val, out T result);

            return result;
        }

        public double? GetDoubleHeader(string key)
        {
            var val = GetHeader(key);

            if (string.IsNullOrEmpty(val))
                return default;

            double.TryParse(val, out var result);

            return result;
        }
    }
}