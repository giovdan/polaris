namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologia di stock
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Stock")]
    public enum StockTypeEnum
    {
        /// <summary>
        /// Materiale nuovo
        /// </summary>
        [EnumSerializationName("Stock")]
        [EnumField("Materiale nuovo", true, "LBL_STOCKTYPE_STOCK")]
        Stock = 1,

        /// <summary>
        /// Rimanenza da lavoriazioni precedenti
        /// </summary>
        [EnumSerializationName("FreeStock")]
        [EnumField("Rimanenza da lavoriazioni precedenti", true, "LBL_STOCKTYPE_FREESTOCK")]
        FreeStock = 2,
    }
}
