namespace Mitrol.Framework.Domain.Models
{
    using Newtonsoft.Json;
    using System.ComponentModel;

    [DefaultValue("NONE")]
    public enum ToleranceTypeEnum
    {
        /// <summary>
        /// Nessuna
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Assoluta
        /// </summary>
        ABS = 1,

        /// <summary>
        /// Percentuale
        /// </summary>
        PERC = 2
    }

    /// <summary>
    /// Configurazione tolleranze sui confronti
    /// </summary>
    public class ToleranceConfiguration
    {
        private const string s_lowerValueJsonName = "LowerValue";
        private const string s_typeJsonName = "Type";
        private const string s_upperValueJsonName = "UpperValue";

        /// <summary>
        /// Parameterless constructor used by AutoMapper
        /// </summary>
        public ToleranceConfiguration()
        { }


        [JsonConstructor]
        public ToleranceConfiguration([JsonProperty(s_typeJsonName)] ToleranceTypeEnum? type,
                                      [JsonProperty(s_upperValueJsonName)] float? upperValue,
                                      [JsonProperty(s_lowerValueJsonName)] float? lowerValue)
        {
            Type = type;
            UpperValue = upperValue;
            LowerValue = lowerValue;
        }

        /// <summary>
        /// Valore inferiore del range di tolleranza in modulo
        /// </summary>
        [JsonProperty(s_lowerValueJsonName)]
        public float? LowerValue { get; protected set; }

        /// <summary>
        /// Tipo di tolleranza
        /// </summary>
        [JsonProperty(s_typeJsonName)]
        public ToleranceTypeEnum? Type { get; protected set; }

        /// <summary>
        /// Valore superiore del range di tolleranza in modulo
        /// </summary>
        [JsonProperty(s_upperValueJsonName)]
        public float? UpperValue { get; protected set; }
    }
}
