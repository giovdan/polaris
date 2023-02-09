using System;
using System.Collections.Generic;

namespace RepoDbVsEF.Domain.Interfaces
{
    public interface IUserSession
    {
        /// <summary>
        /// Gets current User or null.
        /// It can be null if no user logged in.
        /// </summary>
        string Username { get; }


        /// <summary>
        /// SessionId generated when user logged in with success
        /// </summary>
        string SessionId { get; }

        string AccessToken { get; set; }
        DateTime AccessTokenIssueDate { get; set; }
        DateTime? AccessTokenExpirationDate { get; set; }

        string RefreshToken { get; set; }
        DateTime? RefreshTokenExpirationDate { get; set; }

        /// <summary>
        /// Culture of the Logged User
        /// </summary>
        string Culture { get; }
        string Group { get; }
        IEnumerable<string> Permissions { get; }
        long UserId { get; set; }
        string MachineName { get; set; }
        bool IsSystemUser { get; set; }
        string FullName { get; }
    }
}
