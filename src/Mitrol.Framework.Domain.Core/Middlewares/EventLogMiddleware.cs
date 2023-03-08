namespace Mitrol.Framework.Domain.Core.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain.Bus.Events;
    using Mitrol.Framework.Domain.Core.CustomExceptions;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.SignalR;
    using Newtonsoft.Json;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class EventLogMiddleware
    {
        private readonly RequestDelegate _next;

        private UserSession GetUserSession(HttpContext context)
        {
            if (context.User == null)
                return NullUserSession.Instance;

            var sessionValue = context.User.FindFirst(x => x.Type == ClaimTypes.UserData)?.Value;

            return !string.IsNullOrEmpty(sessionValue) ? JsonConvert.DeserializeObject<UserSession>(sessionValue)
                                    : NullUserSession.Instance;
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, UserSession session)
        {
            try
            {
                context.Response.ContentType = "application/json";
                
                string message;

                //Assegna contesto e messaggio di errore
                if (exception is AuthorizationException authorizationException)
                {
                    message = exception.InnerException?.Message ?? exception.Message;
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    session = authorizationException.LastestSession;
                }
                else if (exception is BusinessValidationException businessValidationException)
                {
                    message = businessValidationException.ErrorDetails.ToErrorString();
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                }
                else
                {
                    message = exception.InnerException?.Message ?? exception.Message;
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }

                var eventLogHubClient = context.RequestServices.GetRequiredService<IEventLogHubClient>();
                eventLogHubClient.WriteLogEvent(new WriteLogEvent()
                {
                    EventType = EventTypeEnum.Error,
                    EventContext = EventContextEnum.InternalServerError,
                    Method = context.Request.Path,
                    Message = message,
                    LoggedUser = session.Username,
                    MachineName = session.MachineName,
                    SessionId = session.SessionId,
                });
            }
            catch (Exception ex)
            {
                return context.Response.WriteAsync(
                    JsonConvert.SerializeObject(ex.ToErrorResponseBody(exception))
                );
            }

            return context.Response.WriteAsync(
                JsonConvert.SerializeObject(exception.ToErrorResponseBody())
            );
        }

        public EventLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var session = GetUserSession(httpContext);
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, session);
            }
        }
    }
}