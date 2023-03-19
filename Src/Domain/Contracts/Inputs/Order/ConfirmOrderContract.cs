using System.ComponentModel.DataAnnotations;
using Framework.Exceptions;
using Newtonsoft.Json;
using Pumpkin.Domain.Framework.Exceptions;

namespace Pumpkin.Domain.Contracts.Inputs.Order;

public class ConfirmOrderContract : IValidatableObject
{
    [JsonProperty("BasketCode")] public string BasketCode { get; set; }
    [JsonProperty("BasketItemCode")] public string BasketItemCode { get; set; }
    [JsonProperty("SerialNumber")] public string DeviceSerialNumber { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(BasketCode))
            throw new Dexception(Situation.Make(SitKeys.Unprocessable, message: "BasketCode has not been sent."));

        if (string.IsNullOrWhiteSpace(BasketItemCode))
            throw new Dexception(Situation.Make(SitKeys.Unprocessable, message: "BasketItemCode has not been sent."));

        if (string.IsNullOrWhiteSpace(DeviceSerialNumber))
            throw new Dexception(Situation.Make(SitKeys.Unprocessable, message: "SerialNumber has not been sent."));

        yield break;
    }
}