namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

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
        ProgramPieceSequence = 108,
        ProgramLinearNesting = 109,
        ProgramPieceToMeasure = 110,
        ProgramPlateNesting = 111
    }

    public static class EntityTypeExtensions
    {
        public static ProfileTypeEnum ToProfileType(this EntityTypeEnum entityType)
        {
            return (ProfileTypeEnum)entityType;
        }

        public static ToolTypeEnum ToToolType(this EntityTypeEnum entityType)
        {
            ToolTypeEnum toolType = Enums.ToolTypeEnum.NotDefined;
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

            return toolType;
        }
    }
}
