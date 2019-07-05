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
        private static string connection = "Server=localhost;Database=BakalarskaPrace;Trusted_Connection=True;";
        //"Server=localhost;Database=BakalarskaPrace;Trusted_Connection=True;"
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

            Console.WriteLine("Který test si přeješ spustit\n1 - Select\n2 - SelectAsync\n3 - Update\n4 - UpdateAsync\n5 - Insert\n6 - InsertAsync\n7 - Delete\n8 - DeleteAsync");
            int choosedTest;
            int.TryParse(Console.ReadLine(), out choosedTest);


            switch (choosedTest)
            {
                case 1:
                    Select(tests);
                    break;
                case 2:
                    SelectAsync(tests).Wait();
                    break;
                case 3:
                    Update(tests);
                    break;
                case 4:
                    UpdateAsync(tests).Wait();
                    break;
                case 5:
                    Insert(tests);
                    break;
                case 6:
                    InsertAsync(tests).Wait();
                    break;
                case 7:
                    Delete(tests);
                    break;
                case 8:
                    DeleteAsync(tests).Wait();
                    break;
                default:
                    Console.WriteLine("Špatně vybráno");
                    break;
            }
            Console.ReadLine();
        }

        public static void Write(string[] times)
        {
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}\n", times[0], times[1], times[2]);
        }

        public static void Select(IMainService mainService)
        {
            string[] times;

            Console.WriteLine("Který test chceš přesně\n1 - Synchronize\n2 - Procedure");
            int choosedMethod;
            int.TryParse(Console.ReadLine(), out choosedMethod);
            switch(choosedMethod)
            {
                case 1:
                    Console.WriteLine("------------Select------------");
                    times = mainService.GetSync();
                    break;
                case 2:
                    Console.WriteLine("\n------------SelectProcedure------------");
                    times = mainService.GetProc();
                    break;
                default:
                    times = null;
                    break;
            }            
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
            Console.WriteLine("Který test chceš přesně\n1 - Synchronize\n2 - Procedure");
            int choosedMethod;
            int.TryParse(Console.ReadLine(), out choosedMethod);
            switch (choosedMethod)
            {
                case 1:
                    Console.WriteLine("\n------------Insert------------");
                    times = mainService.InsertSync();
                    break;
                case 2:
                    Console.WriteLine("\n------------InsertProcedure------------");
                    times = mainService.InsertProc();
                    break;
                default:
                    times = null;
                    break;
            }            
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
            Console.WriteLine("Který test chceš přesně\n1 - Synchronize\n2 - Procedure");
            int choosedMethod;
            int.TryParse(Console.ReadLine(), out choosedMethod);
            switch (choosedMethod)
            {
                case 1:
                    Console.WriteLine("\n------------Update------------");
                    times = mainService.UpdateSync();
                    break;
                case 2:
                    Console.WriteLine("\n------------UpdateProcedure------------");
                    times = mainService.UpdateProc();
                    break;
                default:
                    times = null;
                    break;
            }
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
            Console.WriteLine("Který test chceš přesně\n1 - Synchronize\n2 - Procedure");
            int choosedMethod;
            int.TryParse(Console.ReadLine(), out choosedMethod);
            switch (choosedMethod)
            {
                case 1:
                    Console.WriteLine("\n------------Delete------------");
                    times = mainService.DeleteSync();
                    break;
                case 2:
                    Console.WriteLine("\n------------DeleteProcedure------------");
                    times = mainService.DeleteProc();
                    break;
                default:
                    times = null;
                    break;
            }
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
