namespace Mitrol.Framework.Domain.Configuration.Enums
{
    // Configurazione tipo di foratrici angolari (TANG 2 3 4 8 27)
    public enum ForAngEnum
    {
        UF11_VW = 0,        // UF11 assi V-W
        UF11_YZ = 1,        // UF11 stesso asse punzoni Y-Z
        UF11_22_D = 2,      // UF11/22 LFA40 tripla truschini discreti
        UF11_22_P = 3,      // UF11/22 LFA40 tripla assi proporzionali
        UF40_PIA = 4,       // UF40 P1104
        UF11_22_M = 5       // A166T con unità Multitripla
    }
}
