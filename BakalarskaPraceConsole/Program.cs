using AccessFacade.Business;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace BakalarskaPraceConsole
{
    public class Program
    {
        private static string connection = "Server=(localdb)\\mssqllocaldb;Database=BakalarskaPrace;Trusted_Connection=True;MultipleActiveResultSets=true";

        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddOptions()
               .AddSingleton<IMainService, MainService>()
               .AddModuleAccessFacade(o => o.connectionString = connection)
               .BuildServiceProvider();

            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

            var tests = serviceProvider.GetService<IMainService>();
            tests.Initialize();

            //Select(tests);
            //SelectAsync(tests).Wait();
            //Update(tests);
            //UpdateAsync(tests).Wait();
            //Insert(tests);
            //InsertAsync(tests).Wait();
            //Delete(tests);
            //DeleteAsync(tests).Wait();


            Console.ReadLine();
        }

        public static void Write(string[] times)
        {
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}\n", times[0], times[1], times[2]);
        }

        public static void Select(IMainService mainService)
        {
            string[] times;

            Console.WriteLine("------------Select------------");
            times = mainService.GetSync();
            Write(times);

            Console.WriteLine("\n------------SelectProcedure------------");
            times = mainService.GetProc();
            Write(times);
        }

        public static async Task SelectAsync(IMainService mainService)
        {
            string[] times;

            Console.WriteLine("\n------------SelectAsync------------");
            times = await mainService.GetAsync();
            Write(times);
        }

        public static void Insert(IMainService mainService)
        {
            string[] times;

            Console.WriteLine("\n------------Insert------------");
            times = mainService.InsertSync();
            Write(times);

            Console.WriteLine("\n------------InsertProcedure------------");
            times = mainService.InsertProc();
            Write(times);
        }

        public static async Task InsertAsync(IMainService mainService)
        {
            string[] times;

            Console.WriteLine("\n------------InsertAsync------------");
            times = await mainService.InsertAsync();
            Write(times);
        }

        public static void Update(IMainService mainService)
        {
            string[] times;

            Console.WriteLine("\n------------Update------------");
            times = mainService.UpdateSync();
            Write(times);

            Console.WriteLine("\n------------UpdateProcedure------------");
            times = mainService.UpdateProc();
            Write(times);
        }

        public static async Task UpdateAsync(IMainService mainService)
        {
            string[] times;

            Console.WriteLine("\n------------UpdateAsync------------");
            times = await mainService.UpdateAsync();
            Write(times);
        }

        public static void Delete(IMainService mainService)
        {
            string[] times;
            Console.WriteLine("\n------------Delete------------");
            times = mainService.DeleteSync();
            Write(times);

            Console.WriteLine("\n------------DeleteProcedure------------");
            times = mainService.DeleteProc();
            Write(times);
        }

        public static async Task DeleteAsync(IMainService mainService)
        {
            string[] times;

            Console.WriteLine("\n------------DeleteAsync------------");
            times = await mainService.DeleteAsync();
            Write(times);
        }
    }
}
