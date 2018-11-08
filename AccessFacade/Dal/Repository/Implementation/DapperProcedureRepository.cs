using AccessFacade.Configuration;
using AccessFacade.Dal.Entities;
using AccessFacade.Dal.Repository.Abstraction;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AccessFacade.Dal.Repository.Implementation
{
    public class DapperProcedureRepository : IDapperProceudureRepository
    {
        public readonly AccessFacadeOptions options;

        public DapperProcedureRepository(IOptions<AccessFacadeOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            this.options = options.Value;
        }

        public void Select()
        {
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = connection.Query<UserTest>("dbo.selectProcedure", commandType: CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public void Insert(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = connection.Query<UserTest>("dbo.insertProcedure",
                    new { FirstName = FirstName, LastName = LastName, Address = Address, FkOneToTestId = FkOneToTestId },
                    commandType: CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = connection.Query<UserTest>("dbo.deleteProcedure",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }

        public void Update(string FirstName, int id)
        {
            using (SqlConnection connection = new SqlConnection(options.connectionString))
            {
                try
                {
                    var tmp = connection.Query<UserTest>("dbo.updateProcedure",
                    new { FirstName = FirstName, Id = id },
                    commandType: CommandType.StoredProcedure).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
        }
    }
}
