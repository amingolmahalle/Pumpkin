using System.Collections.Generic;
using System;
using System.Net;

namespace Pumpkin.Core.ResponseWrapper
{
    public abstract class ApiException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public object AdditionalData { get; }

        public Action<IEnumerable<ValidationError>> Errors { get; }

        protected ApiException(
            string message,
            HttpStatusCode httpStatusCode,
            Action<IEnumerable<ValidationError>> errors) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            Errors = errors;
        }

        protected ApiException(
            string message,
            HttpStatusCode httpStatusCode,
            Action<IEnumerable<ValidationError>> errors,
            object additionalData)
            : this(message, httpStatusCode, errors)
        {
            AdditionalData = additionalData;
        }
    }
}