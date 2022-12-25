using Newtonsoft.Json;

namespace Pumpkin.Domain.Contracts.Inputs.Policy;

public class GetUserPoliciesContract
{
   [JsonProperty("CustomerId")] public Guid BuyerId { get; set; }
}