

namespace Mitrol.Framework.Domain.Macro.Enum
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json.Converters;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Definizione del gruppo di appartenenza delle macro (ex GRPX -> GRP1 per macro agli angoli)
    /// </summary>
    
    public enum  MacroGroupEnum
    {
        NotDefined = 0,
        /// <summary>
        /// Macro che lavora ai bordi (GRP1)
        /// </summary>
        [Description("Macro che lavora ai bordi")]
        [EnumSerializationName("Edge")]
        [EnumMember(Value = "Edge")]
        Edge = 1,
        /// <summary>
        /// Macro che lavora ai vertici (GRP2)
        /// </summary>
        [Description("Macro che lavora ai vertici")]
        [EnumSerializationName("Apexes")]
        [EnumMember(Value = "Apexes")]
        Apexes =2,
        /// <summary>
        /// Macro che lavora internamente al pezzo (GRP3)
        /// </summary>
        [Description("Macro che lavora internamente al pezzo")]
        [EnumSerializationName("Internal")]
        [EnumMember(Value = "internal")]
        Internal =3,
        /// <summary>
        /// Macro che lavora lungo la lunghezza del pezzo (GRP4)
        /// </summary>
        [Description("Macro che lavora lungo la lunghezza del pezzo")]
        [EnumSerializationName("Length")]
        [EnumMember(Value = "Length")]
        Length =4
    }
}
