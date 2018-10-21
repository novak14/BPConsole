using AccessFacade.Configuration;
using AccessFacade.Dal.Entities;
using AccessFacade.Dal.Repository.Abstraction;
using BpApi.Models;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AccessFacade.Dal.Repository.Implementation
{
    public class DapperSyncRepository : IDapperSyncRepository
    {
        public readonly AccessFacadeOptions options;

        public DapperSyncRepository(IOptions<AccessFacadeOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            this.options = options.Value;
        }

        public void Delete(int id)
        {
            string sql = @"DELETE FROM UserTestDelete WHERE Id = @Id";

            using (var connection = new SqlConnection(options.connectionString))
            {
                var tmp = connection.Execute(sql, new { Id = id});
            }
        }

        public void Insert(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            string sql = @"INSERT INTO UserTestInsert(FirstName, LastName, Address, FkOneToTestId) VALUES(@FirstName, @LastName, @Address, @FkOneToTestId)";

            using (var connection = new SqlConnection(options.connectionString))
            {
                var tmp = connection.Execute(sql, new { FirstName = FirstName, LastName = LastName, Address = Address, FkOneToTestId = FkOneToTestId });
            }
        }

        public void Select()
        {
            #region normalSelect
            //string sql = @"SELECT * FROM UserTest";

            //using (var connection = new SqlConnection(options.connectionString))
            //{
            //    var tmp = connection.Query<UserTest>(sql).ToList();
            //}
            #endregion

            #region oneToMany
            //string sqlOneToMany = @"SELECT * FROM OneToTest INNER JOIN UserTest ON OneToTest.Id = UserTest.FkOneToTestId";

            //using (var connection = new SqlConnection(options.connectionString))
            //{
            //    var lookup = new Dictionary<int, OneToTest>();

            //    var tmp = connection.Query<OneToTest, UserTest, OneToTest>(sqlOneToMany,
            //        (oneToTest, userTest) =>
            //        {
            //            OneToTest oneTest;

            //            if (!lookup.TryGetValue(oneToTest.Id, out oneTest))
            //                lookup.Add(oneToTest.Id, oneTest = oneToTest);

            //            if (userTest != null)
            //                oneTest.UserTests.Add(userTest);

            //            return oneTest;
            //        }).ToList();
            //}
            #endregion

            string sqlOneToMany = @"SELECT * FROM UserTest INNER JOIN OneToTest ON OneToTest.Id = UserTest.FkOneToTestId";

            using (var connection = new SqlConnection(options.connectionString))
            {
                var lookup = new Dictionary<int, OneToTest>();

                var tmp = connection.Query<UserTest, OneToTest, UserTest>(sqlOneToMany,
                    (userTest, oneToTest) =>
                    {
                        userTest.OneToTest = oneToTest;
                        return userTest;
                    }).ToList();
            }
        }

        public string SelectTest()
        {
            string sql = @"SELECT * FROM UserDetails";

            Stopwatch stopwatch = Stopwatch.StartNew();

            using (var connection = new SqlConnection(options.connectionString))
            {
                var tmp = connection.Query<UserInformation>(sql);
                //dom = connection.Query<TestModel>(sql);
            }
            stopwatch.Stop();
            var testStopwatch = stopwatch.Elapsed.ToString();
            return testStopwatch;
        }

        public void Update(string FirstName, int id)
        {
            string sql = @"UPDATE UserTestUpdate SET FirstName = @FirstName WHERE Id = @Id";

            using (var connection = new SqlConnection(options.connectionString))
            {
                var tmp = connection.Execute(sql, new { FirstName = FirstName, Id = id });
            }
        }
    }
}
