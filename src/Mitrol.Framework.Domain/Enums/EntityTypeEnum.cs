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
        ToolHolder = 10,
        ProfileF = 11,
        ProfileN = 12,
        ProfileP = 13,
        ProfileR = 14,
        StockProfileL = 15,
        StockProfileV = 16,
        StockProfileB = 17,
        StockProfileI = 18,
        StockProfileD = 19,
        StockProfileT = 20,
        StockProfileU = 21,
        StockProfileQ = 22,
        StockProfileC = 23,
        StockProfileF = 24,
        StockProfileN = 25,
        StockProfileP = 26,
        StockProfileR = 27,
        ProgramPieceSequence = 93,
        ProgramLinearNesting = 95,
        ProgramPieceToMeasure = 97,
        ProgramPlateNesting = 98
    }

    public static class EntityTypeExtensions
    {
        public static ProfileTypeEnum ToEntityType(this EntityTypeEnum profileType)
        {
            return (ProfileTypeEnum)profileType;
        }
    }
}
