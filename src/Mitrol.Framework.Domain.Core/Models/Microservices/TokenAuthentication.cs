namespace Mitrol.Framework.Domain.Core.Models
{
    public class TokenAuthentication
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        /// <summary>
        /// Time to Access Token Expires (minutes)
        /// </summary>
        public int ExpiredAt { get; set; }

        /// <summary>
        /// Time to Refresh Token Expires (minutes)
        /// </summary>
        public int RefreshExpiredAt { get; set; }
    }
}