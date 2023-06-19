using Mitrol.Framework.Domain.Attributes;

namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Tipologia di impianto
    /// </summary>
    public enum MachineTypeEnum : byte
    {
        /// <summary>
        /// Impianto base (segatrice/robot/sabbiatrice standalone)
        /// </summary>
        [DatabaseDisplayName("Fora")]
        TANG_BASE = 0,

        /// <summary>
        /// punzonatrice/foratrice piastre 803 - 1001 - 1003
        /// </summary>
        [DatabaseDisplayName("Piastrina")]
        TANG_PIASTRINA = 1,

        /// <summary>
        /// taglio con calibro
        /// </summary>
        [DatabaseDisplayName("Calibro")]
        TANG_CALIBRO = 5,

        /// <summary>
        /// foratrice piastre F401P
        /// </summary>
        [DatabaseDisplayName("F401P")]
        TANG_F401P = 16,

        /// <summary>
        /// punzonatrice travi 306PS
        /// </summary>
        [DatabaseDisplayName("306PS")]
        TANG_306PS = 17,

        /// <summary>
        /// punzonatrice/foratrice 504PS o TipoD8
        /// </summary>
        [DatabaseDisplayName("504PS")]
        TANG_504PS = 19,

        /// <summary>
        /// Victory11 / Excalibur12
        /// </summary>
        [DatabaseDisplayName("V11")]
        TANG_V11 = 26,

        /// <summary>
        /// angolari A166
        /// </summary>
        [DatabaseDisplayName("A166")]
        TANG_A166 = 27,

        /// <summary>
        /// punzonatrice/foratrice/taglio piastre TipoC
        /// </summary>
        [DatabaseDisplayName("TipoC")]
        TANG_TIPOC = 28,

        /// <summary>
        /// foratrice/taglio piastre TipoA con carrino
        /// </summary>
        [DatabaseDisplayName("TipoA")]
        TANG_TIPOA31 = 29,

        /// <summary>
        /// GEMINI con CNC Fanuc o con CNC Mitrol (Ficep France)
        /// </summary>
        [DatabaseDisplayName("Gemini")]
        TANG_GEMINI = 33,

        /// <summary>
        /// Piastre TipoB 254
        /// </summary>
        [DatabaseDisplayName("TipoB254")]
        TANG_TIPOB254 = 34,

        /// <summary>
        /// Gantry Travi
        /// </summary>
        [DatabaseDisplayName("GantryT")]
        TANG_GANTRY_T = 35,

        /// <summary>
        /// foratrice/taglio piastre TipoA25 Lateral Grip
        /// </summary>
        [DatabaseDisplayName("TipoALG")]
        TANG_TIPOALG = 36,

        /// <summary>
        /// Endeavour
        /// </summary>
        [DatabaseDisplayName("Endeavour")]
        TANG_ENDEAVOUR = 37,

        /// <summary>
        /// Foratrice angolari HPS
        /// </summary>
        [DatabaseDisplayName("HPS")]
        TANG_HP_S = 38,

        /// <summary>
        /// TIPO_G con CNC Fanuc
        /// </summary>
        [DatabaseDisplayName("TipoG")]
        TANG_TIPOG = 39,

        /// <summary>
        /// Gantry Travi Orient
        /// </summary>
        [DatabaseDisplayName("Orient")]
        TANG_GANTRY_T_O = 40
    }
}