namespace RepoDbVsEF.Api
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.WindowsServices;
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Core.Helpers;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    class Program
    {
        private static bool _isService;

        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

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

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var baseDirectory = DomainExtensions.GetStartUpDirectoryInfo().ToString();
            var exceptionLogFilename = $"{Process.GetCurrentProcess().ProcessName}.exception.log";

            File.WriteAllText(baseDirectory is null ? exceptionLogFilename : Path.Combine(baseDirectory, exceptionLogFilename),
                              e.ExceptionObject.ToString());
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

            //Get Environment Variable for WEB API. Default is Development
            var enviroment = Environment.GetEnvironmentVariable("MITROLWEBAPI_ENV", EnvironmentVariableTarget.Machine) ?? "Development";
            WebApiHelper.Initialize("Machine", enviroment);
            var applicationUrls = WebApiHelper.Instance.GetConfigurationSection("ApplicationUrls").Value;

            return WebHost.CreateDefaultBuilder(webHostArgs)
                    .UseUrls(applicationUrls)
                    .UseEnvironment(enviroment)
                    .UseStartup<Startup>()
                    .Build();
        }
    }
}
