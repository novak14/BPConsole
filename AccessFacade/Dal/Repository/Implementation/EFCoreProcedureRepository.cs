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
            try
            {
                var affRows = context.Database.ExecuteSqlCommand("dbo.deleteProcedure @Id = {0}", id);
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
                var affRows = context.Database.ExecuteSqlCommand("dbo.insertProcedure @FirstName = {0}, @LastName = {1}, @Address = {2}, @FkOneToTestId = {3}", FirstName, LastName, Address, FkOneToTestId);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }

        public void Select()
        {
            try
            {
                var userTest = context.UserTest.FromSql("dbo.selectProcedure").ToList();

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
                var affRows = context.Database.ExecuteSqlCommand("dbo.updateProcedure @FirstName = {0}, @Id = {1}", FirstName, id);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }
    }
}
