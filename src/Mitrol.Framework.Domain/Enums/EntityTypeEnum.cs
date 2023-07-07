namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public enum EntityTypeEnum
    {
        [EnumSerializationName("NotDefined")]
        NotDefined = 0,
        ProfileL = 1,
        ProfileV = 2,
        ProfileB = 3,
        ProfileI = 4,
        ProfileD = 5,
        ProfileT = 6,
        ProfileU = 7,
        ProfileQ = 8,
        ProfileC = 9,
        ProfileF = 11,
        ProfileN = 12,
        ProfileP = 13,
        ProfileR = 14,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [EnumSerializationName("PUNCH_15")]
        ToolTS15 = 15,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [EnumSerializationName("PUNCH_MK")]
        ToolTS16 = 16,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [EnumSerializationName("PUNCH_NOTCH")]
        ToolTS17 = 17,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [EnumSerializationName("PUNCH_18")]
        ToolTS18 = 18,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [EnumSerializationName("PUNCH_19")]
        ToolTS19 = 19,
        [PlantUnit(PlantUnitEnum.PunchingMachine)]
        [EnumSerializationName("PUNCH_POINT")]
        ToolTS20 = 20,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("DRILL_32")]
        ToolTS32 = 32,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("DRILL_33")]
        ToolTS33 = 33,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("DEBURR")]
        ToolTS34 = 34,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("FLARE")]
        ToolTS35 = 35,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("DEBURR_36")]
        ToolTS36 = 36,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("BORE")]
        ToolTS38 = 38,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("POINT")]
        ToolTS39 = 39,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("DRILL_40")]
        ToolTS40 = 40,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("TAP")]
        ToolTS41 = 41,
        [PlantUnit(PlantUnitEnum.Shear)]
        [EnumSerializationName("SHEAR")]
        ToolTS50 = 50,
        [PlantUnit(PlantUnitEnum.PlasmaTorch)]
        [EnumSerializationName("PLA")]
        ToolTS51 = 51,
        [PlantUnit(PlantUnitEnum.OxyCutTorch)]
        [EnumSerializationName("OXY")]
        ToolTS52 = 52,
        [PlantUnit(PlantUnitEnum.PlasmaTorch)]
        [EnumSerializationName("PLA_MK")]
        ToolTS53 = 53,
        [PlantUnit(PlantUnitEnum.OxyCutTorch)]
        [EnumSerializationName("OXY_BEV")]
        ToolTS54 = 54,
        [PlantUnit(PlantUnitEnum.SawingMachine)]
        [EnumSerializationName("DISC_BLADE")]
        ToolTS55 = 55,
        [PlantUnit(PlantUnitEnum.SawingMachine)]
        [EnumSerializationName("BAND_BLADE")]
        ToolTS56 = 56,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("ROTATING_BLADE")]
        ToolTS57 = 57,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("MILL")]
        ToolTS61 = 61,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("POCKET")]
        ToolTS62 = 62,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("POCKET_68")]
        ToolTS68 = 68,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("POCKET_69")]
        ToolTS69 = 69,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("SENSE_70")]
        ToolTS70 = 70,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("TAP_71")]
        ToolTS71 = 71,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("DFLARE")]
        ToolTS73 = 73,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("DDEBURR")]
        ToolTS74 = 74,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("POCKET_75")]
        ToolTS75 = 75,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("POCKET_76")]
        ToolTS76 = 76,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("PENCIL_77")]
        ToolTS77 = 77,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("PENCIL_78")]
        ToolTS78 = 78,
        [PlantUnit(PlantUnitEnum.DrillingMachine)]
        [EnumSerializationName("POCKET_79")]
        ToolTS79 = 79,
        [PlantUnit(PlantUnitEnum.PalpingMachine)]
        [EnumSerializationName("SENSE_80")]
        ToolTS80 = 80,
        [PlantUnit(PlantUnitEnum.CharMarker)]
        [EnumSerializationName("MARK_87")]
        ToolTS87 = 87,
        [PlantUnit(PlantUnitEnum.CharMarker)]
        [EnumSerializationName("MARK_88")]
        ToolTS88 = 88,
        [PlantUnit(PlantUnitEnum.InkJetMarker)]
        [EnumSerializationName("MARK_JET")]
        ToolTS89 = 89,
        StockProfileL = 94,
        StockProfileV = 95,
        StockProfileB = 96,
        StockProfileI = 97,
        StockProfileD = 98,
        StockProfileT = 99,
        StockProfileU = 100,
        StockProfileQ = 101,
        StockProfileC = 102,
        StockProfileF = 103,
        StockProfileN = 104,
        StockProfileP = 105,
        StockProfileR = 106,
        ToolHolder = 107,
        PieceProfileL = 108,
        PieceProfileV = 109,
        PieceProfileB = 110,
        PieceProfileI = 111,
        PieceProfileD = 112,
        PieceProfileT = 113,
        PieceProfileU = 114,
        PieceProfileQ = 115,
        PieceProfileC = 116,
        PieceProfileF = 117,
        PieceProfileN = 118,
        PieceProfileP = 119,
        PieceProfileR = 120,
        ProgramElement = 125,
        ProgramCode = 128,
        ToolRangeTS32 = 132,
        ToolRangeTS33 = 133,
        ToolRangeTS34 = 134,
        ToolRangeTS35 = 135,
        ToolRangeTS36 = 136,
        ToolRangeTS38 = 138,
        ToolRangeTS39 = 139,
        ToolRangeTS40 = 140,
        ToolRangeTS41 = 141,
        ToolRangeTS51 = 151,
        ToolRangeTS52 = 152,
        ToolRangeTS53 = 153,
        ToolRangeTS54 = 154,
        ToolRangeTS55 = 155,
        ToolRangeTS56 = 156,
        ToolRangeTS57 = 157,
        ToolRangeTS61 = 161,
        ToolRangeTS62 = 162,
        ToolRangeTS68 = 168,
        ToolRangeTS69 = 169,
        ToolRangeTS70 = 170,
        ToolRangeTS71 = 171,
        ToolRangeTS73 = 173,
        ToolRangeTS74 = 174,
        ToolRangeTS75 = 175,
        ToolRangeTS76 = 176,
        ToolRangeTS77 = 177,
        ToolRangeTS78 = 178,
        ToolRangeTS79 = 179,
        ToolSubRangeBevelTS51 = 184,
        ToolSubRangeBevelTS52 = 185,
        ToolSubRangeBevelTS53 = 186,
        ToolSubRangeBevelTS54 = 187,
        ToolSubRangeTrueHoleTS51 = 188,
        ToolSubRangeTrueHoleTS53 = 191,
        ToolRangeMarkingTS51 = 194,
        ToolRangeMarkingTS53 = 196,
        ProgramProfileL = 198,
        ProgramProfileV = 199,
        ProgramProfileB = 200,
        ProgramProfileI = 201,
        ProgramProfileD = 202,
        ProgramProfileT = 203,
        ProgramProfileU = 204,
        ProgramProfileQ = 205,
        ProgramProfileC = 206,
        ProgramProfileF = 207,
        ProgramProfileN = 208,
        ProgramProfileP = 209,
        ProgramProfileR = 210,
        ProductionRowProfileL = 211,
        ProductionRowProfileV = 212,
        ProductionRowProfileB = 213,
        ProductionRowProfileI = 214,
        ProductionRowProfileD = 215,
        ProductionRowProfileT = 216,
        ProductionRowProfileU = 217,
        ProductionRowProfileQ = 218,
        ProductionRowProfileC = 219,
        ProductionRowProfileF = 220,
        ProductionRowProfileN = 221,
        ProductionRowProfileP = 222,
        ProductionRowProfileR = 223,
        OperationNode = 224,
        OperationHol = 225,
        OperationPoint = 226,
        OperationSlot = 227,
        OperationTap = 228,
        OperationPathC = 229,
        OperationDeburr = 230,
        OperationBore = 231,
        OperationFlare = 232,
        OperationShape = 233,
        OperationMill = 234,
        OperationMCut = 235,
        OperationNotch = 236,
        OperationShf = 237,
        OperationMisc = 238,
        OperationStiff = 239,
        OperationFHol = 240,
        OperationDHol = 241,
        OperationPathVertex = 242,
        OperationPathS = 243,
        OperationPathM = 244,
        OperationCope = 245,
        OperationMark = 246,
        OperationVertex = 247,
        OperationToolTechnology = 248,
        OperationBHol = 249,
        OperationMSaw = 250,
        Material = 251
    }

    public static class EntityTypeExtensions
    {
        public static ProfileTypeEnum ToProfileType(this EntityTypeEnum entityType)
        {
            return (ProfileTypeEnum)entityType;
        }

        public static string DisplayName(this EntityTypeEnum entityType)
        {
            return entityType.ToToolType().ToString();
        }



        public static IEnumerable<EntityTypeEnum> GetToolEntityTypes()
        {
            return new[]
            {
                EntityTypeEnum.ToolTS15,
                EntityTypeEnum.ToolTS16,
                EntityTypeEnum.ToolTS17,
                EntityTypeEnum.ToolTS18,
                EntityTypeEnum.ToolTS19,
                EntityTypeEnum.ToolTS20,
                EntityTypeEnum.ToolTS32 ,
                EntityTypeEnum.ToolTS33 ,
                EntityTypeEnum.ToolTS34 ,
                EntityTypeEnum.ToolTS35 ,
                EntityTypeEnum.ToolTS36 ,
                EntityTypeEnum.ToolTS38 ,
                EntityTypeEnum.ToolTS39 ,
                EntityTypeEnum.ToolTS40 ,
                EntityTypeEnum.ToolTS41 ,
                EntityTypeEnum.ToolTS50 ,
                EntityTypeEnum.ToolTS51 ,
                EntityTypeEnum.ToolTS52 ,
                EntityTypeEnum.ToolTS53 ,
                EntityTypeEnum.ToolTS54 ,
                EntityTypeEnum.ToolTS55 ,
                EntityTypeEnum.ToolTS56 ,
                EntityTypeEnum.ToolTS57 ,
                EntityTypeEnum.ToolTS61 ,
                EntityTypeEnum.ToolTS62 ,
                EntityTypeEnum.ToolTS68 ,
                EntityTypeEnum.ToolTS69 ,
                EntityTypeEnum.ToolTS70 ,
                EntityTypeEnum.ToolTS71 ,
                EntityTypeEnum.ToolTS73 ,
                EntityTypeEnum.ToolTS74 ,
                EntityTypeEnum.ToolTS75 ,
                EntityTypeEnum.ToolTS76 ,
                EntityTypeEnum.ToolTS77 ,
                EntityTypeEnum.ToolTS78 ,
                EntityTypeEnum.ToolTS79 ,
                EntityTypeEnum.ToolTS80 ,
                EntityTypeEnum.ToolTS87 ,
                EntityTypeEnum.ToolTS88 ,
                EntityTypeEnum.ToolTS89 
            };
        }

        /// <summary>
        /// Get related plant unit from entityType
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static PlantUnitEnum GetPlantUnit(this EntityTypeEnum entityType)
        {
            var toolType = entityType.ToToolType();
            var plantUnit = PlantUnitEnum.None;
            if (toolType != ToolTypeEnum.NotDefined)
            {
                plantUnit = toolType.GetEnumAttribute<PlantUnitAttribute>()?.PlantUnit ?? PlantUnitEnum.None;
            }

            return plantUnit;
        }

        /// <summary>
        /// Get related parent type from entitytype
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static ParentTypeEnum ToParentType(this EntityTypeEnum entityType)
        {
            ParentTypeEnum parentType = ParentTypeEnum.None;
            if (entityType.ToToolType() != ToolTypeEnum.NotDefined)
            {
                parentType = ParentTypeEnum.Tool;
            }

            return parentType;
        }

        /// <summary>
        /// Get related tool type from entityType
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static ToolTypeEnum ToToolType(this EntityTypeEnum entityType)
        {
            var definedToolTypes = GetToolEntityTypes()
                        .ToList();
            ToolTypeEnum toolType = ToolTypeEnum.NotDefined;
            if (definedToolTypes.Contains(entityType))
            {
                toolType = (ToolTypeEnum)entityType;
            }

            return toolType;
        }
    }
}
