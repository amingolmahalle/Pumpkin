using System.ComponentModel.DataAnnotations;
using Framework.Exceptions;
using Newtonsoft.Json;
using Pumpkin.Domain.Events.DataTransferObjects.Order;
using Pumpkin.Domain.Events.DataTransferObjects.Profile;
using Pumpkin.Domain.Framework.Exceptions;

namespace Pumpkin.Domain.Contracts.Inputs.Order;

public record RegisterOrderContract : IValidatableObject
{
    [JsonProperty("providerId")] public string BasketCode { get; set; }
    [JsonProperty("policyHolder")] public PolicyHolderDetailDto Customer { get; set; }
    [JsonProperty("products")] public ProductBasicDetailsDto[] Products { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(BasketCode))
            throw new Dexception(Situation.Make(SitKeys.Unprocessable, message: "BasketCode has not been sent."));

        if (Customer is null)
            throw new Dexception(Situation.Make(SitKeys.Unprocessable, message: "The Customer's profile has not been sent."));

        yield break;
    }
}