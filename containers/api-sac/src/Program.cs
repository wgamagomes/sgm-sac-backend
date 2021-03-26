using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SGM.SAC.Api
{
    public class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder
                        .UseStartup<Startup>());


    }
}
