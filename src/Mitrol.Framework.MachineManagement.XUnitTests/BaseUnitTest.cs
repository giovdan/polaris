namespace XUnitTests
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models.Core;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Models;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Repositories;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using Mitrol.Framework.MachineManagement.Application.Mappings;
    using Mitrol.Framework.Domain.Core.Extensions;

    public class BaseUnitTest
    {
        public IConfiguration Configuration { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }
        internal Dictionary<EntityTypeEnum, IEnumerable<AttributeDefinition>> AttributeDefinitionsDictionary { get; set; }
        protected CancellationTokenSource CancellationTokenSource { get; private set; }

        public string ConnectionString
        {
            get
            {
                var mySQLSection = new MySQLSection();
                DomainExtensions.GetConfiguration(isTest: true).GetSection("MySQL").Bind(mySQLSection);
                return
                 ($"Server={mySQLSection.Server};port={mySQLSection.Port};Database=machine;Uid={mySQLSection.Username};Pwd={SimpleStringCipher.Instance.Decrypt(mySQLSection.Password)}");

            }
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IServiceFactory, ServiceFactory>();
            services.AddScoped<IUnitOfWorkFactory<IMachineManagentDatabaseContext>, Mitrol.Framework.MachineManagement.Data.MySQL.Models.UnitOfWorkFactory>();
            services.AddTransient<IMachineManagentDatabaseContext, MachineManagementDatabaseContext>();
            services.AddTransient<IDatabaseContext, MachineManagementDatabaseContext>();
            services.AddTransient<IUnitOfWork<IMachineManagentDatabaseContext>, Mitrol.Framework.MachineManagement.Data.MySQL.Models.UnitOfWork>();
            services.AddScoped<IDatabaseContextFactory, DatabaseContextFactory>();
            services.AddScoped<IEntityRepository, EntityRepostiory>();
            services.AddScoped<IEntityLinkRepository, EntityLinkRepository>();
            services.AddScoped<IAttributeDefinitionRepository, AttributeDefinitionRepository>();
            services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();

            services.AddAutoMapper(cfg =>
            {
                cfg.DisableConstructorMapping();
                cfg.AddProfile(new ServiceProfile());
            });

            services.AddDbContext<MachineManagementDatabaseContext>(options =>
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
            CancellationTokenSource = new CancellationTokenSource();
            AttributeDefinitionsDictionary = new Dictionary<EntityTypeEnum, IEnumerable<AttributeDefinition>>();
            BuildConfiguration();
        }

        protected void BuildConfiguration(
            string jsonAppSettings = "UnitTests"
            , string environmentName = "Development"
            , string externalConfigFile = null)
        {
            string directory = CoreExtensions.GetConfigDirectory()?.FullName ?? Directory.GetCurrentDirectory();
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
