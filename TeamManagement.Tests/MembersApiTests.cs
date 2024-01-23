using System.Net;
using System.Net.Http.Headers;
using TeamManagement.Tests.Configuration;

namespace TeamManagement.Tests
{
   public class MembersApiTests
   {
      [Fact]
      public async Task GET_Root_Responds_NotFound()
      {
         await using var application = new TestWebApplicationFactory();

         using var client = application.CreateClient();
         using var response = await client.GetAsync("/");

         Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
      }

      [Fact]
      public async Task GET_Members_Responds_OK()
      {
         await using var application = new TestWebApplicationFactory();

         using var client = application.CreateClient();
         using var response = await client.GetAsync("/members");

         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      }

      [Fact]
      public async Task GET_NonExisting_Member_Responds_NotFound()
      {
         await using var application = new TestWebApplicationFactory();

         using var client = application.CreateClient();
         using var response = await client.GetAsync($"/members/{int.MaxValue}");

         Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
      }

      [Fact]
      public async Task POST_Valid_Member_Responds_Created()
      {
         await using var application = new TestWebApplicationFactory();

         const string memberJson = """
                                   {
                                     "name": "Input Test",
                                     "salaryPerYear": 45,
                                     "type": "Employee",
                                     "employeeRole": "Role Test",
                                     "country": "Brazil",
                                     "tags": [ "Tag Test" ]
                                   }
                                   """;
         using var jsonContent = new StringContent(memberJson);
         jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

         using var client = application.CreateClient();
         using var response = await client.PostAsync("/members", jsonContent);

         Assert.Equal(HttpStatusCode.Created, response.StatusCode);
         Assert.NotNull(response.Headers.Location);
      }

      [Fact]
      public async Task POST_WrongData_Member_Responds_BadRequest()
      {
         await using var application = new TestWebApplicationFactory();

         const string wrongMemberJson = """
                                        {
                                          "name": "Wrong Input Test",
                                          "salaryPerYear": -45,
                                          "type": "Employee",
                                          "employeeRole": "Role Test",
                                          "country": "Brazil",
                                          "tags": [ "Tag Test" ]
                                        }
                                        """;
         using var jsonContent = new StringContent(wrongMemberJson);
         jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

         using var client = application.CreateClient();
         using var response = await client.PostAsync("/members", jsonContent);

         Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
      }

      [Fact]
      public async Task PUT_Valid_Member_Responds_NoContent()
      {
         await using var application = new TestWebApplicationFactory();

         const string memberJson = """
                                        {
                                          "name": "Input Test",
                                          "salaryPerYear": 45,
                                          "type": "Employee",
                                          "employeeRole": "Role Test",
                                          "country": "Brazil",
                                          "tags": [ "Tag Test" ]
                                        }
                                        """;
         using var jsonContent = new StringContent(memberJson);
         jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

         using var client = application.CreateClient();
         using var response = await client.PostAsync("/members", jsonContent);

         var memberId = response.Headers.Location?.OriginalString.Split("/").LastOrDefault();

         const string updatedMemberJson = """
                                      {
                                        "name": "Input Test",
                                        "salaryPerYear": 45,
                                        "type": "Employee",
                                        "employeeRole": "Role Test",
                                        "country": "Brazil",
                                        "tags": [ "Tag Test", "New Tag" ]
                                      }
                                      """;

         using var updateJsonContent = new StringContent(updatedMemberJson);
         updateJsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

         using var updatedResponse = await client.PutAsync($"/members/{memberId}", updateJsonContent);

         Assert.Equal(HttpStatusCode.NoContent, updatedResponse.StatusCode);
      }

      [Fact]
      public async Task PUT_Wrong_Member_Responds_BadRequest()
      {
         await using var application = new TestWebApplicationFactory();

         const string memberJson = """
                                        {
                                          "name": "Input Test",
                                          "salaryPerYear": 45,
                                          "type": "Employee",
                                          "employeeRole": "Role Test",
                                          "country": "Brazil",
                                          "tags": [ "Tag Test" ]
                                        }
                                        """;
         using var jsonContent = new StringContent(memberJson);
         jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

         using var client = application.CreateClient();
         using var response = await client.PostAsync("/members", jsonContent);

         var memberId = response.Headers.Location?.OriginalString.Split("/").LastOrDefault();

         const string updatedMemberJson = """
                                      {
                                        "name": "Input Test",
                                        "salaryPerYear": -45,
                                        "type": "Employee",
                                        "employeeRole": "Role Test",
                                        "country": "Brazil",
                                        "tags": [ "Tag Test", "New Tag" ]
                                      }
                                      """;

         using var updateJsonContent = new StringContent(updatedMemberJson);
         updateJsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

         using var updatedResponse = await client.PutAsync($"/members/{memberId}", updateJsonContent);

         Assert.Equal(HttpStatusCode.BadRequest, updatedResponse.StatusCode);
      }

      [Fact]
      public async Task DELETE_Member_Responds_NoContent()
      {
         await using var application = new TestWebApplicationFactory();

         const string memberJson = """
                                        {
                                          "name": "Input Test",
                                          "salaryPerYear": 45,
                                          "type": "Employee",
                                          "employeeRole": "Role Test",
                                          "country": "Brazil",
                                          "tags": [ "Tag Test" ]
                                        }
                                        """;
         using var jsonContent = new StringContent(memberJson);
         jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

         using var client = application.CreateClient();
         using var response = await client.PostAsync("/members", jsonContent);

         var memberId = response.Headers.Location?.OriginalString.Split("/").LastOrDefault();

         using var deletedResponse = await client.DeleteAsync($"/members/{memberId}");

         Assert.Equal(HttpStatusCode.NoContent, deletedResponse.StatusCode);
      }

      [Fact]
      public async Task DELETE_NonExisting_Member_Responds_NotFound()
      {
         await using var application = new TestWebApplicationFactory();
         using var client = application.CreateClient();
         using var response = await client.DeleteAsync($"/members/{int.MaxValue}");

         Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
      }
   }
}
