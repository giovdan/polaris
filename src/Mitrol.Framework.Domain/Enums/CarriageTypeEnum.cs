namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipo di motrice (Program)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("VerticalClamp")]
    public enum CarriageTypeEnum
    {
        /// <summary>
        /// Motrice con pinza verticale
        /// </summary>
        [EnumSerializationName("VerticalClamp")]
        [EnumField("Motrice con pinza verticale", true, "LBL_CARRIAGETYPE_VERTICALCLAMP")]
        VerticalClamp = 0,

        /// <summary>
        /// Motrice con pinza orizzontale
        /// </summary>
        [EnumSerializationName("HorizontalClamp")]
        [EnumField("Motrice con pinza orizzontale", true, "LBL_CARRIAGETYPE_HORIZONTALCLAMP")]
        HorizontalClamp = 1,

        /// <summary>
        /// Motrice con calibro
        /// </summary>
        [EnumSerializationName("Gauge")]
        [EnumField("Motrice con calibro", true, "LBL_CARRIAGETYPE_GAUGE")]
        Gauge = 2,

        /// <summary>
        /// Motrice con fascio di barre
        /// </summary>
        [EnumSerializationName("BarBundle")]
        [EnumField("Motrice con fascio di barre", true, "LBL_CARRIAGETYPE_BARBUNDLE")]
        BarBundle = 3,

        /// <summary>
        /// Motrice con spingitore
        /// </summary>
        [EnumSerializationName("Pusher")]
        [EnumField("Motrice con spingitore", true, "LBL_CARRIAGETYPE_PUSHER")]
        Pusher = 4,

        ///<summary>
        /// Motrice con spingitore fascio di barre
        ///</summary>
        [EnumSerializationName("BarBundlePusher")]
        [EnumField("Motrice con spingitore fascio di barre", true, "LBL_CARRIAGETYPE_BARBUNDLEPUSHER")]
        BarBundlePusher = 5,

        ///<summary>
        /// Motrice con vassoio (nesting piastre robot)
        /// </summary>
        [EnumSerializationName("NestingPlateRobot")]
        [EnumField("Motrice con vassoio (nesting piastre robot)", true, "LBL_CARRIAGETYPE_NESTINGPLATEROBOT")]
        NestingPlateRobot = 6
    }
}
