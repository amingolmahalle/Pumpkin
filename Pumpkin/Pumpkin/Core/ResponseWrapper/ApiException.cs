using System.Collections.Generic;

namespace Pumpkin.Core.ResponseWrapper
{
    public class ApiException : System.Exception
    {
       // public int StatusCode { get; set; }

        public IEnumerable<ValidationError> Errors { get; set; }

        public string ReferenceErrorCode { get; set; }
        
        public string ReferenceDocumentLink { get; set; }

        public ApiException(
            string message, 
            IEnumerable<ValidationError> errors = null,
            string errorCode = "",
            string refLink = "") :base(message)
        {
            Errors = errors;
            ReferenceErrorCode = errorCode;
            ReferenceDocumentLink = refLink;
        }

        public ApiException(System.Exception ex, int statusCode = 500) : base(ex.Message)
        {
//            StatusCode = statusCode;
        }
    }
}