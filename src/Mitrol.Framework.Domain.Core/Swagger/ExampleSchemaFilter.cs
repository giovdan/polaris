namespace Mitrol.Framework.Domain.Core.Swagger
{
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class ExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(System.Collections.Generic.Dictionary<string, string>))
            {
                schema.Example = new OpenApiObject
                {
                    ["error"] = new OpenApiString(ErrorCodesEnum.ERR_GEN007.ToString()),
                    ["{fieldName}"] = new OpenApiString("{ErrorCodesEnum}")
                };
            }
            else if (context.Type == typeof(LoginRequest))
            {
                ///     POST api/v1/login
                ///     {
                ///         "UserName": "admin",
                ///         "password": "mitrol2021",
                ///         "machineName": "GEMINI HPE",
                ///         "grantType": "Password"
                ///     }
                schema.Example = new OpenApiObject()
                {
                    ["UserName"] = new OpenApiString("user"),
                    ["Password"] = new OpenApiPassword("**********"),
                    ["MachineName"] = new OpenApiString("Machine name"),
                    ["GrantType"] = new OpenApiString("Password"),
                };
            }
        }
    }
}