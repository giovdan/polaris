namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using System;
    using System.Collections.Generic;

    public class UserSession : IUserSession
    {
        public string Username { get; private set; }

        public string SessionId { get; private set; }

        public string AccessToken { get; set; }
        public DateTime AccessTokenIssueDate { get; set; }
        public DateTime? AccessTokenExpirationDate { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }

        public string Culture { get; private set; }

        public GroupEnum Group { get; private set; }

        public IEnumerable<string> Permissions { get; private set; }

        public long UserId { get; set; }
        public string MachineName { get; set; }
        public bool IsSystemUser { get; set; }
        public string FullName { get; private set; }

        public UserConfiguration Configuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public MeasurementSystemEnum ConversionSystem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public UserSession(string username, string fullName, long userId, string culture, IEnumerable<string> permissions, string machineName)
        {
            Username = username;
            FullName = fullName;
            UserId = userId;
            Culture = culture;
            Permissions = permissions;
            MachineName = machineName;
        }

        public bool HasPermission(params PermissionEnum[] permission)
        {
            throw new NotImplementedException();
        }
    }
}
