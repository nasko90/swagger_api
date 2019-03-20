using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data_Access.Repository
{
    public class BaseRepository 
    {
        protected readonly string _connectionString;

         public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("iTest");
        }

        protected IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}