namespace Mitrol.Framework.Domain.Core.Attributes
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain.Bus.Events;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.SignalR;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class MustLog : ActionFilterAttribute
    {
        public EventTypeEnum EventType { get; set; }
        public EventContextEnum EventContext { get; set; }
        public string ValidationKey { get; set; }
        private string _eventInput;


        private UserSession GetUserSession(ResultExecutingContext context)
        {
            if (EventType == EventTypeEnum.Login)
            {
                UserSession result = NullUserSession.Instance;
                if (context.Result is OkObjectResult)
                {
                    //Recupera la sessione se è Ok altrimenti ritorna NullUserSession.Instance
                    result = (((context.Result as OkObjectResult).Value) as ResponseModel<UserSession>)?.Result 
                            ?? NullUserSession.Instance;
                    //Se il log non è andato a buon fine allora recupero il Machine Name dall'input
                    if (result is NullUserSession)
                    {
                        var loginRequest = JsonConvert.DeserializeObject<LoginRequest>(_eventInput);
                        result.MachineName = loginRequest.MachineName;
                    }
                }
                return result;
            }
            string sessionValue = context.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.UserData)?.Value;
            return !string.IsNullOrEmpty(sessionValue) ? JsonConvert.DeserializeObject<UserSession>(sessionValue) : NullUserSession.Instance;
        }

        public MustLog()
        {
            _eventInput = string.Empty;
            ValidationKey = $"{EventType.ToString().ToUpper()}_{EventContext.ToString().ToUpper()}";
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (Enum.IsDefined(typeof(EventTypeEnum), EventType))
            {
                var arguments = context.ActionArguments;
                StringBuilder eventArguments = new StringBuilder();

                if (arguments.Any())
                {
                    arguments.ForEach(a =>
                    {
                        eventArguments.AppendLine(JsonConvert.SerializeObject(a.Value));
                    });

                    _eventInput = eventArguments.ToString();
                }
            }
            return base.OnActionExecutionAsync(context, next);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (Enum.IsDefined(typeof(EventTypeEnum), EventType))
            {
                //Get Event Bus
                var logEventPublisher = context.HttpContext.RequestServices.GetRequiredService<IEventLogHubClient>();
                //Get Current Session
                var session = GetUserSession(context);

                try
                {
                    if (!context.ModelState.IsValid)
                    {
                        context.Result = new BadRequestObjectResult(ErrorCodesEnum.ERR_GEN001.ToErrorResponseBody());
                    }
                    else
                    {
                        string eventOutput = null;
                        
                        if (context.Result is ObjectResult objectResult)
                        {
                            eventOutput = JsonConvert.SerializeObject(objectResult.Value);
                        }

                        logEventPublisher.WriteLogEvent(new WriteLogEvent()
                        {
                            EventType = EventType,
                            EventContext = EventContext,
                            Method = context.ActionDescriptor.AttributeRouteInfo.Template ?? context.ActionDescriptor.DisplayName,
                            MachineName = session.MachineName,
                            LoggedUser = session.Username,
                            SessionId = session.SessionId,
                            EventInput = _eventInput,
                            EventOutput = eventOutput
                        });
                    }
                   
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return base.OnResultExecutionAsync(context, next);
        }
    }
}