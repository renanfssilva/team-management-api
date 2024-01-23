#nullable disable
using TeamManagement.Domain.AggregatesModel.MemberAggregate;

namespace TeamManagement.Application.DTOs.Member
{
   public class MemberOutput
   {
      public int Id { get; set; }
      public string Name { get; set; }
      public decimal SalaryPerYear { get; set; }
      public MemberType Type { get; set; }
      public int? ContractDurationMonths { get; set; }
      public string EmployeeRole { get; set; }
      public Domain.AggregatesModel.CountryAggregate.Country Country { get; set; }
      public IEnumerable<string> Tags { get; set; } = [];

      public MemberOutput() { }

      public MemberOutput(Domain.AggregatesModel.MemberAggregate.Member member)
      {
         Id = member.MemberId;
         Name = member.MemberName;
         SalaryPerYear = member.SalaryPerYear;
         Type = member.Type;
         ContractDurationMonths = member.ContractDurationMonths;
         EmployeeRole = member.EmployeeRole;
         Country = member.Country;
         Tags = member.Tags.Select(t => t?.TagName);
      }
   }
}
