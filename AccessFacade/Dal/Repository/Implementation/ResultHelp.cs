﻿using AccessFacade.Configuration;
using AccessFacade.Dal.Repository.Abstraction;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

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

        public void DeleteTest()
        {
            string sql = @"DELETE FROM UserTestInsert";

            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = connection.Execute(sql);
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public async Task DeleteTestAsync()
        {
            string sql = @"DELETE FROM UserTestInsert";

            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public void InsertTest(string FirstName, int id)
        {
            string sql = @"INSERT INTO UserTestDelete(Id, FirstName) VALUES(@Id, @FirstName)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = connection.Execute(sql, new { Id = id, FirstName = FirstName });
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public async Task InsertTestAsync(string FirstName, int id)
        {
            string sql = @"INSERT INTO UserTestDelete(Id, FirstName) VALUES(@Id, @FirstName)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = await connection.ExecuteAsync(sql, new { Id = id, FirstName = FirstName });
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public void SelectResults(string dapper, string ado, string efCore, string type)
        {
            string sql = @"INSERT INTO SelectResults(Dapper, Ado, EfCore, Type, Date) VALUES(@Dapper, @Ado, @EfCore, @Type, @Date)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = connection.Execute(sql, new { Dapper = dapper, Ado = ado, EfCore = efCore, Type = type, Date = DateTime.Now });
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public void InsertResults(string dapper, string ado, string efCore, string type)
        {
            string sql = @"INSERT INTO InsertResults(Dapper, Ado, EfCore, Type, Date) VALUES(@Dapper, @Ado, @EfCore, @Type, @Date)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = connection.Execute(sql, new { Dapper = dapper, Ado = ado, EfCore = efCore, Type = type, Date = DateTime.Now });
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public void UpdateResults(string dapper, string ado, string efCore, string type)
        {
            string sql = @"INSERT INTO UpdateResults(Dapper, Ado, EfCore, Type, Date) VALUES(@Dapper, @Ado, @EfCore, @Type, @Date)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = connection.Execute(sql, new { Dapper = dapper, Ado = ado, EfCore = efCore, Type = type, Date = DateTime.Now });
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public void DeleteResults(string dapper, string ado, string efCore, string type)
        {
            string sql = @"INSERT INTO DeleteResults(Dapper, Ado, EfCore, Type, Date) VALUES(@Dapper, @Ado, @EfCore, @Type, @Date)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = connection.Execute(sql, new { Dapper = dapper, Ado = ado, EfCore = efCore, Type = type, Date = DateTime.Now });
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public void OpenConnectionPool()
        {
            using (var connection = new SqlConnection(options.connectionString))
            {
                connection.Open();
                connection.Close();
            }
        }
    }
}
