using System.IO;
using System.Linq;
using System.Collections.Generic;
using WebApiExample.Application.Interfaces;

namespace WebApiExample.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        public Dictionary<string, string> LoadCountryNames()
        {
            var csvFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "countries.csv");
            return File.ReadAllLines(csvFilePath)
                .Skip(1)
                .Select(line => line.Split(','))
                .GroupBy(parts => parts[1].Trim())
                .ToDictionary(group => group.Key, group => group.First()[0].Trim());
        }
    }
}
