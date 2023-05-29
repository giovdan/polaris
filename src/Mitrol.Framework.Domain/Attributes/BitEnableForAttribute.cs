namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    /// <summary>
    /// Descrive l'abilitazione di un tool per un'unità.
    /// </summary>
    /// <remarks>
    /// L'abilitazione di un tool per una specifica unità è rappresentato da una maschera a bit.
    /// Ogni bit rappresenta l'associazione con un'unità.
    /// In base alla configurazione macchina mostra solamente i flag di abilitazione che possono essere gestiti (unità presenti).
    /// </remarks>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class BitEnableFor : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        public BitEnableFor(UnitEnum unit) => Unit = unit;

        /// <summary>
        /// 
        /// </summary>
        public UnitEnum Unit { get; set; }
    }
}
