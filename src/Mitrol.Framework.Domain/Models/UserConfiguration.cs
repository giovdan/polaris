namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class UserConfiguration
    {
        [JsonProperty("WidgetsConfiguration")]
        public string WidgetsConfiguration { get; set; }

        [JsonProperty("MenuSide")] //left, right...
        public MenuSideEnum MenuSide { get; set; }

        [JsonProperty("DefaultConversionSystem")]
        public MeasurementSystemEnum DefaultConversionSystem { get; set; }

        public UserConfiguration()
        {
            MenuSide = MenuSideEnum.Left;
            DefaultConversionSystem = MeasurementSystemEnum.MetricSystem;
        }
    }

    public enum MenuSideEnum
    {
        Rigth = 1,
        Left = 2
    }
}
