namespace Mitrol.Framework.Domain.Core.Swagger
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.Filters;
    using System;
    using System.IO;

    public static class StartUpExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services, OpenApiInfo apiInfo)
        {
            services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<ExampleSchemaFilter>();

                //options.SwaggerDoc("v1", new OpenApiInfo
                //{
                //    Version = "v1",
                //    Title = "Sample API",
                //    Description = "Api by Sample platform.",
                //    TermsOfService = new Uri("https://sample.com/terms"),
                //    Contact = new OpenApiContact
                //    {
                //        Name = "Sample Technology Team",
                //        Email = "tech@sample.com",
                //        Url = new Uri("https://sample.com/contact-us"),
                //    }
                //});

                // Define swagger's general informations for this service
                c.SwaggerDoc(apiInfo.Version, apiInfo);

                c.OperationFilter<AddResponseHeadersFilter>(); // [SwaggerResponseHeader]

                // Retrieve the xml docs that'll drive the swagger UI
                foreach (var xmlFile in new[]
                {
                    $"{System.Diagnostics.Process.GetCurrentProcess().ProcessName }.xml",
                    $"Mitrol.Framework.Domain.xml"
                })
                {
                    // Include them in swagger configuration
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
                }

                // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization
                // or use the generic method, e.g. c.OperationFilter<AppendAuthorizeToSummaryOperationFilter<MyCustomAttribute>>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // add Security information to each operation for OAuth2
                // or use the generic method, e.g. c.OperationFilter<SecurityRequirementsOperationFilter<MyCustomAttribute>>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // if you're using the SecurityRequirementsOperationFilter, you also need to tell Swashbuckle you're using OAuth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Please, Copy&Paste your access token here.",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Scheme = "Bearer",
                    Type = SecuritySchemeType.Http,
                });
            });
            
            // order is vital, this *must* be called *after* AddNewtonsoftJson()
            //services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
