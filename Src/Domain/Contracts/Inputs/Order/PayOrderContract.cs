using System.ComponentModel.DataAnnotations;
using Framework.Exceptions;
using Newtonsoft.Json;
using Pumpkin.Domain.Framework.Exceptions;

namespace Pumpkin.Domain.Contracts.Inputs.Order;

public class PayOrderContract : IValidatableObject
{
    [JsonProperty("BasketCode")] public string BasketCode { get; set; }
    [JsonProperty("TrackingCode")] public string PaymentTrackingCode { get; set; }
    [JsonProperty("IsPaid")] public bool IsPaid { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(BasketCode))
            throw new Dexception(Situation.Make(SitKeys.Unprocessable, message: "BasketCode has not been sent."));

        if (string.IsNullOrWhiteSpace(PaymentTrackingCode))
            throw new Dexception(Situation.Make(SitKeys.Unprocessable, message: "TrackingCode has not been sent."));

        yield break;
    }
}