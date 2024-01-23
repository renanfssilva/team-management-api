namespace TeamManagement.Domain.AggregatesModel.MemberAggregate
{
   public interface IMemberRepository
   {
      Task<IEnumerable<Member>> GetMembersAsync(IEnumerable<string>? tags);
      Task<Member?> GetMemberAsync(int id);
      Task<int> InsertMemberAsync(Member member);
      Task<int> UpdateMemberAsync(Member member);
      Task DeleteMemberAsync(int id);
   }
}
