namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    [Flags]
    public enum ParentTypeEnum : int
    {
        None = 0,

        [DatabaseDisplayName("PR")]
        [Description("Profilo")]
        Profile = 1,

        [Description("Utensili")]
        [DatabaseDisplayName("TO")]
        Tool = 2,

        [Description("Tabelle")]
        [DatabaseDisplayName("TR")]
        ToolRange = 4,

        [Description("ToolHolders")]
        [DatabaseDisplayName("TH")]
        ToolHolder = 8,

        [Description("Tabelle Speciali BEVEL")]
        [DatabaseDisplayName("TSBV")]
        ToolSubRangeBevel = 16,

        [Description("Tabelle Speciali TRUEHOLE")]
        [DatabaseDisplayName("TSTH")]
        ToolSubRangeTrueHole = 32,

        [Description("Tabelle Speciali MARKING")]
        [DatabaseDisplayName("TSMK")]
        ToolRangeMarking = 64,

        [Description("Codice Programma")]
        [DatabaseDisplayName("PRGC")]
        ProgramCode = 128,

        [Description("Programma")]
        [DatabaseDisplayName("PRG")]
        ProgramItem = 256,

        [Description("ProgramPiece")]
        [DatabaseDisplayName("PPI")]
        ProgramPiece = 512,

        [Description("Pezzo")]
        [DatabaseDisplayName("PIE")]
        Piece = 1024,

        [Description("Elemento di magazzino")]
        [DatabaseDisplayName("STK")]
        StockItem = 2048,

        [Description("Tipo di operazione")]
        [DatabaseDisplayName("OPT")]
        OperationType = 4096,

        [Description("Riga di produzione")]
        [DatabaseDisplayName("PROW")]
        ProductionRow = 8192,

        [Description("Materiale")]
        [DatabaseDisplayName("MTR")]
        Material = 16384,

        [Description("OperationAdditionalItem")]
        [DatabaseDisplayName("OAI")]
        OperationAdditionalItem = 65536
    }

    public static class ParentTypeEnumExtensions
    {
        public static IEnumerable<EntityTypeEnum> GetEntityTypes(this ParentTypeEnum parentType)
        {
            var entityTypes = Enumerable.Empty<EntityTypeEnum>();

            switch (parentType)
            {
                case ParentTypeEnum.StockItem:
                    {
                        entityTypes = new[]
                        {
                            EntityTypeEnum.StockProfileB,
                            EntityTypeEnum.StockProfileC,
                            EntityTypeEnum.StockProfileD,
                            EntityTypeEnum.StockProfileF,
                            EntityTypeEnum.StockProfileI,
                            EntityTypeEnum.StockProfileL,
                            EntityTypeEnum.StockProfileN,
                            EntityTypeEnum.StockProfileP,
                            EntityTypeEnum.StockProfileQ,
                            EntityTypeEnum.StockProfileR,
                            EntityTypeEnum.StockProfileT,
                            EntityTypeEnum.StockProfileU,
                            EntityTypeEnum.StockProfileV
                        };
                    }
                    break;

            }
            return entityTypes;
        }
        public static ToolRangeTypeEnum GetToolRangeType(this ParentTypeEnum parentType, PlantUnitEnum plantUnit)
        {
            var toolRangeType = ToolRangeTypeEnum.None;

            switch(parentType)
            {
                case ParentTypeEnum.ToolRange:
                    {
                        switch(plantUnit)
                        {
                            case PlantUnitEnum.DrillingMachine:
                                toolRangeType = ToolRangeTypeEnum.Drill;
                                break;
                            case PlantUnitEnum.PlasmaTorch:
                            case PlantUnitEnum.OxyCutTorch:
                                toolRangeType = ToolRangeTypeEnum.Cut;
                                break;
                            case PlantUnitEnum.SawingMachine:
                                toolRangeType = ToolRangeTypeEnum.Saw;
                                break;
                        }
                    }
                    break;
                case ParentTypeEnum.ToolRangeMarking:
                    {
                        toolRangeType = ToolRangeTypeEnum.Mark;
                    }
                    break;
                case ParentTypeEnum.ToolSubRangeBevel:
                    {
                        toolRangeType = ToolRangeTypeEnum.Bevel;
                    }
                    break;
                case ParentTypeEnum.ToolSubRangeTrueHole:
                    {
                        toolRangeType = ToolRangeTypeEnum.TrueHole;
                    }
                    break;
            }

            return toolRangeType;
        }

    }
        
}