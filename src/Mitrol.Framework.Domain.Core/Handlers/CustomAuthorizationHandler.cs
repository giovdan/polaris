namespace Mitrol.Framework.AuthServer.Api.Handlers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Net.Http.Headers;
    using Mitrol.Framework.Domain.Core.CustomExceptions;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class CustomAuthorizationHandler : AuthorizationHandler<CustomAuthorizationRequirement>
    {
        #region Private Members

        /// <summary>
        /// Check Permission for logged User
        /// </summary>
        /// <param name="requirement"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        private static bool CheckPermission(CustomAuthorizationRequirement requirement, string userData)
        {
            if (string.IsNullOrEmpty(userData))
                return false;

            //If List Of Claims isn't specified, user is authorized
            if (!requirement.ListOfClaims.Any())
                return true;

            //false for default
            bool isSucceded = false;
            var session = JsonConvert.DeserializeObject<UserSession>(userData);
            //Check if session is expired
            if (session.AccessTokenExpirationDate.Value >= DateTime.UtcNow)
            {
                requirement.CurrentSession = session;
                foreach (var r in requirement.ListOfClaims)
                {
                    //If requirements is satisfied than authorization is true
                    if (session.Permissions.Contains(r))
                    {
                        isSucceded = true;
                        break;
                    }
                }
            }
            return isSucceded;
        }

        #endregion Private Members

        /// <summary>
        /// Override HandleRequirementAsync for check Authorization on local or remote
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context
            , CustomAuthorizationRequirement requirement)
        {
            return Task.Factory.StartNew(() =>
            {
                bool isSucceded = false;
                var authContext = (AuthorizationFilterContext)context.Resource;
                var currentRequest = authContext.HttpContext.Request;
                var token = currentRequest.Headers[HeaderNames.Authorization].SingleOrDefault().Replace("Bearer", "").Trim();
                JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
                isSucceded = CheckPermission(requirement, jwt.Claims.SingleOrDefault(x => x.Type == ClaimTypes.UserData).Value);
                if (isSucceded)
                    context.Succeed(requirement);
                else
                {
                    string userData = jwt.Claims
                        .SingleOrDefault(x => x.Type == ClaimTypes.UserData)?.Value;

                    if (!string.IsNullOrEmpty(userData))
                    {
                        throw new AuthorizationException(JsonConvert.DeserializeObject<UserSession>(userData));
                    }

                    throw new AuthorizationException(NullUserSession.Instance);


                }

                return 0;
            });
        }
    }
}