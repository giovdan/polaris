namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class UnitDataComparer : IEqualityComparer<UnitData>
    {
        public static UnitDataComparer Default => s_comparer.Value;

        private static readonly Lazy<UnitDataComparer> s_comparer
            = new Lazy<UnitDataComparer>(Creator);

        private static UnitDataComparer Creator() => new UnitDataComparer();

        public bool Equals([AllowNull] UnitData first, [AllowNull] UnitData second)
        {
#pragma warning disable IDE0041
            var is_first_null = ReferenceEquals(first, null);
            var is_second_null = ReferenceEquals(second, null);
#pragma warning restore IDE0041

            if (is_first_null && is_second_null)
                return true;
            else if (is_first_null || is_second_null)
                return false;
            else
            {
                return first.X == second.X
                    && first.Y == second.Y
                    && first.Z == second.Z
                    && first.Zprobed == second.Zprobed
                    && first.ArcVoltageFinal == second.ArcVoltageFinal
                    && first.ArcVoltageReal == second.ArcVoltageReal
                    && first.StopX == second.StopX
                    && first.StopY == second.StopY
                    && first.GraphX == second.GraphX
                    && first.GraphY == second.GraphY
                    && ToolDataComparer.Default.Equals(first.Tool, second.Tool);
            }
        }

        public int GetHashCode([DisallowNull] UnitData obj)
        {
            var hashCode = 792638326;

            hashCode = hashCode * -1521134295 + EqualityComparer<decimal>.Default.GetHashCode(obj.X);
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal>.Default.GetHashCode(obj.Y);
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal>.Default.GetHashCode(obj.Z);
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal?>.Default.GetHashCode(obj.Zprobed);
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(obj.ArcVoltageFinal);
            hashCode = hashCode * -1521134295 + EqualityComparer<float?>.Default.GetHashCode(obj.ArcVoltageReal);
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal?>.Default.GetHashCode(obj.StopX);
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal?>.Default.GetHashCode(obj.StopY);
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal>.Default.GetHashCode(obj.GraphX);
            hashCode = hashCode * -1521134295 + EqualityComparer<decimal>.Default.GetHashCode(obj.GraphY);
            hashCode = hashCode * -1521134295 + ToolDataComparer.Default.GetHashCode(obj.Tool);

            return hashCode;
        }
    }

    /// <summary>
    /// Classe di definizione delle unità configurate nell'impianto, ad uso visualizzazione nella pagina ISO
    /// </summary>
    public class UnitData
    {
        [JsonProperty("UnitId")]
        public int UnitId { get; set; }

        // Tipo di unità
        [JsonProperty("UnitType")]
        public PlantUnitEnum UnitType { get; set; }

        [JsonProperty("UnitTypeLocalizationKey")]
        public string UnitTypeLocalizationKey { get; set; }

        // Unità
        [JsonProperty("Unit")]
        public UnitEnum Unit { get; set; }

        /// <summary>
        /// Quota corrente X dell'unità
        /// </summary>
        [JsonProperty("X")]
        public decimal X { get; set; }

        /// <summary>
        /// Quota corrente Y dell'unità
        /// </summary>
        [JsonProperty("Y")]
        public decimal Y { get; set; }

        /// <summary>
        /// Quota corrente Z dell'unità
        /// </summary>
        [JsonProperty("Z")]
        public decimal Z { get; set; }

        /// <summary>
        /// Quota corrente Z di palpatura dell'unità
        /// </summary>
        [JsonProperty("Zprobed")]
        public decimal? Zprobed { get; set; }

        /// <summary>
        /// SetPoint Tensione Arco finale [volt]
        /// Solo per unità plasma
        /// </summary>
        [JsonProperty("ArcVoltageFinal")]
        public float? ArcVoltageFinal { get; set; }

        /// <summary>
        /// Tensione Arco reale [volt]
        /// Solo per unità plasma
        /// </summary>
        [JsonProperty("ArcVoltageReal")]
        public float? ArcVoltageReal { get; set; }

        /// <summary>
        /// Coordinata X di interruzione del taglio
        /// Solo per unità plasma
        /// </summary>
        [JsonProperty("StopX")]
        public decimal? StopX { get; set; }

        /// <summary>
        /// Coordinata Y di interruzione del tagli o
        /// Solo per unità plasma
        /// </summary>
        [JsonProperty("StopY")]
        public decimal? StopY { get; set; }

        /// <summary>
        /// Quota corrente X dell'unità per grafica ISO (senza offset unità)
        /// </summary>
        [JsonProperty("GraphX")]
        public decimal GraphX { get; set; }

        /// <summary>
        /// Quota corrente Y dell'unità per grafica ISO (senza offset unità)
        /// </summary>
        [JsonProperty("GraphY")]
        public decimal GraphY { get; set; }

        [JsonProperty("Tool")]
        public ToolData Tool { get; set; }

        public UnitData(PlantUnitEnum plantUnit, UnitEnum unit)
        {
            Tool = new ToolData();
            UnitType = plantUnit;
            UnitTypeLocalizationKey = $"{MachineManagementExtensions.LABEL_SETUPUNIT}_{plantUnit.ToString().ToUpper()}";
            Unit = unit;
        }
    }
}
