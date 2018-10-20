using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccessFacade.Dal.Repository.Abstraction
{
    public interface BusinessObject
    {
        void Select();
        void Insert(string FirstName, string LastName, string Address, int FkOneToTestId);
        void Update(string FirstName, int id);
        void Delete(int id);
    }
}
