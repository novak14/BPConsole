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
            throw new NotImplementedException();
        }

        public void Delete(UserTestDelete userTestDelete)
        {
            try
            {
                context.UserTestDelete.Remove(userTestDelete);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }

        public void Insert(UserTestInsert userTestInsert)
        {
            try
            {
                context.UserTestInsert.Add(userTestInsert);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }

        public void Insert(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
            #region normalSelect
            //var normalSelect = context.UserTest.ToList();
            #endregion

            #region oneToMany
            //var test = context.UserTest
            //    .Include(one => one.OneToTest)
            //    .ToList();

           
            #endregion
            try
            {
                var testOne = context.OneToTest
               .Include(user => user.UserTests)
               .AsNoTracking()
               .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }

        public void Update(string FirstName, int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserTestUpdate userTestUpdate)
        {
            try
            {
                context.UserTestUpdate.Update(userTestUpdate);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }




        public void TestDb()
        {
            using (var contex = new TestDbContext())
            {
                try
                {
                    var testOne = context.OneToTest
                   .Include(user => user.UserTests)
                   .AsNoTracking()
                   .ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(nameof(ex));
                }
            }
            
        }
    }
}
