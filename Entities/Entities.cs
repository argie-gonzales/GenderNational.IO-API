using System.Text.Json.Serialization;

namespace WebApiExample.Domain.Entities
{
    public class GenderizeResponse
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public double Probability { get; set; }
        public int Count { get; set; }
    }

    public class NationalizeResponse
    {
        public string Name { get; set; }
        public List<Country> Country { get; set; }
    }

    public class Country
    {
        [JsonPropertyName("country_id")]
        public string CountryId { get; set; }
        public double Probability { get; set; }
    }
}
