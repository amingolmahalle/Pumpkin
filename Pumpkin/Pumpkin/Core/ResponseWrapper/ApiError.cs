using System.Collections.Generic;

namespace Pumpkin.Core.ResponseWrapper
{
    public class ApiError
    {
        public bool IsError { get; set; }

        public string ExceptionMessage { get; set; }

        public string Details { get; set; }

        public string ReferenceErrorCode { get; set; }

        public string ReferenceDocumentLink { get; set; }

        public IEnumerable<ValidationError> ValidationErrors { get; set; }
        

        public ApiError(string message)
        {
            ExceptionMessage = message;
            IsError = true;
        }
        
        public ApiError(string message, IEnumerable<ValidationError> validationErrors)
        {
            ExceptionMessage = message;
            this.ValidationErrors = validationErrors;
        }
    }
}