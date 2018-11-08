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

        public async Task SelectAsync()
        {
            string query = "SELECT * FROM UserTest";
            List<UserTest> userTests = new List<UserTest>();

            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        UserTest userTest = new UserTest();

                        userTest.Id = (int)reader["Id"];
                        userTest.FirstName = reader["FirstName"] as string;
                        userTest.LastName = reader["LastName"] as string;
                        userTest.Address = reader["Address"] as string;
                        userTest.FkOneToTestId = (int)reader["FkOneToTestId"];

                        userTests.Add(userTest);
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
