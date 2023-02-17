using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using RepoDbVsEF.Domain.Helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RepoDbVsEF.GraphQL
{
    class Program
    {
        private static bool _isService { get; set; }

        static void Main(string[] args)
        {
            _isService = !(Debugger.IsAttached || args.Contains("--console"));
            using var host = BuildWebHost(args);

            if (_isService)
            {
                host.RunAsService();
                Console.WriteLine("Service started");
            }
            else
            {
                host.Run();
            }
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            var pathToContentRoot = Directory.GetCurrentDirectory();
            var webHostArgs = args.Where(arg => arg != "--console").ToArray();

            if (_isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);
                Directory.SetCurrentDirectory(pathToContentRoot);
            }

            var enviroment = "Development";
            WebApiHelper.Initialize("GraphQL", enviroment);
            var applicationUrls = WebApiHelper.Instance.GetConfigurationSection("ApplicationUrls").Value;

            return WebHost.CreateDefaultBuilder(webHostArgs)
                    //.ConfigureLogging((hostingContext, logging) =>
                    //{
                    //    logging.AddConfiguration(WebApiHelper.Instance.Configuration.GetSection("Logging"));
                    //    logging.AddConsole();
                    //})
                    .UseContentRoot(pathToContentRoot)
                    .UseUrls(applicationUrls)
                    .UseEnvironment(enviroment)
                    .UseStartup<Startup>()
                    .Build();
        }

    }
}
