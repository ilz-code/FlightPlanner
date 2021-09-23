using System.Text.Json.Serialization;

namespace FlightPlannerI.Models
{
    public class Airport
    {
        [Newtonsoft.Json.JsonIgnore]
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }
    }
}
