using System.Collections.Generic;

namespace Pumpkin.Web.ResponseWrapper
{
    public class ApiError
    {
        private bool IsError { get; set; }

        public string ExceptionMessage { get; set; }

        public IEnumerable<AdditionalData> AdditionalDatalist { get; set; }

        public ApiError(string message)
        {
            IsError = true;
            ExceptionMessage = message;
        }

        public ApiError(string message, IEnumerable<AdditionalData> additionalDatalist)
            : this(message)
        {
            AdditionalDatalist = additionalDatalist;
        }
    }
}