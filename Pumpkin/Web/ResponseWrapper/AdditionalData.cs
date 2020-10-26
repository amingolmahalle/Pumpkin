using Newtonsoft.Json;

namespace Pumpkin.Web.ResponseWrapper
{
    public class AdditionalData
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public AdditionalData(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}