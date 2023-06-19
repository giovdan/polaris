namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public enum EntityTypeEnum
    {
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
        ToolTS15 = 15,
        ToolTS16 = 16,
        ToolTS17 = 17,
        ToolTS18 = 18,
        ToolTS19 = 19,
        ToolTS20 = 20,
        ToolTS32 = 32,
        ToolTS33 = 33,
        ToolTS34 = 34,
        ToolTS35 = 35,
        ToolTS36 = 36,
        ToolTS38 = 38,
        ToolTS39 = 39,
        ToolTS40 = 40,
        ToolTS41 = 41,
        ToolTS50 = 50,
        ToolTS51 = 51,
        ToolTS52 = 52,
        ToolTS53 = 53,
        ToolTS54 = 54,
        ToolTS55 = 55,
        ToolTS56 = 56,
        ToolTS57 = 57,
        ToolTS61 = 61,
        ToolTS62 = 62,
        ToolTS68 = 68,
        ToolTS69 = 69,
        ToolTS70 = 70,
        ToolTS71 = 71,
        ToolTS73 = 73,
        ToolTS74 = 74,
        ToolTS75 = 75,
        ToolTS76 = 76,
        ToolTS77 = 77,
        ToolTS78 = 78,
        ToolTS79 = 79,
        ToolTS80 = 80,
        ToolTS87 = 87,
        ToolTS88 = 88,
        ToolTS89 = 89,
        ToolTS51XPR = 90,
        ToolTS51HPR = 91,
        ToolTS53XPR = 92,
        ToolTS53HPR = 93,
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
        ToolRangeTS51HPR = 180,
        ToolRangeTS51XPR = 181,
        ToolRangeTS53HPR = 182,
        ToolRangeTS53XPR = 183,
        ToolSubRangeBevelTS51 = 184,
        ToolSubRangeBevelTS52 = 185,
        ToolSubRangeBevelTS53 = 186,
        ToolSubRangeBevelTS54 = 187,
        ToolSubRangeTrueHoleTS51 = 188,
        ToolSubRangeTrueHoleTS51HPR = 189,
        ToolSubRangeTrueHoleTS51XPR = 190,
        ToolSubRangeTrueHoleTS53 = 191,
        ToolSubRangeTrueHoleTS53HPR = 192,
        ToolSubRangeTrueHoleTS53XPR = 193,
        ToolRangeMarkingTS51 = 194,
        ToolRangeMarkingTS51XPR = 195,
        ToolRangeMarkingTS53 = 196,
        ToolRangeMarkingTS53XPR = 197,
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
                EntityTypeEnum.ToolTS89 ,
                EntityTypeEnum.ToolTS51XPR,
                EntityTypeEnum.ToolTS51HPR,
                EntityTypeEnum.ToolTS53XPR,
                EntityTypeEnum.ToolTS53HPR
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
                switch (entityType)
                {
                    case EntityTypeEnum.ToolTS51HPR:
                    case EntityTypeEnum.ToolTS51XPR:
                        toolType = ToolTypeEnum.TS51;
                        break;
                    case EntityTypeEnum.ToolTS53HPR:
                    case EntityTypeEnum.ToolTS53XPR:
                        toolType = ToolTypeEnum.TS53;
                        break;
                    default:
                        toolType = (ToolTypeEnum)entityType;
                        break;
                }
            }


            return toolType;
        }
    }
}
