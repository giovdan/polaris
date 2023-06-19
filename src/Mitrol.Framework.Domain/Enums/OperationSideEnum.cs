namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Definizioni del piano di programmazione delle operazioni del pezzo
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("X")]
    public enum OperationSideEnum : int
    {
        /// <summary>
        /// Non definito
        /// </summary>
        [DatabaseDisplayName("X")]
        [EnumCustomName("X")]
        [EnumSerializationName("X")]
        [EnumField("Non definito", true, "LBL_SIDE_X")]
        X = 0,

        /// <summary>
        /// Right side (external)
        /// </summary>
        [DatabaseDisplayName("A")]
        [EnumCustomName("A")]
        [EnumSerializationName("A")]
        [EnumField("Right side (external)", true, "LBL_SIDE_A")]
        A = 1,

        /// <summary>
        /// Left side (external)
        /// </summary>
        [DatabaseDisplayName("B")]
        [EnumCustomName("B")]
        [EnumSerializationName("B")]
        [EnumField("Left side (external)", true, "LBL_SIDE_B")]
        B = 2,

        /// <summary>
        /// Top web
        /// </summary>
        [DatabaseDisplayName("C")]
        [EnumCustomName("C")]
        [EnumSerializationName("C")]
        [EnumField("Top web", true, "LBL_SIDE_C")]
        C = 3,

        /// <summary>
        /// Bottom web
        /// </summary>
        [DatabaseDisplayName("D")]
        [EnumCustomName("D")]
        [EnumSerializationName("D")]
        [EnumField("Bottom web", true, "LBL_SIDE_D")]
        D = 4,

        /// <summary>
        /// Internal right side (TS76 or TS77 or TS78)
        /// </summary>
        [DatabaseDisplayName("AI")]
        [EnumCustomName("AI")]
        [EnumSerializationName("AI")]
        [EnumField("Internal right side", true, "LBL_SIDE_AI")]
        AI = 5,

        /// <summary>
        /// Internal left side (TS76 or TS77 or TS78)
        /// </summary>
        [DatabaseDisplayName("BI")]
        [EnumCustomName("BI")]
        [EnumSerializationName("BI")]
        [EnumField("Internal left side", true, "LBL_SIDE_BI")]
        BI = 6,

        /// <summary>
        /// Right and left side (both from A to B)
        /// </summary>
        [DatabaseDisplayName("AB")]
        [EnumCustomName("AB")]
        [EnumSerializationName("AB")]
        [EnumField("Right and left side", true, "LBL_SIDE_AB")]
        AB = 7,

        /// <summary>
        /// Left and Right side (both from B to A)
        /// </summary>
        [DatabaseDisplayName("BA")]
        [EnumCustomName("BA")]
        [EnumSerializationName("BA")]
        [EnumField("Left and Right side", true, "LBL_SIDE_BA")]
        BA = 8,

        /// <summary>
        /// Top and bottom web (both from C to D)
        /// </summary>
        [DatabaseDisplayName("CD")]
        [EnumCustomName("CD")]
        [EnumSerializationName("CD")]
        [EnumField("Top and bottom web", true, "LBL_SIDE_CD")]
        CD = 9
    }
}
