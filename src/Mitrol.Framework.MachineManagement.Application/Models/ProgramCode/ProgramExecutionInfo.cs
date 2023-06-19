namespace Mitrol.Framework.MachineManagement.Application.Models.ProgramCode
{
    using Mitrol.Framework.Domain.Configuration;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class ProgramExecutionInfo
    {
        [JsonProperty("Units")]
        public List<UnitData> Units { get; set; }

        [JsonProperty("Overlays")]
        public List<GraphicOverlay> Overlays { get; set; }

        [JsonProperty("CurrentUnitIndex")]
        public int CurrentUnitIndex { get; set; }

        // TO DO
        //[JsonProperty("AxesOverride")]
        //public JogOverrideValue AxesOverride { get; set; }

        [JsonProperty("AxesOverrideConfiguration")]
        public OverrideConfiguration AxesOverrideConfiguration { get; set; }

        public ProgramExecutionInfo()
        {

        }

        public ProgramExecutionInfo(CncConfiguration cncConfiguration)
        {
            // TODO
            // AxesOverride = new JogOverrideValue(0, minColorThreshold: 0, maxColorThreshold: 100);
            AxesOverrideConfiguration = cncConfiguration.GetAxesOverrideConfig();
            Units = new List<UnitData>();
            Overlays = new List<GraphicOverlay>();
        }
    }

    public static class CodeControllerDataExtensions
    {
        public static ProgramExecutionInfo SetAxesOverride(this ProgramExecutionInfo source, short feedRateValue)
        {
            if (source is null) { throw new ArgumentNullException(nameof(source)); }

            // TODO
            //source.AxesOverride = new JogOverrideValue(feedRateValue, minColorThreshold: 0, maxColorThreshold: 100);

            return source;
        }
    }
}
