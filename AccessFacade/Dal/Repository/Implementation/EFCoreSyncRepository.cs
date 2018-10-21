using AccessFacade.Configuration;
using AccessFacade.Dal.Context;
using AccessFacade.Dal.Entities;
using AccessFacade.Dal.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
            context.UserTestDelete.Remove(userTestDelete);
            context.SaveChanges();
        }

        public void Insert(UserTestInsert userTestInsert)
        {
            context.UserTestInsert.Add(userTestInsert);
            context.SaveChanges();
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

            var query = (from c in context.UserTest
                         join k in context.OneToTest on c.FkOneToTestId equals k.Id
                         select new { c, k }).ToList();
        }

        public void Update(string FirstName, int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserTestUpdate userTestUpdate)
        {
            context.UserTestUpdate.Update(userTestUpdate);
            context.SaveChanges();

        }
    }
}
