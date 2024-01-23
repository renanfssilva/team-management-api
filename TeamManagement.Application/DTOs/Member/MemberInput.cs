using TeamManagement.Domain.AggregatesModel.CountryAggregate;
using TeamManagement.Domain.AggregatesModel.MemberAggregate;
using TeamManagement.Domain.AggregatesModel.TagAggregate;
using ArgumentNullException = System.ArgumentNullException;

namespace TeamManagement.Application.DTOs.Member
{
   public class MemberInput
   {
      public required string Name { get; set; }
      public decimal SalaryPerYear { get; set; }
      public required MemberType Type { get; set; }
      public int? ContractDurationMonths { get; set; }
      public string? EmployeeRole { get; set; }
      public required string Country { get; set; }
      public IEnumerable<string> Tags { get; set; } = [];
   }

   public static class MemberInputExtensions
   {
      public static async Task<Domain.AggregatesModel.MemberAggregate.Member> ToMemberModelAsync(this MemberInput input, ICountryService countryService)
      {
         ArgumentNullException.ThrowIfNull(input);

         var tags = CreateTagsFromList(input);
         var countryCurrency = await countryService.GetCountryCurrencyAsync(input.Country);

         var member = new Domain.AggregatesModel.MemberAggregate.Member
         {
            MemberName = input.Name,
            SalaryPerYear = input.SalaryPerYear,
            Type = input.Type,
            ContractDurationMonths = input.ContractDurationMonths,
            EmployeeRole = input.EmployeeRole,
            Country = new Domain.AggregatesModel.CountryAggregate.Country
            {
               CountryName = input.Country,
               Currency = countryCurrency,
            },
            Tags = tags,
         };

         return member;
      }

      private static List<Tag?> CreateTagsFromList(MemberInput input)
      {
         return !input.Tags.Any() ? new List<Tag?>() : input.Tags.Select(t => new Tag { TagName = t }).ToList();
      }
   }
}
