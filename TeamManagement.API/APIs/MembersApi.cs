using TeamManagement.API.Validation;
using TeamManagement.Application.DTOs.Member;
using TeamManagement.Domain.AggregatesModel.CountryAggregate;
using TeamManagement.Domain.AggregatesModel.MemberAggregate;

namespace TeamManagement.API.APIs
{
   public static class MembersApi
   {
      public static void ConfigureMembersApi(this WebApplication app)
      {
         var members = app.MapGroup("/members");

         members.MapGet("/", GetAllMembers);
         members.MapGet("/{id:int}", GetMember);
         members.MapPost("/", CreateMember).AddEndpointFilter<ValidationFilter<MemberInput>>();
         members.MapPut("/{id:int}", UpdateMember).AddEndpointFilter<ValidationFilter<MemberInput>>();
         members.MapDelete("/{id:int}", DeleteMember);
      }

      private static async Task<IResult> GetAllMembers(string[]? tags, IMemberRepository data)
      {
         try
         {
            var result = new List<MemberOutput>();
            result.AddRange((await data.GetMembersAsync(tags)).Select(member => new MemberOutput(member)));

            return TypedResults.Ok(result);
         }
         catch (Exception e)
         {
            return TypedResults.Problem(e.Message);
         }
      }

      private static async Task<IResult> GetMember(int id, IMemberRepository data)
      {
         try
         {
            var member = await data.GetMemberAsync(id);
            return member == null ? TypedResults.NotFound() : TypedResults.Ok(new MemberOutput(member));
         }
         catch (Exception e)
         {
            return TypedResults.Problem(e.Message);
         }
      }

      private static async Task<IResult> CreateMember(MemberInput member, IMemberRepository data, ICountryService countryService)
      {
         try
         {
            var memberId = await data.InsertMemberAsync(await member.ToMemberModelAsync(countryService));

            return TypedResults.Created($"/members/{memberId}");
         }
         catch (Exception e)
         {
            return TypedResults.Problem(e.Message);
         }
      }

      private static async Task<IResult> UpdateMember(int id, MemberInput memberInput, ICountryService countryService, IMemberRepository data)
      {
         try
         {
            var existingMember = await data.GetMemberAsync(id);

            if (existingMember == null)
               return TypedResults.NotFound();

            var member = await memberInput.ToMemberModelAsync(countryService);
            member.MemberId = id;

            await data.UpdateMemberAsync(member);
            return TypedResults.NoContent();
         }
         catch (Exception e)
         {
            return TypedResults.Problem(e.Message);
         }
      }

      private static async Task<IResult> DeleteMember(int id, IMemberRepository data)
      {
         try
         {
            if (await data.GetMemberAsync(id) is null)
               return TypedResults.NotFound();

            await data.DeleteMemberAsync(id);
            return TypedResults.NoContent();
         }
         catch (Exception e)
         {
            return TypedResults.Problem(e.Message);
         }
      }
   }
}
