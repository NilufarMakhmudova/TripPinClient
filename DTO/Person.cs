using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TripPinClient.DTO
{
    public class Person
    {
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }

        [JsonPropertyName("FirstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("LastName")]
        public string LastName { get; set; }       

        [JsonPropertyName("Gender")]
        public PersonGender Gender { get; set; }        

        [JsonPropertyName("Emails")]
        public List<string> Emails { get; set; }

        [JsonPropertyName("AddressInfo")]
        public List<Location> AddressInfo { get; set; }       
        
        [JsonPropertyName("Trips")]
        public List<Trip> Trips { get; set; }       

        [JsonIgnore]
        public string FullName => $"{FirstName} {LastName}";
    }
}
