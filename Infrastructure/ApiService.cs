using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiExample.Application.Interfaces;
using WebApiExample.Domain.Entities;

namespace WebApiExample.Infrastructure.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GenderizeResponse> GetGenderizeData(string name)
        {
            var url = $"https://api.genderize.io/?name={name}";
            return await FetchApiData<GenderizeResponse>(url);
        }

        public async Task<NationalizeResponse> GetNationalizeData(string name)
        {
            var url = $"https://api.nationalize.io/?name={name}";
            return await FetchApiData<NationalizeResponse>(url);
        }

        private async Task<T> FetchApiData<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
