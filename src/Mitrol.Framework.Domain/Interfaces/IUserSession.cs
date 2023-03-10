namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Collections.Generic;

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
        GroupEnum Group { get; }
        IEnumerable<string> Permissions { get; }
        long UserId { get; set; }
        string MachineName { get; set; }
        bool IsSystemUser { get; set; }
        string FullName { get; }
        UserConfiguration Configuration { get; set; }
        MeasurementSystemEnum ConversionSystem { get; set; }
        bool HasPermission(params PermissionEnum[] permission);
    }
}