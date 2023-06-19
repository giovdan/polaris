namespace Mitrol.Framework.Domain.Enums
{
    public enum WorkSideEnum : int
    {
        /// <summary>
        /// Non definito
        /// </summary>
        DX = 0,

        /// <summary>
        /// Ala destra (esterna)
        ///	ex DA		0
        /// </summary>
        DA = 1,

        /// <summary>
        /// Ala sinistra (esterna)
        ///	ex DB		1
        /// </summary>
        DB = 2,

        /// <summary>
        /// Anima superiore (testa C o D)
        ///	ex DC		2
        /// </summary>
        DC = 3,

        /// <summary>
        /// Anima inferiore
        ///	ex DD		4
        /// </summary>
        DD = 4,

        /// <summary>
        /// Ala destra interna (TS76 or TS77 or TS78)
        ///	ex DAI		8
        /// </summary>
        DAI = 5,

        /// <summary>
        /// Ala sinistra interna (TS76 or TS77 or TS78)
        ///	ex DBI		9
        /// </summary>
        DBI = 6,

        /// <summary>
        /// Ala destra e sinistra (da A a B)
        /// </summary>
        DAB = 7,

        /// <summary>
        /// Ala sinistra e destra (da B ad A)
        /// </summary>
        DBA = 8,

        /// <summary>
        /// Anima superiore ed inferiore (da C a D)
        /// </summary>
        DCD = 9,

        /// <summary>
        /// Anima superiore con testa C
        /// </summary>
        DC_D = 10,

        /// <summary>
        /// Anima superiore con testa D
        ///	ex DCS		3
        /// </summary>
        DC_S = 11,

        /// <summary>
        /// Anima superiore con testa C e D (in doppio)
        ///	ex DCDS	5
        /// </summary>
        DC_DS = 12,

        /// <summary>
        /// Testa della trave (impianti Gantry)
        ///	ex DE		6
        /// </summary>
        DE = 13,

        /// <summary>
        /// Coda della trave (impianti Gantry)
        ///	ex DF		7
        /// </summary>
        DF = 14,

        /// <summary>
        /// Ala destra angolari con testa B (impianti foratrici)
        /// </summary>
        DA_B = 15
    }
}
