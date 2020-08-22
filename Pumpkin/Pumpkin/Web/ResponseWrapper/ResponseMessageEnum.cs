using System.ComponentModel;

namespace Pumpkin.Web.ResponseWrapper
{
    public enum ResponseMessageEnum
    {
        [Description("Request successful.")] Success = 0,

        [Description("Request not found. The specified uri does not exist.")]
        NotFound = 1,

        [Description("Request responded with 'Method Not Allowed'.")]
        MethodNotAllowed = 2,

        [Description("Request no content. The specified uri does not contain any content.")]
        NotContent = 3,

        [Description("Request responded with exceptions.")]
        Exception = 4,

        [Description("Request denied. Unauthorized access.")]
        UnAuthorized = 5,

        [Description(
            "Request responded with validation error(s). Please correct the specified validation errors and try again.")]
        ValidationError = 6,

        [Description("Request cannot be processed. Please contact a support.")]
        Unknown = 7,

        [Description("Request responded with validation error(s). Please contact a support.")]
        BadRequest = 8,

        [Description("Unhandled Exception occured. Unable to process the request.")]
        Unhandled = 9,

        [Description("Unable to process the request.")]
        Failure = 10,

        [Description("An error occurred on the server.")]
        ServerError = 11,
    }
}