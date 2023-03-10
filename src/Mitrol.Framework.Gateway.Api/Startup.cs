namespace Mitrol.Framework.Gateway.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Core.Helpers;
    using Mitrol.Framework.Domain.Core.Middlewares;
    //using Mitrol.Framework.Gateway.Api.Hubs;
    //using Mitrol.Framework.Ioc;
    using Newtonsoft.Json;
    using Ocelot.DependencyInjection;
    using Ocelot.Middleware;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            CoreExtensions.SetSiteCorsPolicy(services);
            WebApiHelper.SetAuthenticationSpecs(services);

            //services.AddSignalR(config =>
            //{
            //    config.EnableDetailedErrors = true;
            //})
            //.AddNewtonsoftJsonProtocol(options =>
            //{
            //    options.PayloadSerializerSettings = new JsonSerializerSettings
            //    {
            //        TypeNameHandling = TypeNameHandling.Auto,
            //        DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            //    };
            //});

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                //options.Filters.Add<MustLog>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });

            services.AddOcelot(WebApiHelper.Instance.Configuration);
            services.AddSwaggerForOcelot(WebApiHelper.Instance.Configuration);
            services.AddSwaggerGen();
            //DependencyContainer.RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            app.UseWebSockets();
            app.UseCors(CoreExtensions.CORS_POLICIES);
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            //app.UseEndpoints(c =>
            //{
            //    c.MapHub<EventHub>("/events");
            //});
            app.UseMiddleware<EventLogMiddleware>();
            app.UseMvc();
            app.UseSwagger();
            app.UseStaticFiles();
            app.UseSwaggerForOcelotUI();
            app.UseOcelot().Wait();
        }
    }
}