using System.ComponentModel.DataAnnotations;
using Framework.Exceptions;
using Newtonsoft.Json;
using Pumpkin.Domain.Framework.Exceptions;

namespace Pumpkin.Domain.Contracts.Inputs.Order;

public class GetCustomerPoliciesContract: IValidatableObject
{
   [JsonProperty("CustomerId")] public Guid CustomerId { get; set; }
   public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
   {
      if (CustomerId == Guid.Empty)
         throw new Dexception(Situation.Make(SitKeys.Unprocessable, message: "CustomerId has not been sent."));
      
      yield break;
   }
}