using System.Text.Json.Serialization;

namespace TripPinClient.DTO
{
    public class Location
    {
        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("City")]
        public City City { get; set; }

        public override string ToString()
        {
            return $"{City.Region} {City.CountryRegion} {City.Name} {Address}";
        }
    }
}
