﻿using AccessFacade.Configuration;
using AccessFacade.Dal.Entities;
using AccessFacade.Dal.Repository.Abstraction;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessFacade.Dal.Repository.Implementation
{
    public class DapperAsyncRepository : IDapperAsyncRepository
    {
        public readonly AccessFacadeOptions options;

        public DapperAsyncRepository(IOptions<AccessFacadeOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            this.options = options.Value;
        }

        public async Task DeleteAsync(int id)
        {
            string sql = @"DELETE FROM UserTestDelete WHERE Id = @Id";

            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = await connection.ExecuteAsync(sql, new { Id = id });
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public async Task InsertAsync(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            string sql = @"INSERT INTO UserTestInsert(FirstName, LastName, Address, FkOneToTestId) VALUES(@FirstName, @LastName, @Address, @FkOneToTestId)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = await connection.ExecuteAsync(sql, new { FirstName = FirstName, LastName = LastName, Address = Address, FkOneToTestId = FkOneToTestId });
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public async Task<List<OneToTest>> SelectAsync()
        {
            #region normalSelect
            string sqlOneToMany = @"SELECT 
                                OneToTest.Id,
                                OneToTest.Name,
                                OneToTest.Age,
                                UserTest.Id,
                                UserTest.FirstName,
                                UserTest.LastName,
                                UserTest.Address,
                                UserTest.FkOneToTestId
                                FROM OneToTest 
                            INNER JOIN UserTest 
                            ON OneToTest.Id = UserTest.FkOneToTestId";
            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var lookup = new Dictionary<int, OneToTest>();

                    var tmp = (connection.QueryAsync<OneToTest, UserTest, OneToTest>(sqlOneToMany,
                        (oneToTest, userTest) =>
                        {
                            OneToTest oneTest;

                            if (!lookup.TryGetValue(oneToTest.Id, out oneTest))
                                lookup.Add(oneToTest.Id, oneTest = oneToTest);

                            if (userTest != null)
                                oneTest.UserTests.Add(userTest);

                            return oneTest;
                        })).Result.Distinct().ToList();

                    return tmp;
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
            #endregion
        }

        public async Task UpdateAsync(string FirstName, int id)
        {
            string sql = @"UPDATE UserTestUpdate SET FirstName = @FirstName WHERE Id = @Id";

            using (var connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = await connection.ExecuteAsync(sql, new { FirstName = FirstName, Id = id });
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }
    }
}
