using AccessFacade.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessFacade.Business
{
    public class AccessFacadeService
    {
        private IResultHelp resultHelp;

        public AccessFacadeService (IResultHelp resultHelp)
        {
            this.resultHelp = resultHelp;
        }

        public void InsertSelectResult(string[] times)
        {
            resultHelp.SelectResults(times[0], times[1], times[2]);
        }

        public void DeleteInsert()
        {
            resultHelp.DeleteTest();
        }

        public void InsertDelete(string firstName, int id)
        {
            resultHelp.InsertTest(firstName, id);
        }

    }
}
