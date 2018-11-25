using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccessFacade.Dal.Repository.Abstraction
{
    public interface IResultHelp
    {
        void SelectResults(string dapper, string ado, string efCore, string type);
        void InsertResults(string dapper, string ado, string efCore);
        void UpdateResults(string dapper, string ado, string efCore);
        void DeleteResults(string dapper, string ado, string efCore);

        void InsertTest(string FirstName, int id);
        Task InsertTestAsync(string FirstName, int id);
        void DeleteTest();
        Task DeleteTestAsync();
        void OpenConnectionPool();
    }
}
