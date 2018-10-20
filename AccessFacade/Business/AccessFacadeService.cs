using AccessFacade.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessFacade.Business
{
    public class AccessFacadeService
    {
        private IDapperSyncRepository dapperSync;

        public AccessFacadeService (IDapperSyncRepository dapperSync)
        {
            this.dapperSync = dapperSync;
        }

        // Synchronize Dapper
        public string SelectDapperSync()
        {
            dapperSync.Select();
            return dapperSync.SelectTest();
        }

        public string UpdateDapperSync()
        {
            return "test";
        }

        public string InsertDapperSync(string FirstName, int id)
        {
            return dapperSync.InsertTest(FirstName, id);
        }

        public void DeleteDapperSync()
        {
            dapperSync.DeleteTest();
        }

        // Asychronize Dapper
        public string SelectDapperASync()
        {
            return "test";
        }

        public string UpdateDapperASync()
        {
            return "test";
        }

        public string InsertDapperASync()
        {
            return "test";
        }

        public string DeleteDapperASync()
        {
            return "test";
        }

        // Procedure Dapper
        public string SelectDapperProcedure()
        {
            return "test";
        }

        public string UpdateDapperProcedure()
        {
            return "test";
        }

        public string InsertDapperProcedure()
        {
            return "test";
        }

        public string DeleteDapperProcedure()
        {
            return "test";
        }
    }
}
