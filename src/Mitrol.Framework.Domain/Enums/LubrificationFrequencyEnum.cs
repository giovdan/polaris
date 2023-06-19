namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;

    using System.ComponentModel;

    /// <summary>
    /// Frequenza di lubrificazione punte (usato nelle unità DRILL)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("FLUB_1")]
    public enum LubrificationFrequencyEnum : int
    {
        /// <summary>
        /// 2 sec
        /// </summary>
        [EnumSerializationName("2sec")]
        [EnumField("Ogni 2 secondi", true, "LBL_FLUB_1")]
        FLUB_1 = 1,

        /// <summary>
        /// 1 sec
        /// </summary>
        [EnumSerializationName("1sec")]
        [EnumField("Ogni 1 secondi", true, "LBL_FLUB_2")]
        FLUB_2 = 2,

        /// <summary>
        /// 0.66 sec
        /// </summary>
        [EnumSerializationName("0.66sec")]
        [EnumField("Ogni 0.66 secondi", true, "LBL_FLUB_3")]
        FLUB_3 = 3,

        /// <summary>
        /// 0.5 sec
        /// </summary>
        [EnumSerializationName("0.5sec")]
        [EnumField("Ogni 0.5 secondi", true, "LBL_FLUB_4")]
        FLUB_4 = 4,

        /// <summary>
        /// 0.4 sec
        /// </summary>
        [EnumSerializationName("0.4sec")]
        [EnumField("Ogni 0.4 secondi", true, "LBL_FLUB_5")]
        FLUB_5 = 5,

        /// <summary>
        /// 0.33 sec
        /// </summary>
        [EnumSerializationName("0.33sec")]
        [EnumField("Ogni 0.33 secondi", true, "LBL_FLUB_6")]
        FLUB_6 = 6,

        /// <summary>
        /// 3 sec
        /// </summary>
        [EnumSerializationName("3sec")]
        [EnumField("Ogni 3 secondi", true, "LBL_FLUB_7")]
        FLUB_7 = 7,

        /// <summary>
        /// 4 sec
        /// </summary>
        [EnumSerializationName("4sec")]
        [EnumField("Ogni 4 secondi", true, "LBL_FLUB_8")]
        FLUB_8 = 8
    }
}
