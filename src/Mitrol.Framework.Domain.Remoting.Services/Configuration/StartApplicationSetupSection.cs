namespace Mitrol.Framework.Domain.Remoting.Services.Configuration
{
    using Microsoft.Extensions.Configuration;
    using Mitrol.Framework.Domain.Configuration.Models;

    public class StartApplicationSetupSection : SetupSection
    {
        public StartApplicationSetupSection() { }

        public StartApplicationSetupSection(IConfigurationRoot configurationRoot)
        {
            configurationRoot.GetSection("Setup").Bind(this);
        }

        public int StartServiceWait { get; set; }
        public string[] Services { get; set; }
        public string[] BatchList { get; set; }
    }
}
