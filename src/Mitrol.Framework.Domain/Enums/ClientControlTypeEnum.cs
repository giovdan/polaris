namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System;
    using System.ComponentModel;

    [Flags]
    public enum ClientControlTypeEnum : int
    {
        [DatabaseDisplayName("None")]
        [Description("Nessun controllo associato")]
        None = 1,
        [DatabaseDisplayName("Edit")]
        [Description("controllo di Tipo TextBox")]
        Edit = 2,
        [DatabaseDisplayName("Label")]
        [Description("controllo di Tipo Label")]
        Label = 4,
        [DatabaseDisplayName("Combo")]
        [Description("Controllo di tipo ComboBox")]
        Combo = 8,
        [DatabaseDisplayName("ListBox")]
        [Description("Controllo di tipo ListBox")]
        ListBox = 16,
        [DatabaseDisplayName("Check")]
        [Description("Controllo di tipo CheckBox")]
        Check = 32,
        [DatabaseDisplayName("Override")]
        [Description("Controllo di tipo Corrector")]
        Override = 64,
        [DatabaseDisplayName("Image")]
        [Description("Controllo di tipo immagine")]
        Image = 128,
        [DatabaseDisplayName("MultiValue")]
        [Description("Controllo di tipo MultiValue")]
        MultiValue = 256
    }


}