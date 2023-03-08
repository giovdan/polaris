namespace Mitrol.Framework.Domain.Remoting.Services.Extensions
{
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Remoting.Services.WebApi;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public static class RestClientExtensions
    {
        public static List<ErrorDetail> ToErrorDetails(this Exception ex)
        {
            return new List<ErrorDetail>() { new ErrorDetail(ex.InnerException?.Message ?? ex.Message) };
        }

        public static Result<T> ToResult<T>(this WebApiResponse webApiResponse, T value)
        {
            return FluentSwitch.On<HttpStatusCode, Result<T>>(webApiResponse.StatusCode)
                .Case(HttpStatusCode.OK, _ => webApiResponse.Errors != null && webApiResponse.Errors.Any()
                    ? Result.Fail<T>(webApiResponse.Errors)
                    : Result.Ok(value))
                .Default(statusCode => webApiResponse.Errors.Any() ? Result.Fail<T>(webApiResponse.Errors) : Result.Fail<T>(webApiResponse.StatusCode.ToString()));
            ;
        }

        public static Result ToResult(this WebApiResponse webApiResponse)
        {
            return FluentSwitch.On<HttpStatusCode, Result>(webApiResponse.StatusCode)
                .Case(HttpStatusCode.OK, _ => webApiResponse.Errors != null && webApiResponse.Errors.Any()
                    ? Result.Fail(webApiResponse.Errors)
                    : Result.Ok())
                .Default(statusCode => webApiResponse.Errors.Any() ? Result.Fail(webApiResponse.Errors) : Result.Fail(webApiResponse.StatusCode.ToString()));
        }
    }
}