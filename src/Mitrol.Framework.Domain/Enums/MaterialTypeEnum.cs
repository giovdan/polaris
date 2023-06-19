namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologie di materiali
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Undefined")]
    public enum MaterialTypeEnum : long
    {
        /// <summary>
        /// Non definito
        /// </summary>
        [Description("Non definito")]
        [EnumSerializationName("Undefined")]
        [EnumField("Non Definito",true,"LBL_MATERIALTYPE_UNDEFINED")]
        Undefined = 0,

        /// <summary>
        /// Acciaio dolce
        /// </summary>
        [Description("Acciaio dolce")]
        [EnumSerializationName("MildSteel")]
        [EnumCustomName("Mild Steel"), EnumCustomName("MS")] // Custom name per l'import da csv
        [EnumField("Acciaio dolce", true, "LBL_MATERIALTYPE_MILDSTEEL")]
        MildSteel = 1,

        /// <summary>
        /// Acciao inox (normativa iso 1.43)
        /// </summary>
        [Description("Acciaio inossidabile")]
        [EnumSerializationName("StainlessSteel")]
        [EnumCustomName("Stainless Steel")] // Custom name per l'import da csv
        [EnumField("Acciaio inossidabile", true, "LBL_MATERIALTYPE_STAINLESSSTEEL")]
        StainlessSteel = 2,

        /// <summary>
        /// Alluminio
        /// </summary>
        [Description("Alluminio")]
        [EnumSerializationName("Aluminum")]
        [EnumField("Alluminio", true, "LBL_MATERIALTYPE_ALUMINUM")]
        Aluminum = 4,

        /// <summary>
        /// Acciao hardox
        /// </summary>
        [Description("Acciaio duro")]
        [EnumSerializationName("HardSteel")]
        [EnumCustomName("Hard Steel")] // Custom name per l'import da csv
        [EnumField("Acciaio duro", true, "LBL_MATERIALTYPE_HARDSTEEL")]
        HardSteel = 8,

        /// <summary>
        /// Acciao inox (normativa iso 1.45)
        /// </summary>
        [Description("Acciaio inossidabile 1.45")]
        [EnumSerializationName("StainlessSteel145")]
        [EnumField("Acciaio inossidabile 1.45", true, "LBL_MATERIALTYPE_STAINLESSSTEEL145")]
        StainlessSteel145 = 16,
    }

    /// <summary>
    /// Tipologie di materiali 'combinati'
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("MildSteel")]
    public enum MaterialTypeCombinationEnum : long
    {
        [Description("Non definito")]
        [EnumSerializationName("NotDefined")]
        [EnumField("Non Definito", true, "LBL_MATERIALTYPE_UNDEFINED")]
        NotDefined = 0,

        [Description("Acciaio dolce")]
        [EnumSerializationName("MildSteel")]
        [EnumField("Acciaio dolce", true, "LBL_MATERIALTYPE_MILDSTEEL")]
        MildSteel = 1,

        [Description("Acciaio inox")]
        [EnumSerializationName("StainlessSteel")]
        [EnumField("Acciaio inossidabile", true, "LBL_MATERIALTYPE_STAINLESSSTEEL")]
        StainlessSteel = 2,

        [Description("Alluminio")]
        [EnumSerializationName("Aluminum")]
        [EnumField("Alluminio", true, "LBL_MATERIALTYPE_ALUMINUM")]
        Aluminum = 4,

        [Description("Acciaio duro")]
        [EnumSerializationName("HardSteel")]
        [EnumField("Acciaio duro", true, "LBL_MATERIALTYPE_HARDSTEEL")]
        HardSteel = 8,

        [Description("Acciaio inox / Acciaio duro")]
        [EnumSerializationName("StainlessSteel_HardSteel")]
        [EnumField("Acciaio inox / Acciaio duro", true, "LBL_MATERIALTYPE_STAINLESSSTEEL_HARDSTEEL")]
        StainlessSteel_HardSteel = StainlessSteel | HardSteel,
    }
}