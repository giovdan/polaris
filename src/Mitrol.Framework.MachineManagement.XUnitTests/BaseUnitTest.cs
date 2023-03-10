namespace XUnitTests
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models.Core;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    
    public class BaseUnitTest
    {
        public IConfiguration Configuration { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }
        public string ConnectionString
        {
            get
            {
                var mySQLSection = new MySQLSection();
                DomainExtensions.GetConfiguration().GetSection("MySQL").Bind(mySQLSection);
                return
                 ($"Server={mySQLSection.Server};port={mySQLSection.Port};Database={mySQLSection.Database};Uid={mySQLSection.Username};Pwd={SimpleStringCipher.Instance.Decrypt(mySQLSection.Password)}");

            }
        }

        public void RegisterServices(IServiceCollection services, bool isRepoDb = true)
        {
            services.AddSingleton<IServiceFactory, ServiceFactory>();

            services.AddScoped<IUnitOfWorkFactory<IEFDatabaseContext>, EFUnitOfWorkFactory>();
            services.AddTransient<IEFDatabaseContext, EFDatabaseContext>();
            services.AddTransient<IDatabaseContext, EFDatabaseContext>();
            services.AddTransient<IUnitOfWork<IEFDatabaseContext>, EFUnitOfWork>();
            services.AddScoped<IDatabaseContextFactory, DatabaseContextFactory>();
            services.AddDbContext<EFDatabaseContext>(options =>
            {
                options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
            });

            services.AddTransient(serviceProvider => Configuration);
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();

            ServiceProvider = new AutofacServiceProvider(container);
        }

        #region < Start / Stop DbServer >
        //private Result StartDBServer()
        //{
        //    var startupCommand = StartPolarisConfiguration.MySQLSection.StartUp;
        //    var serverPort = StartPolarisConfiguration.MySQLSection.Port;

        //    (var fileName, var arguments) = startupCommand.SplitPathAndArgs();
        //    var fullpath = Path.Combine(DomainExtensions.GetStartUpDirectoryInfo().FullName, fileName);

        //    // Se in sessione di debug e il database è già avviato non devo lanciarlo
        //    if (Debugger.IsAttached)
        //    {
        //        var existingProcess = Process.GetProcessesByName(Path.GetFileName(fullpath)).Any();
        //        if (existingProcess) return Result.Ok();
        //    }

        //    using var process = Common.StartProcess(fullpath, string.Join(" ", arguments, $" --port={serverPort}"), noWindow: !Debugger.IsAttached
        //                    , onExit: (s, e) =>
        //                    {
        //                        //Program.s_frmMain.InvokeIfRequired(() => Program.s_frmMain.Close(force: true));
        //                    });
        //    var isAlive = !process.WaitForExit(2000);
        //    Program.Log4Net.Debug(isAlive ? "DB Server started" : "Cannot start database server");
        //    return isAlive ? Result.Ok() : Result.Fail("Can't start database server");
        //}

        //private void StopDBServer()
        //{
        //    OnProgressChanged($"Stopping database server", SetupResultEnum.Success);

        //    var shutdownCommand = StartPolarisConfiguration.MySQLSection.Shutdown;
        //    var serverPort = StartPolarisConfiguration.MySQLSection.Port;

        //    (var fileName, var arguments) = shutdownCommand.SplitPathAndArgs();
        //    var fullpath = Path.Combine(DomainExtensions.GetStartUpDirectoryInfo().FullName, fileName);

        //    var dbServerProcess = Process.GetProcessesByName("mysqld").SingleOrDefault();

        //    var process = Common.StartProcess(fullpath, string.Join(" ", arguments, $" --port={serverPort}"), noWindow: !Debugger.IsAttached);
        //    process.WaitForExit();

        //    dbServerProcess?.WaitForExit();
        //}
        #endregion

        public BaseUnitTest()
        {
            BuildConfiguration();
        }

        protected void BuildConfiguration(
            string jsonAppSettings = "UnitTests"
            , string environmentName = "Development"
            , string externalConfigFile = null)
        {
            string directory = DomainExtensions.GetConfigDirectory()?.FullName ?? Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile($"{jsonAppSettings}.{environmentName}.json", optional: false, reloadOnChange: true);
            if (!string.IsNullOrEmpty(externalConfigFile))
                builder = builder.AddJsonFile($"{externalConfigFile}.{environmentName}.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public static IEnumerable<object[]> GetRecordCounts()
        {
            foreach (var value in new[] { 0, 1, 5, 10, 100, 500, 1000, 5000, 10000 })
                yield return new object[] { value };
        }
    }
}
