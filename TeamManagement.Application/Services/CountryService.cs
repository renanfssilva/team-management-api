using Newtonsoft.Json;
using TeamManagement.Application.DTOs.Country;
using TeamManagement.Domain.AggregatesModel.CountryAggregate;

namespace TeamManagement.Application.Services
{
   public class CountryService : ICountryService
   {
      public async Task<string?> GetCountryCurrencyAsync(string countryName)
      {
         var apiUrl = $"https://restcountries.com/v3.1/name/{countryName}";

         try
         {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
               throw new ArgumentException("Country was not found");

            var content = await response.Content.ReadAsStringAsync();
            var countriesArray = JsonConvert.DeserializeObject<List<CountryInfo>>(content);
            var country = countriesArray?.FirstOrDefault();
            var currencies = country?.Currencies;

            return currencies?.Keys.FirstOrDefault();
         }
         catch (Exception ex)
         {
            throw new Exception("There was a problem retrieving the country data", ex);
         }
      }
   }
}
