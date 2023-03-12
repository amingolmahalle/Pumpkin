using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Pumpkin.Domain.Events.DataTransferObjects.Order;

[DataContract]
public class ProductBasicDetailDto
{
    [JsonProperty("basketItemCode")] public string BasketItemCode { get; set; }
    [JsonProperty("category")] public string Category { get; set; }
    [JsonProperty("brand")] public string Brand { get; set; }
    [JsonProperty("model")] public string Model { get; set; }
    [JsonProperty("price")] public decimal Price { get; set; }
}