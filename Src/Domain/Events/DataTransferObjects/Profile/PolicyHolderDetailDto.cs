using Newtonsoft.Json;

namespace Pumpkin.Domain.Events.DataTransferObjects.Profile;

public record PolicyHolderDetailDto
{
    [JsonProperty("firstname")] public string FirstName { get; set; }
    [JsonProperty("LastName")] public string LastName { get; set; }
    [JsonProperty("mobileNo")] public string MobileNumber { get; set; }
    [JsonProperty("nationalCode")] public string NationalCode { get; set; }
    [JsonProperty("address")] public string Address { get; set; }
    [JsonProperty("isMale")] public bool? Gender { get; set; }
}