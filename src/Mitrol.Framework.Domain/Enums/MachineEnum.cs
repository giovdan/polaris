namespace Mitrol.Framework.Domain.Enums
{
    // Tipologie di impianti
    public enum MachineEnum
    {
        FORA,                   // linee di taglio / robot standalone
        PIASTRINA,              // punzonatrice/foratrice piastre 803 - 1001 - 1003
        CALIBRO,                // taglio con calibro
        F401P,                  // foratrice piastre F401P
        P306PS,                 // punzonatrice travi 306PS
        P504PS,                 // punzonatrice/foratrice 504PS o TipoD8
        P504_PLA,               // 504PS con plasma
        V11,                    // Victory11 / Excalibur12
        E12,                    // Ecalibur 12
        A166,                   // angolari A166
        A166_TRI,               // angolari A166 con UF11-22
        A166_TRI_P,             // angolari A166 con UF11-22 truschini assi
        A166_TRI_D,             // angolari A166 con UF11-22 truschini discreti
        A166_TRI_M,             // angolari A166 con UF11-22 multitripla
        A166M,                  // angolari A166 con pinza
        A166R,                  // angolari A166 con rulli
        TIPOC,                  // punzonatrice/foratrice/taglio piastre TipoC
        TIPOA31,                // foratrice/taglio piastre TipoA con carrino
        GEMINI,                 // GEMINI con CNC Fanuc o con CNC Mitrol (Ficep France)
        TIPOB254,               // Piastre TipoB 254
        GANTRY_T,               // Gantry Travi
        TIPOALG,                // foratrice/taglio piastre TipoA25 Lateral Grip
        ENDEAVOUR,              // Endeavour
        ORIENT,                 // Orient
        ORIENT_1T,              // Orient singola testa (T2xxx rotante)
        ORIENT_2T,              // Orient doppia testa (T1xxx fissa - T2xxx rotante)
        HP_S,                   // Foratrice angolari HPS
        TIPOG,                  // TIPO_G con CNC Fanuc
        TIPOG_DX,               // TIPO_G con CNC Fanuc destra
        TIPOG_SX,               // TIPO_G con CNC Fanuc sinistra
        GANTRY_T_O,             // Gantry Travi Orient
        GEMINIFF,               // GEMINI con CNC Mitrol (Ficep France)
        FORATRICI,              // Famiglia foratrici
        FAM_ANGOLARI,           // Famiglia angolari
        FAM_PIASTRE,            // Famiglia piastre
        ROB_FANUC,              // Robot Fanuc antropomorfo
        CNC_FANUC               // Impianto con CNC Fanuc
    }
}
