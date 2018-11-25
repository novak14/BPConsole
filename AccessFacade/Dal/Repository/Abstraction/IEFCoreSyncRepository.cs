using AccessFacade.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessFacade.Dal.Repository.Abstraction
{
    public interface IEFCoreSyncRepository : BusinessObject
    {
        void Insert(UserTestInsert userTestInsert);
        void Update(UserTestUpdate userTestUpdate);
        void Delete(UserTestDelete userTestDelete);

        void TestDb();
    }
}
