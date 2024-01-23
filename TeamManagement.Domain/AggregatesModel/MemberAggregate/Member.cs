using TeamManagement.Domain.AggregatesModel.CountryAggregate;
using TeamManagement.Domain.AggregatesModel.TagAggregate;

namespace TeamManagement.Domain.AggregatesModel.MemberAggregate
{
   public class Member
   {
      public int MemberId { get; set; }
      public required string MemberName { get; set; }
      public decimal SalaryPerYear { get; set; }
      public required MemberType Type { get; set; }
      public int? ContractDurationMonths { get; set; }
      public string? EmployeeRole { get; set; }
      public required Country Country { get; set; }
      public List<Tag?> Tags { get; set; } = [];
   }
}
