namespace WebApiExample.Application.DTOs
{
    public class PersonDto
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public double? GenderProbability { get; set; }
        public string Country { get; set; }
        public double? CountryProbability { get; set; }
    }
}
