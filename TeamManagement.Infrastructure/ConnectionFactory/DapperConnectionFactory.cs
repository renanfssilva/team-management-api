using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TeamManagement.Infrastructure.ConnectionFactory
{
   public class DapperConnectionFactory(IConfiguration config) : IDbConnectionFactory
   {
      public IDbConnection CreateDbConnection(string connectionId = "Default")
      {
         var connectionString = config.GetConnectionString(connectionId);

         if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException(nameof(connectionId), "ConnectionString was not found");

         return new SqlConnection(connectionString);
      }
   }
}
