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
            var bar = serviceProvider.GetService<IMainService>();

            var times = bar.GetSync();
            //dapperService.SelectDapperSync();
            Console.WriteLine("DapperSync: {0}\nAdoSync: {1}\nEntityFramework Core: {2}", times[0], times[1], times[2]);
            Console.ReadLine();
        }
    }
}
