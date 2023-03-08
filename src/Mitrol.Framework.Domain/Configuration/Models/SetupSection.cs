namespace Mitrol.Framework.Domain.Configuration.Models
{
    public class SetupSection
    {
        public string ApplicationFolder { get; set; }
        public string StartupFolder { get; set; }
        public string BaseUri { get; set; }
        public string ApiVersion { get; set; }
        public int MaxConnectionAttempts { get; set; }
    }
}
