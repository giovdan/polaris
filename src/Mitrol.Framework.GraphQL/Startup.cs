using GraphQL.Server.Ui.Voyager;

namespace Mitrol.Framework.GraphQL
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models.Core;
    using Mitrol.Framework.GraphQL.Core;
    using Mitrol.Framework.GraphQL.Core.Types;
    using System;
    using HotChocolate.Types.Descriptors;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Models;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Repositories;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Services;
    using Mitrol.Framework.MachineManagement.Application.Mappings;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Core.Helpers;

    public class Startup
    {
        private static IContainer s_container;

        private IServiceProvider RegisterService(IServiceCollection services)
        {
            services.AddSingleton<INamingConventions, EnumNamingConvention>();

            

            services.AddTransient<IUnitOfWork<IEFDatabaseContext>, EFUnitOfWork>();


            services.AddScoped<IEntityRepository, EntityRepostiory>();
            services.AddScoped<IAttributeDefinitionRepository, AttributeDefinitionRepository>();
            services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();
            services.AddScoped<IEntityService, EntityService>();

            //Scoped
            services.AddScoped<IUnitOfWorkFactory<IEFDatabaseContext>, EFUnitOfWorkFactory>();
            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped<IDatabaseContextFactory, DatabaseContextFactory>();

            //Transient
            services.AddTransient<IDatabaseContext, EFDatabaseContext>();
            services.AddTransient<IUnitOfWork<IEFDatabaseContext>, EFUnitOfWork>();
            services.AddTransient(provider => provider.GetService<IDatabaseContext>() as IEFDatabaseContext);

            //services.AddTransient<IDatabaseContext, EFDatabaseContext>();
            var containerBuilder = new ContainerBuilder();
            s_container = containerBuilder.Build();
            return new AutofacServiceProvider(s_container);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.DisableConstructorMapping();
                cfg.AddProfile(new ServiceProfile());
            });


            RegisterService(services);

            services.AddDbContext<EFDatabaseContext>(options =>
            {
                string connectionString = WebApiHelper.Instance
                                .GetConnectionString<EFDatabaseContext>("MySQL");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

                options.EnableSensitiveDataLogging();
            }, contextLifetime: ServiceLifetime.Transient);

            services.AddMvc()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    });

            services
                .AddGraphQLServer()
                .AddConvention<INamingConventions, EnumNamingConvention>()
                .RegisterService<IEntityService>()
                .AddSorting()
                .AddType<AttributeValueType>()
                .AddQueryType<Query>()
                .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
                .AddMutationType<Mutation>();

            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager("graphql-voyager", options: new VoyagerOptions
            {
                GraphQLEndPoint = "/"
            });
        }
    }
}
