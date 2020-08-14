using System;
using System.Collections.Generic;

namespace Pumpkin.Core.ResponseWrapper
{
    public class ApiError
    {
        private bool IsError { get; }

        public string ExceptionMessage { get; set; }

        public Action<IEnumerable<ValidationError>> ValidationErrors { get; set; }

        public ApiError(string message)
        {
            IsError = true;
            ExceptionMessage = message;
        }

        public ApiError(string message, Action<IEnumerable<ValidationError>> validationErrors)
            : this(message)
        {
            ValidationErrors = validationErrors;
        }
    }
}