using AccessFacade.Configuration;
using AccessFacade.Dal.Entities;
using AccessFacade.Dal.Repository.Abstraction;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AccessFacade.Dal.Repository.Implementation
{
    public class AdoAsyncRepository : IAdoASyncRepository
    {
        public readonly AccessFacadeOptions options;

        public AdoAsyncRepository(IOptions<AccessFacadeOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            this.options = options.Value;
        }

        public async Task DeleteAsync(int id)
        {
            string query = @"DELETE FROM UserTestDelete WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    await connection.OpenAsync();
                    var affRows = await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public async Task InsertAsync(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            string query = @"INSERT INTO UserTestInsert(FirstName, LastName, Address, FkOneToTestId) VALUES(@FirstName, @LastName, @Address, @FkOneToTestId)";
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = LastName;
                    command.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = Address;
                    command.Parameters.Add("@FkOneToTestId", SqlDbType.Int).Value = FkOneToTestId;

                    await connection.OpenAsync();
                    var affRows = await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public async Task<List<OneToTest>> SelectAsync()
        {
            string query = @"SELECT 
                                OneToTest.Id,
                                OneToTest.Name,
                                OneToTest.Age,
                                UserTest.Id AS UserId,
                                UserTest.FirstName,
                                UserTest.LastName,
                                UserTest.Address,
                                UserTest.FkOneToTestId
                                FROM OneToTest 
                            INNER JOIN UserTest 
                            ON OneToTest.Id = UserTest.FkOneToTestId";

            List<OneToTest> oneToTests = new List<OneToTest>();
            var lookup = new Dictionary<int, OneToTest>();

            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            int oneTestId = (int)reader["Id"];

                            OneToTest oneTest;
                            if (!lookup.TryGetValue(oneTestId, out oneTest))
                            {
                                oneTest = new OneToTest();
                                oneTest.Id = oneTestId;
                                oneTest.Name = reader["Name"] as string;
                                oneTest.Age = (int)reader["Age"];

                                lookup.Add(oneTestId, oneTest);
                                oneToTests.Add(oneTest);
                            }

                            UserTest userTest = new UserTest();
                            userTest.Id = (int)reader["UserId"];
                            userTest.FirstName = reader["FirstName"] as string;
                            userTest.LastName = reader["LastName"] as string;
                            userTest.Address = reader["Address"] as string;
                            userTest.FkOneToTestId = (int)reader["FkOneToTestId"];

                            oneTest.UserTests.Add(userTest);
                        }
                        return oneToTests;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public async Task UpdateAsync(string FirstName, int id)
        {
            string query = @"UPDATE UserTestUpdate SET FirstName = @FirstName WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = FirstName;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    await connection.OpenAsync();
                    var affRows = await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }
    }
}
