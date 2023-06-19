namespace Mitrol.Framework.Domain.Enums
{
    using System;

    [Flags()]
    public enum LineStatusEnum
    {
        /// <summary>
        /// Linea senza marker (da eseguire sempre)
        /// </summary>
        WithoutMarker = 0,

        /// <summary>
        /// Linea con marker da eseguire
        /// </summary>
        ToBeExecuted = 1,

        /// <summary>
        /// Linea con marker eseguita OK
        /// </summary>
        Executed = 2,

        /// <summary>
        /// Linea con marker eseguita NOT OK
        /// </summary>
        Failed = 3,
    }
}
