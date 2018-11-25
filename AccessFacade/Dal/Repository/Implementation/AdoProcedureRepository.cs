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

        public void Select()
        {
            #region normalSelect
            List<UserTest> userTests = new List<UserTest>();

            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.selectProcedure", connection))
                {
                    try
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
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
