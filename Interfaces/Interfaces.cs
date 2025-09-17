using WebApiExample.Domain.Entities;

namespace WebApiExample.Application.Interfaces
{
    public interface ICountryService
    {
        Dictionary<string, string> LoadCountryNames();
    }

    public interface IApiService
    {
        Task<GenderizeResponse> GetGenderizeData(string name);
        Task<NationalizeResponse> GetNationalizeData(string name);
    }
}
