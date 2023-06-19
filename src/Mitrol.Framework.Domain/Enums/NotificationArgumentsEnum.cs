namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum NotificationArgumentsEnum
    {
        NotificationNumber = 1,

        /// <summary>
        /// Numero del parametro dell'allarme
        /// </summary>
        /// <remarks>
        /// Equivalente alla macro %P di Pegaso
        /// </remarks>
        NotificationParameter = 2,

        /// <summary>
        /// Nome dell' asse 'n' dove 'n' è il parametro dell'allarme
        /// </summary>
        /// <remarks>
        /// Equivalente alla macro %AP di Pegaso
        /// </remarks>
        [EnumSerializationName("AP")]
        AxisNameByIndex = 3,

        /// <summary>
        /// Numero e nome del nodo associato all'asse di indice 'n'
        /// </summary>
        /// <remarks>
        /// Equivalente alla macro %AN di Pegaso
        /// </remarks>
        [EnumSerializationName("AN")]
        NodeInfoByAxisIndex = 4,

        /// <summary>
        /// Nome del nodo EtherCat 'n' dove 'n' è il parametro dell'allarme.
        /// </summary>
        /// <remarks>
        /// Equivalente alla macro %NE di Pegaso
        /// </remarks>
        [EnumSerializationName("NE")]
        EtherCatNodeNameById = 5,

        /// <summary>
        /// Nome del nodo CanBus 'n' dove 'n' è il parametro dell'allarme.
        /// </summary>
        /// <remarks>
        /// Equivalente alla macro %NP di Pegaso
        /// </remarks>
        [EnumSerializationName("NP")]
        CanBusNodeNameById = 6,
    }
}
