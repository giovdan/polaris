namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Enumerato che definisce i gruppi di sistema
    /// </summary>
    public enum SystemGroupEnum
    {
        [DatabaseDisplayName("ADMINS")]
        [Description("Gruppo Amministratori FICEP")]
        ADMINS = 1,

        [DatabaseDisplayName("SUPERUSERS")]
        [Description("Gruppo Amministratori IMPIANTO")]
        SUPERUSERS = 2,

        [DatabaseDisplayName("USERS")]
        [Description("Gruppo Operatori IMPIANTO")]
        USERS = 3,

        [DatabaseDisplayName("BOOTUSERS")]
        [Description("Gruppo utenti per boot servizi")]
        BOOTUSERS = 4,

        [DatabaseDisplayName("STEELPROJECTS")]
        [Description("Gruppo utenti STEELPROJECTS")]
        STEELPROJECTS = 5,

        [DatabaseDisplayName("FICEP")]
        [Description("Gruppo utenti FICEP")]
        FICEP = 6,
        NONE = 999, //Gruppo usato per indicare attributi/parametri READ ONLY
    }
}
