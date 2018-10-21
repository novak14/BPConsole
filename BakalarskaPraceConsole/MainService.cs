using AccessFacade.Business;
using BakalarskaPraceConsole.Models;
using RandomNameGeneratorLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace BakalarskaPraceConsole
{
    public class MainService : IMainService
    {
        private readonly AccessFacadeService accessFacadeService;
        private readonly DapperService dapperService;
        private readonly AdoService adoService;
        private readonly EFCoreService eFCoreService;

        public MainService(
            AccessFacadeService accessFacadeService,
            DapperService dapperService,
            AdoService adoService,
            EFCoreService eFCoreService)
        {
            this.accessFacadeService = accessFacadeService;
            this.dapperService = dapperService;
            this.adoService = adoService;
            this.eFCoreService = eFCoreService;
        }

        public string[] GetSync()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();


            dapper.Start();
            for (int i = 0; i < 100; i ++)
            {
                string dapperSync = dapperService.SelectDapperSync();
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            for (int i = 0; i < 100; i++)
            {
                string adoSync = adoService.SelectAdoSync();
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            for (int i = 0; i < 100; i++)
            {
                string efCoreSync = eFCoreService.SelectEFCoreSync();
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
            //accessFacadeService.InsertSelectResult(times);
        }

        public async Task GetAsync()
        {
            var dapperASync = await dapperService.SelectDapperASync();
            string adoAsync = await adoService.SelectAdoASync();
            string efCoreAsync = await eFCoreService.SelectEFCoreASync();
        }

        public void GetProc()
        {
            string dapperProcedure = dapperService.SelectDapperProcedure();
            string adoProcedure = adoService.SelectAdoProcedure();
            string efCoreProcedure = eFCoreService.SelectEFCoreProcedure();
        }



        public void InsertSync()
        {
            DeleteToInsert();
            var collection = GenerateModelUser();
            foreach (var item in collection)
            {
                string dapperSync = dapperService.InsertDapperSync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }

            DeleteToInsert();
            foreach (var item in collection)
            {
                string adoSync = adoService.InsertAdoSync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }

            DeleteToInsert();
            foreach (var item in collection)
            {
                string efCoreSync = eFCoreService.InsertEFCoreSync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
        }

        public async Task InsertAsync()
        {
            DeleteToInsert();
            var collection = GenerateModelUser();
            foreach (var item in collection)
            {
                string dapperASync = await dapperService.InsertDapperASync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }

            DeleteToInsert();
            foreach (var item in collection)
            {
                string adoAsync = await adoService.InsertAdoASync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }

            DeleteToInsert();
            foreach (var item in collection)
            {
                string efCoreAsync = await eFCoreService.InsertEFCoreASync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
        }

        public void InsertProc()
        {
            DeleteToInsert();
            var collection = GenerateModelUser();
            foreach (var item in collection)
            {
                string dapperProcedure = dapperService.InsertDapperProcedure(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }

            DeleteToInsert();
            foreach (var item in collection)
            {
                string adoProcedure = adoService.InsertAdoProcedure(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }

            DeleteToInsert();
            foreach (var item in collection)
            {
                string efCoreProcedure = eFCoreService.InsertEFCoreProcedure(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
        }



        public void UpdateSync()
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i ++)
            {
                var firstName = random.GenerateRandomFirstName();
                string dapperSync = dapperService.UpdateDapperSync(firstName, i);
            }

            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string adoSync = adoService.UpdateAdoSync(firstName, i);
            }

            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string efCoreSync = eFCoreService.UpdateEFCoreSync(firstName, i);
            }

        }

        public async Task UpdateAsync()
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string dapperASync = await dapperService.UpdateDapperASync(firstName, i);
            }

            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string adoAsync = await adoService.UpdateAdoASync(firstName, i);
            }

            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string efCoreAsync = await eFCoreService.UpdateEFCoreASync(firstName, i);
            }
        }

        public void UpdateProc()
        {
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string dapperProcedure = dapperService.UpdateDapperProcedure(firstName, i);
            }

            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string adoProcedure = adoService.UpdateAdoProcedure(firstName, i);
            }

            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string efCoreProcedure = eFCoreService.UpdateEFCoreProcedure(firstName, i);
            }
        }



        public void DeleteSync()
        {
            InsertToDelete();
            for (int i = 1; i <= 1000; i ++)
            {
                string dapperSync = dapperService.DeleteDapperSync(i);
            }

            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string adoSync = adoService.DeleteAdoSync(i);
            }

            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string efCoreSync = eFCoreService.DeleteEFCoreSync(i);
            }
        }

        public async Task DeleteAsync()
        {
            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string dapperASync = await dapperService.DeleteDapperASync(i);
            }

            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string adoAsync = await adoService.DeleteAdoASync(i);
            }

            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string efCoreAsync = await eFCoreService.DeleteEFCoreASync(i);
            }
        }

        public void DeleteProc()
        {
            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string dapperProcedure = dapperService.DeleteDapperProcedure(i);
            }

            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string adoProcedure = adoService.DeleteAdoProcedure(i);
            }

            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string efCoreProcedure = eFCoreService.DeleteEFCoreProcedure(i);
            }
        }

        private List<ModelUserTest> GenerateModelUser()
        {
            ModelUserTest modelUser = new ModelUserTest();
            for (int i = 0; i < 1000; i++)
            {
                ModelUserTest modelUserTest = new ModelUserTest();
                modelUser.userTestCollection.collections.Add(modelUserTest);
            }

            return modelUser.userTestCollection.collections;
        }

        private void InsertToDelete()
        {
            Random random = new Random();
            for (int i = 1; i <= 1000; i ++)
            {
                accessFacadeService.InsertDelete(random.GenerateRandomFirstName(), i);
            }
        }

        private void DeleteToInsert()
        {
            accessFacadeService.DeleteInsert();
        }
    }
}
