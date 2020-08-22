using System;
using System.Collections.Generic;
using System.Net;

namespace Pumpkin.Web.ResponseWrapper
{
    public class ApiException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        public IEnumerable<AdditionalData> AdditionalDataList { get; set; }

        public ApiException(
            HttpStatusCode httpStatusCode,
            string message,
            Exception exception = null,
            IEnumerable<AdditionalData> additionalDataList = null) : base(message, exception)
        {
            HttpStatusCode = httpStatusCode;
            AdditionalDataList = additionalDataList;
        }
    }
}