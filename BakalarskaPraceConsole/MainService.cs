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

            accessFacadeService.OpenConnectionPool();

            dapper.Start();
            for (int i = 0; i < 1000; i ++)
            {
                string dapperSync = dapperService.SelectDapperSync();
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            for (int i = 0; i < 1000; i++)
            {
                string adoSync = adoService.SelectAdoSync();
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            for (int i = 0; i < 1000; i++)
            {
                string efCoreSync = eFCoreService.SelectEFCoreSync();
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
            //accessFacadeService.InsertSelectResult(times);
        }

        public async Task<string[]> GetAsync()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();

            dapper.Start();
            for (int i = 0; i < 1000; i++)
            {
                var dapperASync = await dapperService.SelectDapperASync();
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            for (int i = 0; i < 1000; i++)
            {
                string adoAsync = await adoService.SelectAdoASync();
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            for (int i = 0; i < 1000; i++)
            {
                string efCoreAsync = await eFCoreService.SelectEFCoreASync();
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
        }

        public string[] GetProc()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();

            dapper.Start();
            for (int i = 0; i < 1000; i++)
            {
                string dapperProcedure = dapperService.SelectDapperProcedure();
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            for (int i = 0; i < 1000; i++)
            {
                string adoProcedure = adoService.SelectAdoProcedure();
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            for (int i = 0; i < 1000; i++)
            {
                string efCoreProcedure = eFCoreService.SelectEFCoreProcedure();
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
        }



        public string[] InsertSync()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();

            DeleteToInsert();
            var collection = GenerateModelUser();
            dapper.Start();
            foreach (var item in collection)
            {
                string dapperSync = dapperService.InsertDapperSync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            DeleteToInsert();
            foreach (var item in collection)
            {
                string adoSync = adoService.InsertAdoSync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            DeleteToInsert();
            foreach (var item in collection)
            {
                string efCoreSync = eFCoreService.InsertEFCoreSync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
        }

        public async Task<string[]> InsertAsync()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();

            DeleteToInsert();
            var collection = GenerateModelUser();

            dapper.Start();
            foreach (var item in collection)
            {
                string dapperASync = await dapperService.InsertDapperASync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            DeleteToInsert();
            foreach (var item in collection)
            {
                string adoAsync = await adoService.InsertAdoASync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            DeleteToInsert();
            foreach (var item in collection)
            {
                string efCoreAsync = await eFCoreService.InsertEFCoreASync(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
        }

        public string[] InsertProc()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();

            DeleteToInsert();
            var collection = GenerateModelUser();

            dapper.Start();
            foreach (var item in collection)
            {
                string dapperProcedure = dapperService.InsertDapperProcedure(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            DeleteToInsert();
            foreach (var item in collection)
            {
                string adoProcedure = adoService.InsertAdoProcedure(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            DeleteToInsert();
            foreach (var item in collection)
            {
                string efCoreProcedure = eFCoreService.InsertEFCoreProcedure(item.FirstName, item.LastName, item.Address, item.FkOneToTestId);
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
        }



        public string[] UpdateSync()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();

            Random random = new Random();

            dapper.Start();
            for (int i = 0; i < 1000; i ++)
            {
                var firstName = random.GenerateRandomFirstName();
                string dapperSync = dapperService.UpdateDapperSync(firstName, i);
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string adoSync = adoService.UpdateAdoSync(firstName, i);
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string efCoreSync = eFCoreService.UpdateEFCoreSync(firstName, i);
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
        }

        public async Task<string[]> UpdateAsync()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();

            Random random = new Random();

            dapper.Start();
            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string dapperASync = await dapperService.UpdateDapperASync(firstName, i);
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string adoAsync = await adoService.UpdateAdoASync(firstName, i);
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string efCoreAsync = await eFCoreService.UpdateEFCoreASync(firstName, i);
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
        }

        public string[] UpdateProc()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();

            Random random = new Random();

            dapper.Start();
            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string dapperProcedure = dapperService.UpdateDapperProcedure(firstName, i);
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string adoProcedure = adoService.UpdateAdoProcedure(firstName, i);
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            for (int i = 0; i < 1000; i++)
            {
                var firstName = random.GenerateRandomFirstName();
                string efCoreProcedure = eFCoreService.UpdateEFCoreProcedure(firstName, i);
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
        }



        public string[] DeleteSync()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();

            InsertToDelete();
            dapper.Start();
            for (int i = 1; i <= 1000; i ++)
            {
                string dapperSync = dapperService.DeleteDapperSync(i);
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string adoSync = adoService.DeleteAdoSync(i);
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string efCoreSync = eFCoreService.DeleteEFCoreSync(i);
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
        }

        public async Task<string[]> DeleteAsync()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();

            InsertToDelete();
            dapper.Start();
            for (int i = 1; i <= 1000; i++)
            {
                string dapperASync = await dapperService.DeleteDapperASync(i);
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string adoAsync = await adoService.DeleteAdoASync(i);
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string efCoreAsync = await eFCoreService.DeleteEFCoreASync(i);
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
        }

        public string[] DeleteProc()
        {
            string[] times = new string[3];
            Stopwatch dapper = new Stopwatch();
            Stopwatch ado = new Stopwatch();
            Stopwatch efCore = new Stopwatch();

            InsertToDelete();
            dapper.Start();
            for (int i = 1; i <= 1000; i++)
            {
                string dapperProcedure = dapperService.DeleteDapperProcedure(i);
            }
            dapper.Stop();
            times[0] = dapper.Elapsed.ToString();

            ado.Start();
            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string adoProcedure = adoService.DeleteAdoProcedure(i);
            }
            ado.Stop();
            times[1] = ado.Elapsed.ToString();

            efCore.Start();
            InsertToDelete();
            for (int i = 1; i <= 1000; i++)
            {
                string efCoreProcedure = eFCoreService.DeleteEFCoreProcedure(i);
            }
            efCore.Stop();
            times[2] = efCore.Elapsed.ToString();

            return times;
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
