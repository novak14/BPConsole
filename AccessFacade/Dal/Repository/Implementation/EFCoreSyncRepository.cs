using AccessFacade.Configuration;
using AccessFacade.Dal.Context;
using AccessFacade.Dal.Entities;
using AccessFacade.Dal.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AccessFacade.Dal.Repository.Implementation
{
    public class EFCoreSyncRepository : IEFCoreSyncRepository
    {
        private readonly EfCoreDbContext context;

        public EFCoreSyncRepository(EfCoreDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            try
            {
                context.UserTestDelete.Remove(new UserTestDelete
                {
                    Id = id
                });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }

        public void Insert(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            try
            {
                context.UserTestInsert.Add(new UserTestInsert
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Address = Address,
                    FkOneToTestId = FkOneToTestId
                });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }

        public List<OneToTest> Select()
        {
            try
            {
                var testOne = context.OneToTest
                    .Where(o => o.UserTests.Where(u => u.FkOneToTestId == o.Id).Any())
                    .Include(o => o.UserTests)
                   .AsNoTracking()
                   .ToList();

                return testOne;
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }

        public void Update(string FirstName, int id)
        {
            try
            {
                context.UserTestUpdate.Update(new UserTestUpdate
                {
                    Id = id,
                    FirstName = "test"
                });
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }




        public void TestDb()
        {
            //using (var contex = new TestDbContext())
            //{
            //    try
            //    {
            //        var testOne = context.OneToTest
            //       .Include(user => user.UserTests)
            //       .AsNoTracking()
            //       .ToList();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(nameof(ex));
            //    }
            //}
            
        }
    }
}
