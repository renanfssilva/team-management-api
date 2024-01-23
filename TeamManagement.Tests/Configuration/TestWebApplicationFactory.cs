using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TeamManagement.API;

namespace TeamManagement.Tests.Configuration
{
   internal class TestWebApplicationFactory(string environment = "Development") : WebApplicationFactory<Program>
   {
      protected override IHost CreateHost(IHostBuilder builder)
      {
         builder.UseEnvironment(environment);

         builder.ConfigureServices(services =>
         {
            services.AddScoped(sp => new DbContextOptionsBuilder()
               .UseInMemoryDatabase("team_management")
               .UseApplicationServiceProvider(sp)
               .Options);
         });

         return base.CreateHost(builder);
      }
   }
}
