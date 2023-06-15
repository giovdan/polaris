namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Default Session using Mitrol.Framework.Domain.Core
    /// </summary>
    [Serializable()]
    public class UserSession : IUserSession
    {
        #region < User's Info >

        [JsonProperty("UserId")]
        public long UserId { get; set; }

        [JsonProperty("Username")]
        public string Username { get; private set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("Culture")]
        public string Culture { get; set; }

        [JsonProperty("Groups")]
        public IEnumerable<GroupItem> Groups { get; private set; }

        [JsonProperty("Permissions")]
        public IEnumerable<string> Permissions { get; private set; }

        [JsonProperty("IsSystemUser")]
        public bool IsSystemUser { get; set; }

        [JsonProperty("Configuration")]
        public UserConfiguration Configuration { get; set; }

        #endregion < User's Info >

        #region < Session's Info >
        [JsonProperty("SessionId")]
        public string SessionId { get; set; }

        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("AccessTokenIssueDate")]
        public DateTime AccessTokenIssueDate { get; set; }

        [JsonProperty("AccessTokenExpirationDate")]
        public DateTime? AccessTokenExpirationDate { get; set; }

        [JsonProperty("RefreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("RefreshTokenExpirationDate")]
        public DateTime? RefreshTokenExpirationDate { get; set; }

        [JsonProperty("ConversionSystem")]
        public MeasurementSystemEnum ConversionSystem { get; set; }
        #endregion < Session's Info >

        #region < Machine's Info >

        [JsonProperty("MachineName")]
        public string MachineName { get; set; }


        #endregion < Machine's Info >

        public UserSession(string username
                            , string fullName
                            , long userId
                            , string culture
                            , IEnumerable<GroupItem> groups
                            , IEnumerable<string> permissions
                            , string machineName
                            , string userConfiguration
                            , MeasurementSystemEnum conversionSystem)
        {
            Username = username;
            Culture = culture;
            Groups = groups;
            Permissions = permissions;
            UserId = userId;
            MachineName = machineName;
            FullName = fullName;
            Configuration = !string.IsNullOrEmpty(userConfiguration)
                                ? JsonConvert.DeserializeObject<UserConfiguration>(userConfiguration)
                                : null;
            ConversionSystem = conversionSystem;
        }


        public bool HasPermission(params PermissionEnum[] permissions)
        {
            bool hasPermission = false;
            if (Permissions != null)
            {
                foreach (var permission in permissions)
                {
                    if (Permissions.Contains(permission.ToString()))
                    {
                        hasPermission = true;
                        break;
                    }
                }
            }

            return hasPermission;
        }

        /// <summary>
        /// Get Protection Levels for Paramters management
        /// </summary>
        /// <returns></returns>
        public HashSet<ProtectionLevelEnum> GetProtectionLevels()
        {
            var protectionLevels = new HashSet<ProtectionLevelEnum>();
            if (HasPermission(PermissionEnum.MACHINEPARAMETERS_CRITICAL_READ))
            {
                protectionLevels.Add(ProtectionLevelEnum.Critical);
            }

            if (HasPermission(PermissionEnum.MACHINEPARAMETERS_HIGH_READ))
            {
                protectionLevels.Add(ProtectionLevelEnum.High);
            }

            if (HasPermission(PermissionEnum.MACHINEPARAMETERS_MEDIUM_READ))
            {
                protectionLevels.Add(ProtectionLevelEnum.Medium);
            }

            if (HasPermission(PermissionEnum.MACHINEPARAMETERS_NORMAL_READ))
            {
                protectionLevels.Add(ProtectionLevelEnum.Normal);
            }

            return protectionLevels;
        }
    }
}