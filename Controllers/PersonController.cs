using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebApiExample.Application.DTOs;
using WebApiExample.Application.Interfaces;

namespace WebApiExample.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IApiService _apiService;
        private readonly ICountryService _countryService;
        private readonly Dictionary<string, string> _countryNames;

        public PersonController(IApiService apiService, ICountryService countryService)
        {
            _apiService = apiService;
            _countryService = countryService;
            _countryNames = _countryService.LoadCountryNames();
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPersonInfo(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Name cannot be empty.");

            try
            {
                var genderizeData = await _apiService.GetGenderizeData(name);
                var nationalizeData = await _apiService.GetNationalizeData(name);

                var topCountry = nationalizeData?.Country?
                    .OrderByDescending(c => c.Probability)
                    .FirstOrDefault();

                var result = new PersonDto
                {
                    Name = name,
                    Gender = genderizeData?.Gender,
                    GenderProbability = genderizeData?.Probability,
                    Country = topCountry != null && _countryNames.ContainsKey(topCountry.CountryId)
                        ? _countryNames[topCountry.CountryId]
                        : topCountry?.CountryId,
                    CountryProbability = topCountry?.Probability
                };

                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(503, $"Error communicating with external APIs: {ex.Message}");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
