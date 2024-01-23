namespace TeamManagement.Domain.AggregatesModel.CountryAggregate
{
   public interface ICountryService
   {
      Task<string?> GetCountryCurrencyAsync(string countryName);
   }
}
