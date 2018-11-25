using AccessFacade.Configuration;
using AccessFacade.Dal.Entities;
using AccessFacade.Dal.Repository.Abstraction;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AccessFacade.Dal.Repository.Implementation
{
    public class AdoSyncRepository : IAdoSyncRepository
    {
        public readonly AccessFacadeOptions options;

        public AdoSyncRepository(IOptions<AccessFacadeOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            this.options = options.Value;
        }
        public void Delete(int id)
        {
            string query = @"DELETE FROM UserTestDelete WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    connection.Open();
                    var affRows = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public void Insert(string FirstName, string LastName, string Address, int FkOneToTestId)
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

                    connection.Open();
                    var affRows = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public void Select()
        {
            string queryOneToMany = "SELECT * FROM OneToTest INNER JOIN UserTest ON OneToTest.Id = UserTest.FkOneToTestId";

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
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
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
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(nameof(ex));
                    }
                }
            }
            #region normalSelect
            //string query = "SELECT * FROM UserTest";
            //List<UserTest> userTests = new List<UserTest>();

            //using (SqlConnection connection = new SqlConnection(options.connectionString))
            //{
            //    SqlCommand command = new SqlCommand(query, connection);
            //    try
            //    {
            //        connection.Open();
            //        SqlDataReader reader = command.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            UserTest userTest = new UserTest();

            //            userTest.Id = (int)reader["Id"];
            //            userTest.FirstName = reader["FirstName"] as string;
            //            userTest.LastName = reader["LastName"] as string;
            //            userTest.Address = reader["Address"] as string;
            //            userTest.FkOneToTestId = (int)reader["FkOneToTestId"];

            //            userTests.Add(userTest);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(ex.ToString());
            //    }
            //}
            #endregion
            #region oneToMany
            //string queryOneToMany = "SELECT * FROM UserTest INNER JOIN OneToTest ON OneToTest.Id = UserTest.FkOneToTestId";

            //List<UserTest> userTests = new List<UserTest>();

            //using (SqlConnection connection = new SqlConnection(options.connectionString))
            //{
            //    using (SqlCommand command = new SqlCommand(queryOneToMany, connection))
            //    {
            //        try
            //        {
            //            connection.Open();
            //            SqlDataReader reader = command.ExecuteReader();
            //            while (reader.Read())
            //            {
            //                UserTest userTest = new UserTest();

            //                userTest.Id = (int)reader["Id"];
            //                userTest.FirstName = reader["FirstName"] as string;
            //                userTest.LastName = reader["LastName"] as string;
            //                userTest.Address = reader["Address"] as string;
            //                userTest.FkOneToTestId = (int)reader["FkOneToTestId"];

            //                userTest.OneToTest = new OneToTest();
            //                userTest.OneToTest.Id = userTest.FkOneToTestId;
            //                userTest.OneToTest.Name = reader["Name"] as string;
            //                userTest.OneToTest.Age = (int)reader["Age"];
            //                userTests.Add(userTest);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            throw new Exception(ex.ToString());
            //        }
            //    }
            //}
            #endregion
        }

        public void Update(string FirstName, int id)
        {
            string query = @"UPDATE UserTestUpdate SET FirstName = @FirstName WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = FirstName;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    connection.Open();
                    var affRows = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }
    }
}
