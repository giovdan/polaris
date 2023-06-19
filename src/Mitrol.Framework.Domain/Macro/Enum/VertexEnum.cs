

namespace Mitrol.Framework.Domain.Macro.Enum
{
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel;

    /// <summary>
    /// Vertice longitudinale della figura definita dalla macro
    /// </summary>
    public enum LongitudinalVertexPositionEnum
    {
        // Posizione non definita
        Undefined = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("Posizione iniziale")]
        [EnumSerializationName("Initial")]
        Initial = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("Posizione finale")]
        [EnumSerializationName("Final")]
        Final = 2
     
    }

    /// <summary>
    /// Vertice trasversale della figura definita dalla macro
    /// </summary>
    public enum TransverseVertexPositionEnum
    {
        // Posizione non definita
        Undefined = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("Posizione in alto")]
        [EnumSerializationName("Top")]
        Top = 1,

        /// <summary>
        /// 
        /// </summary>
        [Description("Posizione in basso")]
        [EnumSerializationName("Bottom")]
        Bottom = 2
            
    }
}
