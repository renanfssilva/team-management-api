using System.Data;

namespace TeamManagement.Infrastructure.ConnectionFactory
{
   public interface IDbConnectionFactory
   {
      IDbConnection CreateDbConnection(string connectionId = "Default");
   }
}
