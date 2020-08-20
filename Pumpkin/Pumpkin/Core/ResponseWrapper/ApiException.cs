using System.Collections.Generic;
using System;
using System.Net;

namespace Pumpkin.Core.ResponseWrapper
{
    public abstract class ApiException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public object AdditionalData { get; }

        public IEnumerable<ValidationError> Errors { get; set; }

        protected ApiException(
            string message,
            HttpStatusCode httpStatusCode,
            IEnumerable<ValidationError> errors) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
        }

        protected ApiException(
            string message,
            HttpStatusCode httpStatusCode,
            IEnumerable<ValidationError> errors,
            object additionalData)
            : this(message, httpStatusCode, errors)
        {
            AdditionalData = additionalData;
        }
    }
}