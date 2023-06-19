namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

    public enum ToolCategoryEnum
    {
        /// <summary>
        /// Foratura
        /// </summary>
        [Description("Utensili di Foratura")]
        Drill = 1,

        /// <summary>
        /// Fresatura
        /// </summary>
        [Description("Utensili di Fresatura")]
        Mill = 2,

        /// <summary>
        /// Punzonatura
        /// </summary>
        [Description("Utensili di Punzonatura")]
        Punch = 4,

        /// <summary>
        /// Taglio
        /// </summary>
        [Description("Utensili di Taglio")]
        Cut = 8,

        /// <summary>
        /// Stozzatura
        /// </summary>
        [Description("Utensili di Stozzatura")]
        Notch = 16,

        /// <summary>
        /// Marcatura
        /// </summary>
        [Description("Utensili di Marcatura")]
        Mark = 32,

        /// <summary>
        /// Palpatura
        /// </summary>
        [Description("Utensili di Palpatura")]
        Sense = 64,

        [Description("Utensili di Maschiatura")]
        Tapping = 128,

        /// <summary>
        /// Utensili generici
        /// </summary>
        [Description("Utensili generici")]
        Generic = 256,

        [Description("Utensili per segatrice")]
        Saw = 512,
    }
}