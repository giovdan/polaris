namespace Mitrol.Framework.MachineManagement.Domain.Enums
{
    using System;
    using System.ComponentModel;

    [Flags]
    public enum RevisionTypeEnum : int
    {
        [Description("Versione Originale")]
        /// <summary>
        /// Revisione 0 originale (da fornitore)
        /// </summary>
        Original = 0,

        [Description("Revisionata dal Cliente")]
        /// <summary>
        /// Revisione 1 modificato dal cliente
        /// </summary>
        ModifiedByCustomer = 1,

        [Description("Revisionata da Ficep")]
        /// <summary>
        /// Revisione 2 modificato da Ficep
        /// </summary>
        ModifiedByFICEP = 2
    }
}