namespace Mitrol.Framework.Domain.Bus
{
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;

    public class LoginEvent:Event
    {
        public LoginEvent(UserSession session)
        {
            this.UserSession = session;
        }

        [JsonProperty("UserSession")]
        public UserSession UserSession { get; set; }
    }
}
