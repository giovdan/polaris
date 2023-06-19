namespace Mitrol.Framework.Domain.Models
{
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Methods through which applications can gain Access Tokens.
    /// </summary>
    [Flags]
    public enum GrantTypeEnum
    {
        /// <summary>
        /// Users provide credentials (username and password), typically using an interactive form to get the ID token.
        /// </summary>
        Password = 0,
        /// <summary>
        /// Ask for a new token if the access token has expired or you want to refresh the claims contained in the ID token.
        /// </summary>
        RefreshToken = 1,

        /// <summary>
        /// Users swipe a badge to get the ID token.
        /// </summary>
        Badge = 2,

        //Used Only for Boot User
        HashedPassword = 4
    }

    
    /// <summary>
    /// Login request
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Username
        /// </summary>
        /// <example>"localUser"</example>
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        /// <example>123456</example>
        [JsonProperty("Password")]
        public string Password { get; set; }

        /// <summary>
        /// Machine Name
        /// </summary>
        /// <example>GEMINI HPE</example>
        [JsonProperty("MachineName")]
        public string MachineName { get; set; }

        [JsonProperty("RefreshToken")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <example>Password</example>
        [JsonProperty("GrantType")]
        public GrantTypeEnum GrantType { get; set; }


        [JsonConstructor]
        public LoginRequest()
        {
            GrantType = GrantTypeEnum.Password;
        }

        public LoginRequest(string username, string password, string machineName)
        {
            UserName = username;
            Password = password;
            MachineName = machineName;
            GrantType = GrantTypeEnum.Password;
        }

        public LoginRequest(string refreshToken)
        {
            RefreshToken = refreshToken;
            GrantType = GrantTypeEnum.RefreshToken;
        }
    }
}