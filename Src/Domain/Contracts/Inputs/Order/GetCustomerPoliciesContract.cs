using Newtonsoft.Json;

namespace Pumpkin.Domain.Contracts.Inputs.Order;

public class GetCustomerPoliciesContract
{
   [JsonProperty("CustomerId")] public Guid CustomerId { get; set; }
}