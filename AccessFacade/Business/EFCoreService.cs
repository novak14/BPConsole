using AccessFacade.Dal.Entities;
using AccessFacade.Dal.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AccessFacade.Business
{
    public class EFCoreService
    {
        private readonly IEFCoreSyncRepository eFCoreSyncRepository;
        private readonly IEFCoreASyncRepository eFCoreASyncRepository;
        private readonly IEFCoreProcedureRepository eFCoreProcedureRepository;

        public EFCoreService(
            IEFCoreSyncRepository eFCoreSyncRepository,
            IEFCoreASyncRepository eFCoreASyncRepository,
            IEFCoreProcedureRepository eFCoreProcedureRepository)
        {
            this.eFCoreSyncRepository = eFCoreSyncRepository;
            this.eFCoreASyncRepository = eFCoreASyncRepository;
            this.eFCoreProcedureRepository = eFCoreProcedureRepository;
        }

        public string TestSync()
        {
            eFCoreSyncRepository.TestDb();
            return "test";
        }

        public string TestProc()
        {
            eFCoreProcedureRepository.TestDb();
            return "test";
        }


        #region sync
        // Synchronize EfCore
        public string SelectEFCoreSync()
        {
            eFCoreSyncRepository.Select();
            return "test";
        }

        public string UpdateEFCoreSync(string FirstName, int id)
        {
            eFCoreSyncRepository.Update(FirstName, id);

            return "test";
        }

        public string InsertEFCoreSync(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            eFCoreSyncRepository.Insert(FirstName, LastName, Address, FkOneToTestId);

            return "test";
        }

        public string DeleteEFCoreSync(int id)
        {
            eFCoreSyncRepository.Delete(id);

            return "test";
        }
        #endregion

        #region async
        // Asychronize EfCore
        public async Task<string> SelectEFCoreASync()
        {
            await eFCoreASyncRepository.SelectAsync();
            return "test";
        }

        public async Task<string> UpdateEFCoreASync(string FirstName, int id)
        {
            await eFCoreASyncRepository.UpdateAsync(FirstName, id);

            return "test";
        }

        public async Task<string> InsertEFCoreASync(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            await eFCoreASyncRepository.InsertAsync(FirstName, LastName, Address, FkOneToTestId);

            return "test";
        }

        public async Task<string> DeleteEFCoreASync(int id)
        {
            await eFCoreASyncRepository.DeleteAsync(id);

            return "test";
        }
        #endregion

        #region procedure
        // Procedure EfCore
        public string SelectEFCoreProcedure()
        {
            eFCoreProcedureRepository.Select();

            return "test";
        }

        public string UpdateEFCoreProcedure(string FirstName, int id)
        {
            eFCoreProcedureRepository.Update(FirstName, id);

            return "test";
        }

        public string InsertEFCoreProcedure(string FirstName, string LastName, string Address, int FkOneToTestId)
        {
            eFCoreProcedureRepository.Insert(FirstName, LastName, Address, FkOneToTestId);

            return "test";
        }

        public string DeleteEFCoreProcedure(int id)
        {
            eFCoreProcedureRepository.Delete(id);

            return "test";
        }
        #endregion
    }
}
