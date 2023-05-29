namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Specifica il nome visualizzato per qualsiasi elemento dell'applicazione.
    /// </summary>
    /// <remarks>
    /// Questo attibuto è stato definito dopo aver riscontrato una differenza nell'applicabilità
    /// dell'attributo <see cref="DisplayNameAttribute"/>.
    /// In un assembly con target .NET Core è possibile applicare questo attributo
    /// ai membri di un enumerato, non è invece possibile per gli assembly
    /// .NET Framework e .NET Standard.
    /// </remarks>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class DatabaseDisplayNameAttribute : DisplayNameAttribute
    {
        public DatabaseDisplayNameEnum DisplayNameEnum { get; set; }
        public DatabaseDisplayNameAttribute() : base() { }
        public DatabaseDisplayNameAttribute(string displayName) : base(displayName) { }
        public DatabaseDisplayNameAttribute(DatabaseDisplayNameEnum displayNameEnum) : base(displayNameEnum.ToString())
        {
            DisplayNameEnum = displayNameEnum;
        }
    }
}
