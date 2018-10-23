using AccessFacade.Business;
using Microsoft.Extensions.DependencyInjection;
using System;

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

            //var bar = serviceProvider.GetService<DapperService>();
            var tests = serviceProvider.GetService<IMainService>();

            //Tests(tests);
            TestsAsync(tests);

            Console.ReadLine();
        }

        public static void Tests(IMainService mainService)
        {
            string[] times;

            Console.WriteLine("------------Select------------");
            times = mainService.GetSync();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}\n", times[0], times[1], times[2]);
            times = mainService.GetProc();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}", times[0], times[1], times[2]);

            Console.WriteLine("\n------------Update------------");
            times = mainService.UpdateSync();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}\n", times[0], times[1], times[2]);
            times = mainService.UpdateProc();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}", times[0], times[1], times[2]);

            Console.WriteLine("\n------------Insert------------");
            times = mainService.InsertSync();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}\n", times[0], times[1], times[2]);
            times = mainService.InsertProc();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}", times[0], times[1], times[2]);

            Console.WriteLine("\n------------Delete------------");
            times = mainService.DeleteSync();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}\n", times[0], times[1], times[2]);
            times = mainService.DeleteProc();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}", times[0], times[1], times[2]);
        }

        public static async void TestsAsync(IMainService mainService)
        {
            string[] times;

            Console.WriteLine("\n------------SelectAsync------------");
            times = await mainService.GetAsync();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}", times[0], times[1], times[2]);

            Console.WriteLine("\n------------UpdateAsync------------");
            times = await mainService.UpdateAsync();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}", times[0], times[1], times[2]);

            Console.WriteLine("\n------------InsertAsync------------");
            times = await mainService.InsertAsync();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}", times[0], times[1], times[2]);

            Console.WriteLine("------------DeleteAsync------------");
            times = await mainService.DeleteAsync();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}", times[0], times[1], times[2]);
        }
    }
}
