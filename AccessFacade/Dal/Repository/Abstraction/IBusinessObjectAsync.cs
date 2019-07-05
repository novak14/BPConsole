using AccessFacade.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccessFacade.Dal.Repository.Abstraction
{
    public interface IBusinessObjectAsync
    {
        Task<List<OneToTest>> SelectAsync();
        Task InsertAsync(string FirstName, string LastName, string Address, int FkOneToTestId);
        Task UpdateAsync(string FirstName, int id);
        Task DeleteAsync(int id);
    }
}
