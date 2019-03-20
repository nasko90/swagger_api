using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Data_Access.Models;
using Data_Access.Repository;
using Microsoft.Extensions.Configuration;

namespace dapper_example.Repository
{
    public class TestRepository : BaseRepository, IRepository<TestModel>
    {
        private const string TestTableName = "Tests";

        public TestRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public bool Add(TestModel item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TestModel> FindAll()
        {
            using (IDbConnection dbConnection = GetConnection())
            {
                dbConnection.Open();
                var sqlQuery = $"SELECT * FROM {TestTableName}";
                return dbConnection.Query<TestModel>(sqlQuery);
            }
        }

        public TestModel FindBy<TU>(string colName, TU value)
        {
            using (IDbConnection dbConnection = GetConnection())
            {
                dbConnection.Open();
                return dbConnection.Query<TestModel>("get_test_by_id", 
                                                        new { Id = value },
              commandType: CommandType.StoredProcedure).First();
            }
        }

        public bool Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(TestModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}