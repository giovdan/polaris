namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    public enum ParameterAxeTypeEnum : ushort
    {
        [DatabaseDisplayName("MAX")]
        [Description("Parametro asse: Paracarro massimo")]
        MAX = 100,

        [DatabaseDisplayName("MIN")]
        [Description("Parametro asse: Paracarro minimo")]
        MIN = 101,

        [DatabaseDisplayName("VEL")]
        [Description("Parametro asse: Velocità massima")]
        VEL = 102,

        [DatabaseDisplayName("ACC")]
        [Description("Parametro asse: Accelerazione")]
        ACC = 103,

        [DatabaseDisplayName("PRES")]
        [Description("Parametro asse: Quota di preset ciclo zero")]
        PRES = 104,

        [DatabaseDisplayName("OB")]
        [Description("Parametro asse: Origine base")]
        OB = 105,

        [DatabaseDisplayName("OC")]
        [Description("Parametro asse: Origine corrente")]
        OC = 106,

        [DatabaseDisplayName("GAIN")]
        [Description("Parametro asse: Guadagno anello di posizione")]
        GAIN = 107,

        [DatabaseDisplayName("VZER")]
        [Description("Parametro asse: Velocità ciclo zero")]
        VZER = 108,

        [DatabaseDisplayName("EMAX")]
        [Description("Parametro asse: Soglia allarme servo")]
        EMAX = 109,

        [DatabaseDisplayName("EPOS")]
        [Description("Parametro asse: Soglia asse in posizione")]
        EPOS = 110,

        [DatabaseDisplayName("TPOS")]
        [Description("Parametro asse: Tempo filtro asse in posizione")]
        TPOS = 111,

        [DatabaseDisplayName("IMPE")]
        [Description("Parametro asse: Numero impulsi encoder per giro")]
        IMPE = 112,

        [DatabaseDisplayName("DISA")]
        [Description("Parametro asse: Accoppiamento meccanico")]
        DISA = 113,

        [DatabaseDisplayName("REVE")]
        [Description("Parametro asse: Accoppiamento meccanico")]
        REVE = 114,

        [DatabaseDisplayName("VEL8")]
        [Description("Parametro asse: Scala azionamento")]
        VEL8 = 115,

        [DatabaseDisplayName("OFFS")]
        [Description("Parametro asse: Offset anello di velocità")]
        OFFS = 116,

        [DatabaseDisplayName("OZER")]
        [Description("Parametro asse: Ordine ciclo di zero")]
        OZER = 117,

        [DatabaseDisplayName("RDIN")]
        [Description("Parametro asse: Soglia allarme servo dinamica")]
        RDIN = 118,

        [DatabaseDisplayName("B1P")]
        [Description("Parametro asse: Traguardo 1+ asse idraulico")]
        B1P = 119,

        [DatabaseDisplayName("B2P")]
        [Description("Parametro asse: Traguardo 2+ asse idraulico")]
        B2P = 120,

        [DatabaseDisplayName("B3P")]
        [Description("Parametro asse: Traguardo 3+ asse idraulico")]
        B3P = 121,

        [DatabaseDisplayName("BRGP")]
        [Description("Parametro asse: Missione minima positiva")]
        BRGP = 122,

        [DatabaseDisplayName("FD1P")]
        [Description("Parametro asse: Analogico 1+ asse idraulico")]
        FD1P = 123,

        [DatabaseDisplayName("FD2P")]
        [Description("Parametro asse: Analogico 2+ asse idraulico")]
        FD2P = 124,

        [DatabaseDisplayName("FD3P")]
        [Description("Parametro asse: Analogico 3+ asse idraulico")]
        FD3P = 125,

        [DatabaseDisplayName("B1N")]
        [Description("Parametro asse: Traguardo 1- asse idraulico")]
        B1N = 126,

        [DatabaseDisplayName("B2N")]
        [Description("Parametro asse: Traguardo 2- asse idraulico")]
        B2N = 127,

        [DatabaseDisplayName("B3N")]
        [Description("Parametro asse: Traguardo 3- asse idraulico")]
        B3N = 128,

        [DatabaseDisplayName("BRGN")]
        [Description("Parametro asse: Missione minima negativa")]
        BRGN = 129,

        [DatabaseDisplayName("FD1N")]
        [Description("Parametro asse: Analogico 1 - asse idraulico")]
        FD1N = 130,

        [DatabaseDisplayName("FD2N")]
        [Description("Parametro asse: Analogico 2 - asse idraulico")]
        FD2N = 131,

        [DatabaseDisplayName("FD3N")]
        [Description("Parametro asse: Analogico 3 - asse idraulico")]
        FD3N = 132,

        [DatabaseDisplayName("SCAM")]
        [Description("Parametro asse: Soglia rilevamento camma di zero")]
        SCAM = 133,

        [DatabaseDisplayName("TZER")]
        [Description("Parametro asse: Tipo ciclo di zero")]
        TZER = 134,

        [DatabaseDisplayName("SLEW")]
        [Description("Parametro asse: Rampa analogico in anello aperto")]
        SLEW = 135,

        [DatabaseDisplayName("VZRP")]
        [Description("Parametro asse: Velocità ciclo di zero in rapido")]
        VZRP = 136,

        [DatabaseDisplayName("RFFW")]
        [Description("Parametro asse: Percentuale di feed forward")]
        RFFW = 137,

        [DatabaseDisplayName("TFFW")]
        [Description("Parametro asse: Tempo di ritardo feed forward")]
        TFFW = 138,

        [DatabaseDisplayName("TMIN")]
        [Description("Parametro asse: Tempo minimo di accelerazione")]
        TMIN = 139

    }
}