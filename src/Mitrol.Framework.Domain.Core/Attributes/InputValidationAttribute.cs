namespace Mitrol.Framework.Domain.Core.Attributes
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Attributo che gestisce la validazione degli input delle Web Api
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class InputValidationAttribute : ActionFilterAttribute
    {
        public string ValidationKey { get; set; }
        public InputValidationAttribute()
        {
            ValidationKey = string.Empty;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                List<ErrorDetail> errorDetails = new List<ErrorDetail>();
                foreach (var key in context.ModelState.Keys)
                {
                    if (context.ModelState[key].Errors.Any())
                    {
                        errorDetails.Add(new ErrorDetail(key, context.ModelState[key].Errors.Select(x => $"{key.ToUpper()}_{ValidationKey}_INVALID").ToList()));
                    }
                }
                context.Result = new OkObjectResult(new ResponseModel<string>()
                {
                    ResponseType = ResponseTypeEnum.BadRequest,
                    ErrorDetails = errorDetails
                });
            }

            return base.OnResultExecutionAsync(context, next);
        }
    }
}