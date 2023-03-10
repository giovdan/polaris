namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

    public enum EventTypeEnum : int
    {
        [Description("Evento di informazione generica")]
        Information = 1,
        [Description("Evento di Login")]
        Login = 2,
        [Description("Warning generico")]
        Warning = 4,
        [Description("Evento di errore")]
        Error = 8,
        [Description("Evento di Logout")]
        Logout = 16,
        [Description("Evento di Boot")]
        Boot = 32,
        [Description("Evento di Export")]
        Export = 64,
        [Description("Evento di Import")]
        Import = 128,
        [Description("Evento di creazione")]
        Create = 256,
        [Description("Evento di aggiornamento")]
        Update = 512,
        [Description("Evento di cancellazione")]
        Remove = 1024,
        [Description("Evento di debug")]
        Debug = 2048,
        [Description("Evento di clonazione")]
        Clone = 4096
    }
}
