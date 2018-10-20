using AccessFacade.Configuration;
using AccessFacade.Dal.Context;
using AccessFacade.Dal.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccessFacade.Dal.Repository.Implementation
{
    public class EFCoreProcedureRepository : IEFCoreProcedureRepository
    {
        private readonly EfCoreDbContext context;


        public EFCoreProcedureRepository(EfCoreDbContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var affRows = context.Database.ExecuteSqlCommand("dbo.deleteProcedure @Id = {0}", id);
            context.SaveChanges();
        }

        public void Insert(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            var affRows = context.Database.ExecuteSqlCommand("dbo.insertProcedure @FirstName = {0}, @LastName = {1}, @Address = {2}, @FkOneToTestId = {3}", FirstName, LastName, Address, FkOneToTestId);
            context.SaveChanges();
        }

        public void Select()
        {
            var userTest = context.UserTest.FromSql("dbo.selectProcedure").ToList();
        }

        public void Update(string FirstName, int id)
        {
            var affRows = context.Database.ExecuteSqlCommand("dbo.updateProcedure @FirstName = {0}, @Id = {1}", FirstName, id);
            context.SaveChanges();
        }
    }
}
