namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipolgia di profili
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("X")]
    public enum ProfileTypeEnum : int
    {
        /// <summary>
        /// Profile not define
        /// </summary>
        X = 0,

        /// <summary>
        /// Profile Type: L
        /// </summary>
        [DatabaseDisplayName("L")]
        [Description("Angolare")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("L")]
        [EnumField("Profile Type: L", true, "LBL_PROFILETYPE_L")]
        L = 1,

        /// <summary>
        /// Profile Type: V
        /// </summary>
        [DatabaseDisplayName("V")]
        [Description("Angolare con angolo variabile")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("V")]
        [EnumField("Profile Type: v", true, "LBL_PROFILETYPE_V")]
        V = 2,

        /// <summary>
        /// Profile Type: B
        /// </summary>
        [DatabaseDisplayName("B")]
        [Description("Plats a Boudin")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("B")]
        [EnumField("Profile Type: B", true, "LBL_PROFILETYPE_B")]
        B = 3,

        /// <summary>
        /// Profile Type: I (comprende anche profili USA W ed S)
        /// </summary>
        [DatabaseDisplayName("I")]
        [Description("Trave")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("I")]
        [EnumField("Profile Type: I", true, "LBL_PROFILETYPE_I")]
        I = 4,

        /// <summary>
        /// Profile Type: D
        /// </summary>
        [DatabaseDisplayName("D")]
        [Description("Trave ad ali disuguali")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("D")]
        [EnumField("Profile Type: D", true, "LBL_PROFILETYPE_D")]
        D = 5,

        /// <summary>
        /// Profile Type: T
        /// </summary>
        [DatabaseDisplayName("T")]
        [Description("Profilo T")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("T")]
        [EnumField("Profile Type: T", true, "LBL_PROFILETYPE_T")]
        T = 6,

        /// <summary>
        /// Profile Type: U
        /// </summary>
        [DatabaseDisplayName("U")]
        [Description("Profilo U")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("U")]
        [EnumField("Profile Type: U", true, "LBL_PROFILETYPE_U")]
        U = 7,

        /// <summary>
        /// Profile Type: Q (comprende anche profilo M tubo custom)
        /// </summary>
        [DatabaseDisplayName("Q")]
        [Description("Tubo quadro")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("Q")]
        [EnumField("Profile Type: Q", true, "LBL_PROFILETYPE_Q")]
        Q = 8,

        /// <summary>
        /// Profile Type: C
        /// </summary>
        [DatabaseDisplayName("C")]
        [Description("Profilo a C")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("C")]
        [EnumField("Profile Type: C", true, "LBL_PROFILETYPE_C")]
        C = 9,

        /// <summary>
        /// Profile Type: F
        /// </summary>
        [DatabaseDisplayName("F")]
        [Description("Piatto")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("F")]
        [EnumField("Profile Type: F", true, "LBL_PROFILETYPE_F")]
        F = 11,

        /// <summary>
        /// Profile Type: N
        /// </summary>
        [DatabaseDisplayName("N")]
        [Description("Tubo quadro pieno")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("N")]
        [EnumField("Profile Type: N", true, "LBL_PROFILETYPE_N")]
        N = 12,

        /// <summary>
        /// Profile Type: P
        /// </summary>
        [DatabaseDisplayName("P")]
        [Description("Piastra")]
        [CodeGenerators(DatabaseDisplayNameEnum.Thickness, DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.Width)]
        [SuggestedFormat("${Thickness} ${Length} x ${Width} ${HeatNumber} ${Supplier}")]
        [EnumSerializationName("P")]
        [EnumField("Profile Type: P", true, "LBL_PROFILETYPE_P")]
        P = 13,

        /// <summary>
        /// Profile Type: R
        /// </summary>
        [DatabaseDisplayName("R")]
        [Description("Tubo tondo")]
        [CodeGenerators(DatabaseDisplayNameEnum.Length, DatabaseDisplayNameEnum.ProfileCode)]
        [SuggestedFormat("${Length} ${ProfileCode}")]
        [EnumSerializationName("R")]
        [EnumField("Profile Type: R", true, "LBL_PROFILETYPE_R")]
        R = 14
    }
}