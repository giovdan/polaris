namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Node")]
    public enum OperationTypeEnum : int
    {
        Undefined = 0,

        /// <summary>
        /// NODE: operations node (max. 4 levels)
        /// </summary>
        [Description("Nodo")]
        [DatabaseDisplayName("NODE")]
        [EnumSerializationName("NODE")]
        [SuggestedFormat("${Name}")]
        [OperationInfo(isSlave: false, ToolTypeEnum.NotDefined)]
        Node = 1,

        /// <summary>
        /// HOL: hole
        /// </summary>
        [Description("Foro passante")]
        [SuggestedFormat("s ${Side} x ${X} y ${Y} ø ${Diameter}")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS32, ToolTypeEnum.TS33, ToolTypeEnum.TS40, ToolTypeEnum.TS62, ToolTypeEnum.TS51, ToolTypeEnum.TS52)]
        [DatabaseDisplayName("HOL")]
        [EnumSerializationName("HOL")]
        Hol = 2,

        /// <summary>
        /// POINT: point
        /// </summary>
        [Description("Bulinatura")]
        [DatabaseDisplayName("POINT")]
        [EnumSerializationName("POINT")]
        [SuggestedFormat("s ${Side} x ${X} y ${Y}")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS33, ToolTypeEnum.TS39)]
        Point = 3,

        /// <summary>
        /// SLOT: slot
        /// </summary>
        [Description("Asola")]
        [DatabaseDisplayName("SLOT")]
        [EnumSerializationName("SLOT")]
        [SuggestedFormat("s ${Side} x ${X} y ${Y} r ${Radius}")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS51, ToolTypeEnum.TS52)]
        Slot = 4,

        /// <summary>
        /// TAP: tap
        /// </summary>
        [Description("Maschiatura")]
        [DatabaseDisplayName("TAP")]
        [EnumSerializationName("TAP")]
        [SuggestedFormat("s ${Side} x ${X} y ${Y} ø ${Diameter}")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS41, ToolTypeEnum.TS71)]
        Tap = 5,

        /// <summary>
        /// PATHC: cutting path
        /// </summary>
        [Description("Percorso di taglio sul piano")]
        [DatabaseDisplayName("PATHC")]
        [EnumSerializationName("PATHC")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS51, ToolTypeEnum.TS52)]
        [SuggestedFormat("s ${Side}")]
        [RelatedAdditionalItemType(PathVertex)]
        PathC = 6,

        /// <summary>
        /// DEBURR: deburr
        /// </summary>
        [Description("Sbavatura")]
        [DatabaseDisplayName("DEBURR")]
        [EnumSerializationName("DEBURR")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS34, ToolTypeEnum.TS36)]
        [SuggestedFormat("s ${Side} x ${X} y ${Y} ø ${Diameter}")]
        Deburr = 7,

        /// <summary>
        /// BORE: bore
        /// </summary>
        [Description("Lamatura")]
        [DatabaseDisplayName("BORE")]
        [EnumSerializationName("BORE")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS38)]
        [SuggestedFormat("s ${Side} x ${X} y ${Y} ø ${Diameter}")]
        Bore = 8,

        /// <summary>
        /// FLARE: flare
        /// </summary>
        [Description("Svasatura")]
        [DatabaseDisplayName("FLARE")]
        [EnumSerializationName("FLARE")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS35)]
        [SuggestedFormat("s ${Side} x ${X} y ${Y} ø ${Diameter}")]
        Flare = 9,

        /// <summary>
        /// SHAPE: external profile
        /// </summary>
        [Description("Profilo esterno")]
        [DatabaseDisplayName("SHAPE")]
        [EnumSerializationName("SHAPE")]
        [SuggestedFormat("s ${Side}")]
        [OperationInfo(isSlave: false, ToolTypeEnum.NotDefined)]
        [RelatedAdditionalItemType(Vertex)]
        Shape = 10,

        /// <summary>
        /// MILL: milling macros
        /// </summary>
        [Description("Macro di fresatura")]
        [DatabaseDisplayName("MILL")]
        [EnumSerializationName("MILL")]
        [SuggestedFormat("${MacroName}")]
        [RelatedAdditionalItemType(OperationTypeEnum.ToolTechnology)]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS32, ToolTypeEnum.TS33, ToolTypeEnum.TS34, ToolTypeEnum.TS35, ToolTypeEnum.TS36, ToolTypeEnum.TS38,
                                        ToolTypeEnum.TS39, ToolTypeEnum.TS40, ToolTypeEnum.TS41, ToolTypeEnum.TS61, ToolTypeEnum.TS62, ToolTypeEnum.TS68,
                                        ToolTypeEnum.TS69, ToolTypeEnum.TS70, ToolTypeEnum.TS71, ToolTypeEnum.TS73, ToolTypeEnum.TS74, ToolTypeEnum.TS75,
                                        ToolTypeEnum.TS76, ToolTypeEnum.TS77, ToolTypeEnum.TS78, ToolTypeEnum.TS79)]
        Mill = 11,

        /// <summary>
        /// MCUT: cutting macros
        /// </summary>
        [Description("Macro di taglio")]
        [DatabaseDisplayName("MCUT")]
        [EnumSerializationName("MCUT")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS51, ToolTypeEnum.TS52)]
        [SuggestedFormat("${MacroName}")]
        MCut = 12,

        /// <summary>
        /// NOTCH: notching macros
        /// </summary>
        [Description("Macro di stozzatura")]
        [DatabaseDisplayName("NOTCH")]
        [EnumSerializationName("NOTCH")]
        [OperationInfo(isSlave: false, ToolTypeEnum.NotDefined)]
        [SuggestedFormat("${MacroName}")]
        Notch = 13,

        /// <summary>
        /// SHF: repositioning
        /// </summary>
        [Description("Riposizionamento")]
        [DatabaseDisplayName("SHF")]
        [EnumSerializationName("SHF")]
        [SuggestedFormat("x ${CX} y ${CY}")]
        [OperationInfo(isSlave: false, ToolTypeEnum.NotDefined)]
        Shf = 14,

        /// <summary>
        /// SENSE: probe
        /// </summary>
        [Description("Palpatura")]
        [DatabaseDisplayName("SENSE")]
        [EnumSerializationName("SENSE")]
        [OperationInfo(isSlave: false, ToolTypeEnum.NotDefined)]
        Sense = 15,

        /// <summary>
        /// MISC: CNC function
        /// </summary>
        [Description("Programmazione di una funzione miscellanea specifica del CNC")]
        [DatabaseDisplayName("MISC")]
        [EnumSerializationName("MISC")]
        [SuggestedFormat("s ${Side} x ${X} y ${Y}")]
        [OperationInfo(isSlave: false, ToolTypeEnum.NotDefined)]
        Misc = 16,

        /// <summary>
        /// STIFF: stiffner
        /// </summary>
        [Description("Rinforzo programmato")]
        [DatabaseDisplayName("STIFF")]
        [EnumSerializationName("STIFF")]
        [OperationInfo(isSlave:false, ToolTypeEnum.NotDefined)]
        [SuggestedFormat("s ${Side} x ${X} y ${Y}")]
        Stiff = 17,

        /// <summary>
        /// FHOL: hol with flare
        /// </summary>
        [Description("Foro svasato")]
        [DatabaseDisplayName("FHOL")]
        [EnumSerializationName("FHOL")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS73)]
        [SuggestedFormat("s ${Side} x ${X} y ${Y} ø ${Diameter}")]
        FHol = 18,

        /// <summary>
        /// DHOL: hol with deburr
        /// </summary>
        [Description("Foro sbavato")]
        [DatabaseDisplayName("DHOL")]
        [EnumSerializationName("DHOL")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS74)]
        [SuggestedFormat("s ${Side} x ${X} y ${Y} ø ${Diameter}")]
        DHol = 19,

        /// <summary>
        /// PATHVERTEX: point for PATHC, PATHM, PATHS
        /// </summary>
        [Description("Vertice PATH")]
        [DatabaseDisplayName("PATHVERTEX")]
        [EnumSerializationName("PATHVERTEX")]
        [SuggestedFormat("x ${X} y ${Y} r ${Radius}")]
        [OperationInfo(isSlave: true, ToolTypeEnum.NotDefined)]
        PathVertex = 21,

        /// <summary>
        /// PATHS: scribing path
        /// </summary>
        [Description("Percorso di scribing sul piano")]
        [DatabaseDisplayName("PATHS")]
        [EnumSerializationName("PATHS")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS53, ToolTypeEnum.TS68, ToolTypeEnum.TS76
                        , ToolTypeEnum.TS77, ToolTypeEnum.TS78, ToolTypeEnum.TS79, ToolTypeEnum.TS89)]
        [RelatedAdditionalItemType(PathVertex)]
        [SuggestedFormat("s ${Side}")]
        PathS = 22,

        /// <summary>
        /// PATHM: milling path
        /// </summary>
        [Description("Percorso di fresatura sul piano")]
        [DatabaseDisplayName("PATHM")]
        [EnumSerializationName("PATHM")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS61, ToolTypeEnum.TS62
                  , ToolTypeEnum.TS69, ToolTypeEnum.TS75)]
        [RelatedAdditionalItemType(PathVertex)]
        [SuggestedFormat("s ${Side}")]
        PathM = 23,

        /// <summary>
        /// PATHV: cutting vectorial path
        /// </summary>
        [Description("Percorso di taglio vettoriale")]
        [DatabaseDisplayName("PATHV")]
        [EnumSerializationName("PATHV")]
        [OperationInfo(isSlave: false, ToolTypeEnum.NotDefined)]
        PathV = 24,

        /// <summary>
        /// COPE: robot macros
        /// </summary>
        [Description("Macro di taglio robot")]
        [DatabaseDisplayName("COPE")]
        [EnumSerializationName("COPE")]
        [SuggestedFormat("${Name}")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS51, ToolTypeEnum.TS52)]
        Cope = 25,

        /// <summary>
        /// MARK: mark
        /// </summary>
        [Description("Marcatura")]
        [DatabaseDisplayName("MARK")]
        [EnumSerializationName("MARK")]
        [SuggestedFormat("s ${Side} x ${X} y ${Y}")]
        [OperationInfo(isSlave: false, 
            ToolTypeEnum.TS53, ToolTypeEnum.TS68
            , ToolTypeEnum.TS77, ToolTypeEnum.TS78, ToolTypeEnum.TS79
            , ToolTypeEnum.TS86, ToolTypeEnum.TS87, ToolTypeEnum.TS88
            , ToolTypeEnum.TS89)]
        Mark = 26,

        /// <summary>
        /// VERTEX: point of SHAPE
        /// </summary>
        [Description("VERTEX")]
        [DatabaseDisplayName("VERTEX")]
        [EnumSerializationName("VERTEX")]
        [SuggestedFormat("x ${X} y ${Y} r ${Radius}")]
        [OperationInfo(isSlave: true, ToolTypeEnum.NotDefined)]
        Vertex = 27,

        [Description("TOOLTECHNOLOGY")]
        [DatabaseDisplayName("TOOLTECH")]
        [EnumSerializationName("TOOLTECH")]
        [SuggestedFormat("TS ${TS} DN ${DN} COD ${COD}")]
        [OperationInfo(isSlave: true, ToolTypeEnum.NotDefined)]
        ToolTechnology = 28,

        /// <summary>
        /// BHOL: foro cieco
        /// </summary>
        [Description("Foro cieco")]
        [SuggestedFormat("s ${Side} x ${X} y ${Y} ø ${Diameter}")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS32, ToolTypeEnum.TS33, ToolTypeEnum.TS62)]
        [DatabaseDisplayName("BHOL")]
        [EnumSerializationName("BHOL")]
        BHol = 29,

        [Description("Macro segatrice")]
        [DatabaseDisplayName("MSAW")]
        [EnumSerializationName("MSAW")]
        [OperationInfo(isSlave: false, ToolTypeEnum.TS55, ToolTypeEnum.TS56, ToolTypeEnum.TS57)]
        MSaw = 30,
    }

    public static class OperationTypeEnumExtensions
    {
        public static LineTypeEnum GetLineType(this OperationTypeEnum operationType)
        {
            var lineType = LineTypeEnum.Undefined;
            switch (operationType)
            {
                case OperationTypeEnum.Hol:
                    lineType = LineTypeEnum.DrillOperationHOL;
                    break;
                case OperationTypeEnum.BHol:
                    lineType = LineTypeEnum.DrillOperationBHOL;
                    break;
                case OperationTypeEnum.Point:
                    lineType = LineTypeEnum.DrillOperationPOINT;
                    break;
                case OperationTypeEnum.Tap:
                    lineType = LineTypeEnum.DrillOperationTAP;
                    break;
                case OperationTypeEnum.Deburr:
                    lineType = LineTypeEnum.DrillOperationDEBURR;
                    break;
                case OperationTypeEnum.Bore:
                    lineType = LineTypeEnum.DrillOperationBORE;
                    break;
                case OperationTypeEnum.Flare:
                    lineType = LineTypeEnum.DrillOperationFLARE;
                    break;
                case OperationTypeEnum.FHol:
                    lineType = LineTypeEnum.DrillOperationFHOL;
                    break;
                case OperationTypeEnum.DHol:
                    lineType = LineTypeEnum.DrillOperationDHOL;
                    break;
            }
            return lineType;
        }
    }
}
