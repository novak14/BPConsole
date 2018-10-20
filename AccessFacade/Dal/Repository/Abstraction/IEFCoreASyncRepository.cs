using AccessFacade.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccessFacade.Dal.Repository.Abstraction
{
    public interface IEFCoreASyncRepository : IBusinessObjectAsync
    {
        Task InsertAsync(UserTestInsert userTestInsert);
        Task UpdateAsync(UserTestUpdate userTestUpdate);
        Task DeleteAsync(UserTestDelete userTestDelete);
    }
}
