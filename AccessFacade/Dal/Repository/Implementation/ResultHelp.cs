using AccessFacade.Configuration;
using AccessFacade.Dal.Repository.Abstraction;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AccessFacade.Dal.Repository.Implementation
{
    public class ResultHelp : IResultHelp
    {
        public readonly AccessFacadeOptions options;

        public ResultHelp(IOptions<AccessFacadeOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            this.options = options.Value;
        }

        public void DeleteResults(string dapper, string ado, string efCore)
        {
            string sql = @"INSERT INTO DeleteResults(DapperSync, AdoSync, EfCoreSync) VALUES(@DapperSync, @AdoSync, @EfCoreSync)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                var tmp = connection.Execute(sql, new { DapperSync = dapper, AdoSync = ado, EfCoreSync = efCore });
            }
        }

        public void DeleteTest()
        {
            string sql = @"DELETE FROM UserTestInsert";

            using (var connection = new SqlConnection(options.connectionString))
            {
                var tmp = connection.Execute(sql);
            }
        }

        public void InsertResults(string dapper, string ado, string efCore)
        {
            string sql = @"INSERT INTO InsertResults(DapperSync, AdoSync, EfCoreSync) VALUES(@DapperSync, @AdoSync, @EfCoreSync)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                var tmp = connection.Execute(sql, new { DapperSync = dapper, AdoSync = ado, EfCoreSync = efCore });
            }
        }

        public void InsertTest(string FirstName, int id)
        {
            string sql = @"INSERT INTO UserTestDelete(Id, FirstName) VALUES(@Id, @FirstName)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                var tmp = connection.Execute(sql, new { Id = id, FirstName = FirstName });
                if (tmp > 0)
                {
                    int a = 3;
                }
            }
        }

        public void SelectResults(string dapper, string ado, string efCore)
        {
            string sql = @"INSERT INTO SelectResults(DapperSync, AdoSync, EfCoreSync) VALUES(@DapperSync, @AdoSync, @EfCoreSync)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                var tmp = connection.Execute(sql, new { DapperSync = dapper, AdoSync = ado, EfCoreSync = efCore });
            }
        }

        public void UpdateResults(string dapper, string ado, string efCore)
        {
            string sql = @"INSERT INTO UpdateResults(DapperSync, AdoSync, EfCoreSync) VALUES(@DapperSync, @AdoSync, @EfCoreSync)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                var tmp = connection.Execute(sql, new { DapperSync = dapper, AdoSync = ado, EfCoreSync = efCore });
            }
        }
    }
}
