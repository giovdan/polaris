namespace Mitrol.Framework.MachineManagement.Application.Models.Setup
{
    using Mitrol.Framework.Domain.Configuration;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;


    /// <summary>
    /// Rappresentazione unità di setup
    /// </summary>
    public class UnitSetupListItem : IEntityWithImage
    {
        // Identificativo unità
        [JsonProperty("UnitId")]
        public UnitEnum UnitId { get; set; }

        // Tipologia unità
        [JsonProperty("UnitType")]
        public PlantUnitEnum UnitType { get; set; }

        [JsonProperty("UnitTypeLocalizationKey")]
        public string UnitTypeLocalizationKey { get; set; }

        // Numero di tool non configurati correttamente
        [JsonProperty("WrongConfiguredTools")]
        public int WrongConfiguredTools { get; set; }

        // Totale numero di tool configurati
        [JsonProperty("TotalConfiguredTools")]
        public int TotalConfiguredTools { get; set; }
        public string ImageCode { get; set; }

        /// Posizione nella griglia (4x4) del sinottico impianto
        [JsonProperty("GridPosition")]
        public GridPosition GridPosition { get; set; }

        public UnitSetupListItem(PlantUnitEnum plantUnit, UnitEnum unit)
        {
            UnitType = plantUnit;
            UnitTypeLocalizationKey = $"{MachineManagementExtensions.LABEL_SETUPUNIT}_{plantUnit.ToString().ToUpper()}";
            UnitId = unit;
            ImageCode = $"SETUP_{plantUnit.ToString().ToUpper()}";
        }
    }

}
