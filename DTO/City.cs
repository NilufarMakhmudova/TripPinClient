using System.Text.Json.Serialization;

namespace TripPinClient.DTO
{
    public class City
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("CountryRegion")]
        public string CountryRegion { get; set; }

        [JsonPropertyName("Region")]
        public string Region { get; set; }
    }

}
