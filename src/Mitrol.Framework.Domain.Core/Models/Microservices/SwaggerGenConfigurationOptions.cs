namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class SwaggerGenConfigurationOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private string OpenApiTitle { get;  }

        private readonly IApiVersionDescriptionProvider _provider;
        public SwaggerGenConfigurationOptions(IApiVersionDescriptionProvider provider, string openApiTitle)
        {
            this._provider = provider;
            OpenApiTitle = openApiTitle;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo
                    {
                        Title = OpenApiTitle,
                        Version = description.ApiVersion.ToString()
                    });
            }
        }
    }
}
