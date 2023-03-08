namespace Mitrol.Framework.Domain.Core.CustomExceptions
{
    using Mitrol.Framework.Domain.Models;
    using System;

    public class AuthorizationException : Exception
    {
        public UserSession LastestSession { get; private set; }
        public AuthorizationException(UserSession userSession)
        {
            LastestSession = userSession;
        }
    }
}