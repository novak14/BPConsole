using AccessFacade.Configuration;
using AccessFacade.Dal.Entities;
using AccessFacade.Dal.Repository.Abstraction;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AccessFacade.Dal.Repository.Implementation
{
    public class AdoProcedureRepository : IAdoProcedureRepository
    {
        public readonly AccessFacadeOptions options;

        public AdoProcedureRepository(IOptions<AccessFacadeOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            this.options = options.Value;
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                SqlCommand command = new SqlCommand("dbo.deleteProcedure", connection);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    connection.Open();
                    var affRows = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        public void Insert(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.insertProcedure", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = FirstName;
                        command.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = LastName;
                        command.Parameters.Add("@Address", SqlDbType.VarChar, 50).Value = Address;
                        command.Parameters.Add("@FkOneToTestId", SqlDbType.Int).Value = FkOneToTestId;

                        connection.Open();
                        var affRows = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                }
            }
        }

        public List<OneToTest> Select()
        {
            #region normalSelect
            List<OneToTest> oneToTests = new List<OneToTest>();
            var lookup = new Dictionary<int, OneToTest>();

            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.selectManyProcedure", connection))
                {
                    try
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;

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
                                userTest.Id = (int)reader["UserTestId"];
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
                        throw new Exception(ex.ToString());
                    }
                }
            }
            #endregion
        }

        public void Update(string FirstName, int id)
        {
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                SqlCommand command = new SqlCommand("dbo.updateProcedure", connection);
                try
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = FirstName;
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    connection.Open();
                    var affRows = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
    }
}
