using FluentValidation;
using TeamManagement.Domain.AggregatesModel.MemberAggregate;

namespace TeamManagement.Application.DTOs.Member
{
   public class MemberInputValidator : AbstractValidator<MemberInput>
   {
      public MemberInputValidator()
      {
         RuleFor(m => m.Name)
            .NotEmpty()
            .MaximumLength(150);

         RuleFor(m => m.SalaryPerYear)
            .PrecisionScale(20, 2, true)
            .GreaterThanOrEqualTo(0);

         RuleFor(m => m.Type)
            .IsInEnum();

         RuleFor(m => m.ContractDurationMonths)
            .NotEmpty()
            .When(m => m.Type == MemberType.Contractor)
            .GreaterThanOrEqualTo(0)
            .When(m => m.Type == MemberType.Contractor);

         RuleFor(m => m.EmployeeRole)
            .NotEmpty()
            .When(m => m.Type == MemberType.Employee)
            .MaximumLength(50)
            .When(m => m.Type == MemberType.Employee);

         RuleFor(m => m.Country)
            .NotEmpty()
            .MaximumLength(100);

         RuleForEach(m => m.Tags)
            .MaximumLength(100)
            .When(m => m.Tags.Any());
      }
   }
}
