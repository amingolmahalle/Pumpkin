using System.Runtime.Serialization;

namespace Pumpkin.Web.ResponseWrapper
{
    [DataContract]
    public class ApiResponse
    {
        [DataMember] public int StatusCode { get; set; }

        [DataMember] public string Message { get; set; }

        [DataMember(EmitDefaultValue = false)] public ApiError ResponseException { get; set; }

        [DataMember(EmitDefaultValue = false)] public object Result { get; set; }

        public ApiResponse(int statusCode, string message = "")
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ApiResponse(
            int statusCode,
            string message = "",
            object result = null,
            ApiError apiError = null) : this(statusCode, message)
        {
            Result = result;
            ResponseException = apiError;
        }
    }
}