using Dapper;
using System.Data;
using TeamManagement.Domain.AggregatesModel.CountryAggregate;
using TeamManagement.Domain.AggregatesModel.MemberAggregate;
using TeamManagement.Domain.AggregatesModel.TagAggregate;
using TeamManagement.Infrastructure.ConnectionFactory;

namespace TeamManagement.Infrastructure.Repositories
{
   public class MemberRepository(IDbConnectionFactory dbConnectionFactory) : IMemberRepository
   {
      private IDbConnection DbConnection { get; } = dbConnectionFactory.CreateDbConnection();

      public async Task<IEnumerable<Member>> GetMembersAsync(IEnumerable<string>? tags)
      {
         var members = await DbConnection.QueryAsync<Member, Country, Tag, Member>("dbo.spMembers_GetAll",
            (member, country, tag) =>
            {
               member.Country = country;
               member.Tags.Add(tag);
               return member;
            }, param: new
            {
               TagNames = tags == null || !tags.Any() ? null : string.Join(",", tags),
            }, splitOn: "CountryName,TagId", commandType: CommandType.StoredProcedure);

         return MapMemberTags(members);
      }

      public async Task<Member?> GetMemberAsync(int id)
      {
         var members = await DbConnection.QueryAsync<Member, Country, Tag, Member>("dbo.spMembers_Get",
            (member, country, tag) =>
            {
               member.Country = country;
               member.Tags.Add(tag);
               return member;
            }, splitOn: "CountryName,TagId", param: new { Id = id }, commandType: CommandType.StoredProcedure);

         var result = MapMemberTags(members);
         return result.SingleOrDefault();
      }

      public async Task<int> InsertMemberAsync(Member member) => await DbConnection.QuerySingleAsync<int>("dbo.spMembers_Insert",
         new
         {
            Name = member.MemberName,
            member.SalaryPerYear,
            Type = member.Type.ToString(),
            member.ContractDurationMonths,
            member.EmployeeRole,
            member.Country.CountryName,
            member.Country.Currency,
            TagNames = GetTagNamesString(member.Tags),
         }, commandType: CommandType.StoredProcedure);

      public async Task<int> UpdateMemberAsync(Member member) => await DbConnection.ExecuteAsync("dbo.spMembers_Update",
         new
         {
            member.MemberId,
            Name = member.MemberName,
            member.SalaryPerYear,
            Type = member.Type.ToString(),
            member.ContractDurationMonths,
            member.EmployeeRole,
            member.Country.CountryName,
            member.Country.Currency,
            TagNames = GetTagNamesString(member.Tags),
         }, commandType: CommandType.StoredProcedure);

      public async Task DeleteMemberAsync(int id) => await DbConnection.ExecuteAsync("dbo.spMembers_Delete", new { Id = id }, commandType: CommandType.StoredProcedure);

      private static IEnumerable<Member> MapMemberTags(IEnumerable<Member> members)
      {
         var result = members.GroupBy(m => m.MemberId).Select(g =>
         {
            var groupedMember = g.First();
            groupedMember.Tags = g.Select(m => m.Tags.SingleOrDefault()).ToList();
            return groupedMember;
         });

         return result;
      }

      private static string? GetTagNamesString(IEnumerable<Tag?> memberTags)
      {
         var tagNamesJoined = string.Join(',', memberTags.Select(t => t?.TagName));

         return string.IsNullOrWhiteSpace(tagNamesJoined) ? null : tagNamesJoined;
      }
   }
}
