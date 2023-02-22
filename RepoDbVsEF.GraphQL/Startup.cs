namespace RepoDbVsEF.GraphQL
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using RepoDbVsEF.Application.Interfaces;
    using RepoDbVsEF.Application.Mappings;
    using RepoDbVsEF.Application.Services;
    using RepoDbVsEF.Domain.Helpers;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models.Core;
    using RepoDbVsEF.EF.Data.Interfaces;
    using RepoDbVsEF.EF.Data.Models;
    using RepoDbVsEF.EF.Data.Repositories;
    using RepoDbVsEF.GraphQL.Core;
    
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.DisableConstructorMapping();
                cfg.AddProfile(new ServiceProfile());
            });
            

            services.AddScoped<IUnitOfWorkFactory<IEFDatabaseContext>, EFUnitOfWorkFactory>();
            services.AddTransient<IDatabaseContext, EFDatabaseContext>();
            services.AddTransient<IEFDatabaseContext, EFDatabaseContext>();
            services.AddTransient<IUnitOfWork<IEFDatabaseContext>, EFUnitOfWork>();
            services.AddScoped<IDatabaseContextFactory, DatabaseContextFactory>();
            services.AddDbContext<EFDatabaseContext>(options =>
            {
                string connectionString = WebApiHelper.Instance
                                .GetConnectionString<EFDatabaseContext>("db_polaris");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

                options.EnableSensitiveDataLogging();
            }, contextLifetime: ServiceLifetime.Transient);

            services.AddSingleton<IServiceFactory, ServiceFactory>();
            services.AddScoped<IEntityService, EntityService>();
            services.AddTransient<IEFEntityRepository, EFEntityRepostiory>();
            services.AddTransient<IEFAttributeDefinitionRepository, EFAttributeDefinitionRepository>();
            services.AddTransient<IEFAttributeValueRepository, EFAttributeValueRepository>();

            services.AddMvc()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    });

            services
                .AddGraphQLServer()
                .RegisterService<IEntityService>()
                .AddQueryType<Query>();

            services.AddControllers()
                .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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

            //app.UseGraphQLVoyager("graphql-voyager", options: new VoyagerOptions
            //{
            //    GraphQLEndPoint = "/"
            //});
        }
    }
}
