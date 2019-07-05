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
using System.Threading.Tasks;

namespace AccessFacade.Dal.Repository.Implementation
{
    public class EFCoreASyncRepository : IEFCoreASyncRepository
    {
        private readonly EfCoreDbContext context;

        public EFCoreASyncRepository(EfCoreDbContext context)
        {
            this.context = context;
        }

        public async Task<List<OneToTest>> SelectAsync()
        {
            try
            {
                var oneTo = await context.OneToTest
                    .Where(o => o.UserTests.Where(u => u.FkOneToTestId == o.Id).Any())
                    .Include(o => o.UserTests)
                    .ToListAsync();

                return oneTo;
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }

        public async Task UpdateAsync(string FirstName, int id)
        {
            try
            {
                context.UserTestUpdate.Update(new UserTestUpdate
                {
                    Id = id,
                    FirstName = "test"
                });
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                context.UserTestDelete.Remove(new UserTestDelete
                {
                    Id = id
                });
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }

        public async Task InsertAsync(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            try
            {
                await context.UserTestInsert.AddAsync(new UserTestInsert
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Address = Address,
                    FkOneToTestId = FkOneToTestId
                });
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(nameof(ex));
            }
        }
    }
}
