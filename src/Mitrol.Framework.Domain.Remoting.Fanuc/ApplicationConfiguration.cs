namespace Mitrol.Framework.Domain.Remoting.Fanuc
{
    using Microsoft.Extensions.Configuration;
    using Mitrol.Framework.Domain.Configuration.Models;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    public static class ApplicationConfiguration
    {
        public static IConfigurationRoot GetConfiguration()
        {
            try
            {
                //Log4Net.Debug("Inizializzazione file json di configurazione");

                return new ConfigurationBuilder()
                    .SetBasePath(Path.GetDirectoryName(Application.ExecutablePath))
                    .AddJsonFile("Mitrol.Framework.Domain.Remoting.Fanuc.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables().Build();
            }
            catch (Exception ex)
            {
                //Log4Net.Error($"Setup => {Common.GetError(ex)}");
                Debug.Print(ex.Message);
                return null;
            }
        }

        public static SetupSection SetupSection
        {
            get
            {
                if (s_setupSection == null)
                {
                    s_setupSection = new SetupSection();
                    GetConfiguration().GetSection("Setup").Bind(s_setupSection);
                }
                return s_setupSection;
            }
        }
        private static SetupSection s_setupSection;

        public static BootSection BootSection
        {
            get
            {
                if (s_bootSection == null)
                {
                    s_bootSection = new BootSection();
                    GetConfiguration().GetSection("Boot").Bind(s_bootSection);
                }
                return s_bootSection;
            }
        }
        private static BootSection s_bootSection;
    }
}
