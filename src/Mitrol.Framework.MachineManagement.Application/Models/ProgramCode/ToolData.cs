namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class ToolDataComparer : IEqualityComparer<ToolData>
    {
        public static ToolDataComparer Default => s_comparer.Value;

        private static readonly Lazy<ToolDataComparer> s_comparer
            = new Lazy<ToolDataComparer>(Creator);

        private static ToolDataComparer Creator() => new ToolDataComparer();

        public bool Equals([AllowNull] ToolData first, [AllowNull] ToolData second)
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
                return first.Id == second.Id
                    && first.LifePercentage == second.LifePercentage
                    && first.LifeColor == second.LifeColor;
            }
        }

        public int GetHashCode([DisallowNull] ToolData obj)
        {
            var hashCode = 792638326;

            hashCode = hashCode * -1521134295 + EqualityComparer<long>.Default.GetHashCode(obj.Id);
            if (obj.LifePercentage.HasValue)
                hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(obj.LifePercentage.Value);
            hashCode = hashCode * -1521134295 + EqualityComparer<StatusColorEnum>.Default.GetHashCode(obj.LifeColor);

            return hashCode;
        }
    }

    public class ToolData
    {
        /// <summary>
        /// Identificativo del tool corrente
        /// </summary>
        [JsonProperty("Id")]
        public long Id { get; set; }

        /// <summary>
        /// Nome del tool corrente
        /// </summary>
        [JsonProperty("ToolDisplayName")]
        public string ToolDisplayName { get; set; }

        /// <summary>
        /// Nome dell'icona associata al tool corrente
        /// </summary>
        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }

        /// <summary>
        /// Valore percentuale della vita residua del tool corrente
        /// </summary>
        [JsonProperty("LifePercentage")]
        public int? LifePercentage { get; set; }

        /// <summary>
        /// Colore della vita residua del tool corrente
        /// </summary>
        [JsonProperty("LifeColor")]
        public StatusColorEnum LifeColor { get; set; }

        /// <summary>
        /// Colore dell'utensile per grafica ISO
        /// </summary>
        [JsonProperty("ToolColor")]
        public StatusColorEnum ToolColor { get; set; }

        [JsonProperty("Diameter")]
        public decimal Diameter { get; set; }

        [JsonProperty("Kerf")]
        public decimal? Kerf { get; set; }
    }
}
