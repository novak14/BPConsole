using AccessFacade.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccessFacade.Business
{
    public class AccessFacadeService
    {
        private IResultHelp resultHelp;

        public AccessFacadeService (IResultHelp resultHelp)
        {
            this.resultHelp = resultHelp;
        }

        public void InsertSelectResult(string[] times, string type)
        {
            resultHelp.SelectResults(times[0], times[1], times[2], type);
        }

        public void InsertInsertResult(string[] times, string type)
        {
            resultHelp.InsertResults(times[0], times[1], times[2], type);
        }

        public void InsertUpdateResult(string[] times, string type)
        {
            resultHelp.UpdateResults(times[0], times[1], times[2], type);
        }

        public void InsertDeleteResult(string[] times, string type)
        {
            resultHelp.DeleteResults(times[0], times[1], times[2], type);
        }

        public void DeleteInsert()
        {
            resultHelp.DeleteTest();
        }

        public void InsertDelete(string firstName, int id)
        {
            resultHelp.InsertTest(firstName, id);
        }

        public async Task DeleteInsertAsync()
        {
            await resultHelp.DeleteTestAsync();
        }

        public async Task InsertDeleteAsync(string firstName, int id)
        {
            await resultHelp.InsertTestAsync(firstName, id);
        }

        public void OpenConnectionPool()
        {
            resultHelp.OpenConnectionPool();
        }

    }
}
