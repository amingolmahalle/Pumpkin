using System.ComponentModel.DataAnnotations;
using Framework.Exceptions;
using Newtonsoft.Json;
using Pumpkin.Domain.Events.DataTransferObjects.Policy;
using Pumpkin.Domain.Events.DataTransferObjects.Profile;
using Pumpkin.Domain.Framework.Exceptions;

namespace Pumpkin.Domain.Contracts.Inputs.Policy;

public class PolicyRegisterContract : IValidatableObject
{
    [JsonProperty("providerId")] public string BasketCode { get; set; }
    [JsonProperty("policyHolder")] public PolicyHolderDetailDto Customer { get; set; }
    [JsonProperty("policies")] public ProductBasicDetailDto[] Products { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(BasketCode))
            throw new Dexception(Situation.Make(SitKeys.Unprocessable, message: "Basket code has not been sent."));

        if (Customer is null)
            throw new Dexception(Situation.Make(SitKeys.Unprocessable, message: "The buyer's profile has not been sent."));

        yield break;
    }
}