using Newtonsoft.Json;

namespace Pumpkin.Domain.Events.DataTransferObjects.Order;

public record ProductBasicDetailsDto
{
    [JsonProperty("basketItemCode")] public string BasketItemCode { get; set; }
    [JsonProperty("category")] public string Category { get; set; }
    [JsonProperty("brand")] public string Brand { get; set; }
    [JsonProperty("model")] public string Model { get; set; }
    [JsonProperty("price")] public decimal Price { get; set; }
}