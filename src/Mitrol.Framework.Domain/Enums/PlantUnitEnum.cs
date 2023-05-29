namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System;
    using System.ComponentModel;

    [Flags]
    public enum PlantUnitEnum : int
    {

        [DatabaseDisplayName("NONE")]
        [Description("Nessuna unità")]
        None = 0,

        [DatabaseDisplayName("DRILL")]
        [Description("Foratrice")]
        //[RelatedToolRangeType(ToolRangeTypeEnum.Drill)]
        DrillingMachine = 1,

        [DatabaseDisplayName("PLASMA")]
        [Description("Torcia Plasma")]
        //[RelatedToolRangeType(ToolRangeTypeEnum.Cut)]
        //[RelatedToolRangeType(ToolRangeTypeEnum.Mark, SubRangeTypeEnum.Mark)]
        //[RelatedToolRangeType(ToolRangeTypeEnum.TrueHole, SubRangeTypeEnum.TrueHole)]
        //[RelatedToolRangeType(ToolRangeTypeEnum.Bevel, SubRangeTypeEnum.Bevel)]
        PlasmaTorch = 2,

        [DatabaseDisplayName("OXY")]
        [Description("Torcia Ossitaglio")]
        //[RelatedToolRangeType(ToolRangeTypeEnum.Cut)]
        //[RelatedToolRangeType(ToolRangeTypeEnum.Bevel, SubRangeTypeEnum.Bevel)]
        OxyCutTorch = 4,

        [DatabaseDisplayName("PUNCH")]
        [Description("Punzonatrice")]
        PunchingMachine = 8,

        [DatabaseDisplayName("SHEAR")]
        [Description("Cesoia")]
        Shear = 16,

        [DatabaseDisplayName("INKJET_MARK")]
        [Description("Marcatrice a getto d'inchiostro")]
        InkJetMarker = 32,

        [DatabaseDisplayName("CHAR_MARK")]
        [Description("Marcatrice a carattere")]
        CharMarker = 64,

        [DatabaseDisplayName("NOTCH")]
        [Description("Stozzatrice")]
        ShaperCutter = 128,

        [DatabaseDisplayName("TOUCH")]
        [Description("Palpatore")]
        PalpingMachine = 256,

        [DatabaseDisplayName("SAW")]
        [Description("Unità segatrice")]
        //[RelatedToolRangeType(ToolRangeTypeEnum.Saw)]
        SawingMachine = 512,

        All = DrillingMachine | PlasmaTorch | OxyCutTorch | PunchingMachine
            | Shear | InkJetMarker | CharMarker | ShaperCutter | PalpingMachine
            | SawingMachine
    }

}