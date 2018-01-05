using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SwaggerTester.Web
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            CreateHost(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateHost(params string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            return new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
        }
    }
}
