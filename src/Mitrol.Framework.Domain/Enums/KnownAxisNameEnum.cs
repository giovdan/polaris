namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Nomi degli assi noti
    /// N.B. L'assegnazione del valore non deve essere modificata perchè viene utilizzata in APLR per la configurazione degli assi
    /// </summary>
    public enum KnownAxisNameEnum
    {
        /// <summary>
        /// Asse valido, ma non tra quelli noti.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// CNC Fanuc PATH 1: asse X longitudinale master
        /// </summary>
        XD,
        /// <summary>
        /// CNC Fanuc PATH 1: asse X longitudinale slave
        /// </summary>
        XS,
        /// <summary>
        /// CNC Fanuc PATH 1: asse Y trasversale master
        /// </summary>
        YD,
        /// <summary>
        /// CNC Fanuc PATH 1: asse Z verticale foratrice
        /// </summary>
        Z,
        /// <summary>
        /// CNC Fanuc PATH 1: asse ausilario
        /// </summary>
        U,
        /// <summary>
        /// CNC Fanuc PATH 1: asse verticale torcia plasma
        /// </summary>
        W,
        /// <summary>
        /// CNC Fanuc PATH 1: asse Y trasversale slave
        /// </summary>
        YS,
        /// <summary>
        /// CNC Fanuc PATH 1: asse A torcia bevel
        /// </summary>
        A,
        /// <summary>
        /// CNC Fanuc PATH 1: asse B torcia bevel
        /// </summary>
        B,
        /// <summary>
        /// CNC Fanuc PATH 1: asse V trasversale
        /// </summary>
        V,
        /// <summary>
        /// CNC Fanuc PATH 1: riscontro foratrice 1 TipoG
        /// </summary>
        VR,
        /// <summary>
        /// CNC Fanuc PATH 1: asse pinza C TipoG
        /// </summary>
        XC,
        /// <summary>
        /// CNC Fanuc PATH 1: riscontro foratrice 1 Gemini
        /// </summary>
        YR,
        /// <summary>
        /// CNC Fanuc PATH 2: asse Y master
        /// </summary>
        Y2D,
        /// <summary>
        /// CNC Fanuc PATH 2: asse Z verticale foratrice
        /// </summary>
        Z2,
        /// <summary>
        /// CNC Fanuc PATH 2: asse ausilario
        /// </summary>
        U2,
        /// <summary>
        /// CNC Fanuc PATH 2: asse verticale torcia plasma
        /// </summary>
        W2,
        /// <summary>
        /// CNC Fanuc PATH 2: asse Y slave
        /// </summary>
        Y2S,
        /// <summary>
        /// CNC Fanuc PATH 2: asse A torcia bevel
        /// </summary>
        A2,
        /// <summary>
        /// CNC Fanuc PATH 2: asse B torcia bevel
        /// </summary>
        B2,
        /// <summary>
        /// CNC Fanuc PATH 2: asse V trasversale
        /// </summary>
        V2,
        /// <summary>
        /// CNC Fanuc PATH 2: riscontro foratrice 2 TipoG
        /// </summary>
        VR2,

        /// <summary>
        /// CNC Mitrol: motrice principale
        /// </summary>
        AXX = 1,
        /// <summary>
        /// CNC Mitrol: truschino piano Y
        /// </summary>
        AXY = 2,
        /// <summary>
        /// CNC Mitrol: truschino piano Z
        /// </summary>
        AXZ = 3,
        /// <summary>
        /// CNC Mitrol: truschino piano V
        /// </summary>
        AXV = 4,
        /// <summary>
        /// CNC Mitrol: truschino piano W
        /// </summary>
        AXW = 5,
        /// <summary>
        /// CNC Mitrol: sollevamento pinza
        /// </summary>
        AXP = 6,
        /// <summary>
        /// CNC Mitrol: rotazione segatrice
        /// </summary>
        AXR = 7,
        /// <summary>
        /// CNC Mitrol: primo palpatore (ala Y inferiore)
        /// </summary>
        AP1 = 8,
        /// <summary>
        /// CNC Mitrol: secondo palpatore (ala Z inferiore)
        /// </summary>
        AP2 = 9,
        /// <summary>
        /// CNC Mitrol: morsa mobile
        /// </summary>
        AMM = 10,
        /// <summary>
        /// CNC Mitrol: asse longitudinale ausiliario testa U
        /// </summary>
        AUX = 11,
        /// <summary>
        /// CNC Mitrol: movimento trasversale pinza
        /// </summary>
        AXT = 12,
        /// <summary>
        /// CNC Mitrol: morsa fissa
        /// </summary>
        AMF = 13,
        /// <summary>
        /// CNC Mitrol: avanzamento lama sega
        /// </summary>
        AXS = 14,
        /// <summary>
        /// CNC Mitrol: avanzamento piano Y
        /// </summary>
        AXA = 15,
        /// <summary>
        /// CNC Mitrol: avanzamento piano Z
        /// </summary>
        AXB = 16,
        /// <summary>
        /// CNC Mitrol: avanzamento piano V
        /// </summary>
        AXC = 17,
        /// <summary>
        /// CNC Mitrol: avanzamento piano W
        /// </summary>
        AXD = 18,
        /// <summary>
        /// CNC Mitrol: disco marcatrice
        /// </summary>
        ADM = 19,
        /// <summary>
        /// CNC Mitrol: truschino foratrici UF22 e navetta TipoB / TipoC
        /// </summary>
        AXU = 20,
        /// <summary>
        /// CNC Mitrol: asse A robot
        /// </summary>
        ARA = 21,
        /// <summary>
        /// CNC Mitrol: asse B robot
        /// </summary>
        ARB = 22,
        /// <summary>
        /// CNC Mitrol: asse C robot
        /// </summary>
        ARC = 23,
        /// <summary>
        /// CNC Mitrol: asse D robot
        /// </summary>
        ARD = 24,
        /// <summary>
        /// CNC Mitrol: asse E robot
        /// </summary>
        ARE = 25,
        /// <summary>
        /// CNC Mitrol: cambio utensile piano Y
        /// </summary>
        ATA = 26,
        /// <summary>
        /// CNC Mitrol: cambio utensile piano Z
        /// </summary>
        ATB = 27,
        /// <summary>
        /// CNC Mitrol: cambio utensile piano V
        /// </summary>
        ATC = 28,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APA = 29,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APB = 30,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APC = 31,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APD = 32,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APE = 33,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APF = 34,
        /// <summary>
        /// CNC Mitrol: Asse secondario
        /// </summary>
        BXX = 35,
        /// <summary>
        /// CNC Mitrol: Truschino Y secondario
        /// </summary>
        BXY = 36,
        /// <summary>
        /// CNC Mitrol: cambio utensile piano W
        /// </summary>
        ATD = 37,
        /// <summary>
        /// CNC Mitrol: pinza slave
        /// </summary>
        APS = 38,
        /// <summary>
        /// CNC Mitrol: Asse encoder assoluto per AXX
        /// </summary>
        AKA = 39,
        /// <summary>
        /// CNC Mitrol: Asse encoder assoluto per BXX
        /// </summary>
        AKB = 40,
        /// <summary>
        /// CNC Mitrol: libero
        /// </summary>
        AKC = 41,
        /// <summary>
        /// CNC Mitrol: libero
        /// </summary>
        AKD = 42,
        /// <summary>
        /// CNC Mitrol: mandrino asse Y
        /// </summary>
        MDY = 43,
        /// <summary>
        /// CNC Mitrol: mandrino asse Z
        /// </summary>
        MDZ = 44,
        /// <summary>
        /// CNC Mitrol: mandrino asse V
        /// </summary>
        MDV = 45,
        /// <summary>
        /// CNC Mitrol: mandrino asse W
        /// </summary>
        MDW = 46,
        /// <summary>
        /// CNC Mitrol: truschino torcia (impianto 504PS)
        /// </summary>
        ATY = 47,
        /// <summary>
        /// CNC Mitrol: stand-off torcia (impianto 504PS o TipoA31)
        /// </summary>
        ATZ = 48,
        /// <summary>
        /// CNC Mitrol: terzo palpatore (ala Y superiore)
        /// </summary>
        AP3 = 49,
        /// <summary>
        /// CNC Mitrol: quarto palpatore (ala Z superiore)
        /// </summary>
        AP4 = 50,
        /// <summary>
        /// CNC Mitrol: truschino seconda unità polifunzionale
        /// </summary>
        AXQ = 51,
        /// <summary>
        /// CNC Mitrol: truschino piano Y A
        /// </summary>
        AYA = 52,
        /// <summary>
        /// CNC Mitrol: truschino piano Y B
        /// </summary>
        AYB = 53,
        /// <summary>
        /// CNC Mitrol: truschino piano Y C
        /// </summary>
        AYC = 54,
        /// <summary>
        /// CNC Mitrol: truschino piano Z A
        /// </summary>
        AZA = 55,
        /// <summary>
        /// CNC Mitrol: truschino piano Z B
        /// </summary>
        AZB = 56,
        /// <summary>
        /// CNC Mitrol: truschino piano Z C
        /// </summary>
        AZC = 57,
        /// <summary>
        /// CNC Mitrol: asse F robot
        /// </summary>
        ARF = 58,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APG = 59,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APH = 60,
        /// <summary>
        /// CNC Mitrol: asse longitudinale ausiliario testa Y
        /// </summary>
        AYX = 61,
        /// <summary>
        /// CNC Mitrol: asse longitudinale ausiliario testa W
        /// </summary>
        AWX = 62,
        /// <summary>
        /// CNC Mitrol: truschino foratrice piano V A (A166T)
        /// </summary>
        AVA = 63,
        /// <summary>
        /// CNC Mitrol: truschino foratrice piano V B (A166T)
        /// </summary>
        AVB = 64,
        /// <summary>
        /// CNC Mitrol: truschino foratrice piano V C (A166T)
        /// </summary>
        AVC = 65,
        /// <summary>
        /// CNC Mitrol: truschino foratrice piano W A (A166T)
        /// </summary>
        AWA = 66,
        /// <summary>
        /// CNC Mitrol: truschino foratrice piano W B (A166T)
        /// </summary>
        AWB = 67,
        /// <summary>
        /// CNC Mitrol: truschino foratrice piano W C (A166T)
        /// </summary>
        AWC = 68,
        /// <summary>
        /// CNC Mitrol: truschino foratrice piano U A (A166T)
        /// </summary>
        AUA = 69,
        /// <summary>
        /// CNC Mitrol: truschino foratrice piano U B (A166T)
        /// </summary>
        AUB = 70,
        /// <summary>
        /// CNC Mitrol: truschino foratrice piano U C (A166T)
        /// </summary>
        AUC = 71,
        /// <summary>
        /// CNC Mitrol: avanzamento foratrice piano V A (A166T)
        /// </summary>
        ACA = 72,
        /// <summary>
        /// CNC Mitrol: avanzamento foratrice piano V B (A166T)
        /// </summary>
        ACB = 73,
        /// <summary>
        /// CNC Mitrol: avanzamento foratrice piano V C (A166T)
        /// </summary>
        ACC = 74,
        /// <summary>
        /// CNC Mitrol: avanzamento foratrice piano W A (A166T)
        /// </summary>
        ADA = 75,
        /// <summary>
        /// CNC Mitrol: avanzamento foratrice piano W B (A166T)
        /// </summary>
        ADB = 76,
        /// <summary>
        /// CNC Mitrol: avanzamento foratrice piano W C (A166T)
        /// </summary>
        ADC = 77,
        /// <summary>
        /// CNC Mitrol: avanzamento foratrice piano U A (A166T)
        /// </summary>
        ABA = 78,
        /// <summary>
        /// CNC Mitrol: avanzamento foratrice piano U B (A166T)
        /// </summary>
        ABB = 79,
        /// <summary>
        /// CNC Mitrol: avanzamento foratrice piano U C (A166T)
        /// </summary>
        ABC = 80,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        API = 81,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APJ = 82,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APK = 83,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APL = 84,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APM = 85,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APN = 86,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APO = 87,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APP = 88,
        /// <summary>
        /// CNC Mitrol: asse longitudinale ausiliario testa V
        /// </summary>
        AVX = 89,
        /// <summary>
        /// CNC Mitrol: asse G ausiliario robot
        /// </summary>
        ARG = 90,
        /// <summary>
        /// CNC Mitrol: asse longitudinale ausiliario testa Z
        /// </summary>
        AZX = 91,
        /// <summary>
        /// CNC Mitrol: asse primo contrasto Orizzontale (Gantry) o asse truschino segatrice dal basso (HPS)
        /// </summary>
        AXE = 92,
        /// <summary>
        /// CNC Mitrol: asse secondo contrasto Orizzontale (Gantry)
        /// </summary>
        AXF = 93,
        /// <summary>
        /// CNC Mitrol: asse primo contrasto Verticale (Gantry) o asse riscontro carro segatrice dal basso (HPS)
        /// </summary>
        AXG = 94,
        /// <summary>
        /// CNC Mitrol: asse secondo contrasto Verticale (Gantry)
        /// </summary>
        AXH = 95,
        /// <summary>
        /// CNC Mitrol: Asse Verticale di Misura (Gantry)
        /// </summary>
        AP5 = 96,
        /// <summary>
        /// CNC Mitrol: asse rotazione testa Y (Endeavour Orient)
        /// </summary>
        MRY = 97,
        /// <summary>
        /// CNC Mitrol: asse rotazione testa Z (Endeavour Orient)
        /// </summary>
        MRZ = 98,
        /// <summary>
        /// CNC Mitrol: asse rotazione testa V (Endeavour Orient)
        /// </summary>
        MRV = 99,
        /// <summary>
        /// CNC Mitrol: Truschino stozzatrice rotante destra alta NRH1
        /// </summary>
        SVA = 100,
        /// <summary>
        /// CNC Mitrol: Truschino stozzatrice rotante destra alta NRH2
        /// </summary>
        SVB = 101,
        /// <summary>
        /// CNC Mitrol: Truschino stozzatrice rotante sinistra alta NLH1
        /// </summary>
        SWA = 102,
        /// <summary>
        /// CNC Mitrol: Truschino stozzatrice rotante sinistra alta NLH2
        /// </summary>
        SWB = 103,
        /// <summary>
        /// CNC Mitrol: Truschino stozzatrice rotante sinistra bassa NLL1
        /// </summary>
        SWC = 104,
        /// <summary>
        /// CNC Mitrol: Truschino stozzatrice rotante sinistra bassa NLL2
        /// </summary>
        SWD = 105,
        /// <summary>
        /// CNC Mitrol: Rotazione stozzatrice rotante destra alta NRH1
        /// </summary>
        RVA = 106,
        /// <summary>
        /// CNC Mitrol: Rotazione stozzatrice rotante destra alta NRH2
        /// </summary>
        RVB = 107,
        /// <summary>
        /// CNC Mitrol: Rotazione stozzatrice rotante sinistra alta NLH1
        /// </summary>
        RWA = 108,
        /// <summary>
        /// CNC Mitrol: Rotazione stozzatrice rotante sinistra alta NLH2
        /// </summary>
        RWB = 109,
        /// <summary>
        /// CNC Mitrol: Rotazione stozzatrice rotante sinistra bassa NLL1
        /// </summary>
        RWC = 110,
        /// <summary>
        /// CNC Mitrol: Rotazione stozzatrice rotante sinistra bassa NLL2
        /// </summary>
        RWD = 111,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APQ = 112,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APR = 113,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APT = 114,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        APU = 115,
        /// <summary>
        /// CNC Mitrol: seconda morsa mobile
        /// </summary>
        AMD = 116,
        /// <summary>
        /// CNC Mitrol: Magnete
        /// </summary>
        CXX = 117,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQA = 118,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQB = 119,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQC = 120,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQD = 121,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQE = 122,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQF = 123,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQG = 124,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQH = 125,
        /// <summary>
        /// CNC Mitrol: Rullo trattore scarico robot
        /// </summary>
        DXX = 126,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQI = 127,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQJ = 128,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQK = 129,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQL = 130,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQM = 131,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQN = 132,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQO = 133,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQP = 134,
        /// <summary>
        /// CNC Mitrol: asse PLC
        /// </summary>
        AQQ = 135,

        /*
        /// <summary>
        /// CNC Mitrol: asse idraulico di punzonatura
        /// </summary>
        //APZ,
        /// <summary>
        /// CNC Mitrol: asse idraulico cambio-utensile punzonatura lineare
        /// </summary>
        //ATH,
        /// <summary>
        /// CNC Mitrol: asse idraulico A PLC
        /// </summary>
        //AAH,        
        /// <summary>
        /// CNC Mitrol: asse idraulico B PLC
        /// </summary>
        //ABH,
        /// <summary>
        /// CNC Mitrol: asse idraulico C PLC
        /// </summary>
        //ACH,
        /// <summary>
        /// CNC Mitrol: asse idraulico D PLC
        /// </summary>
        //ADH,
        /// <summary>
        /// CNC Mitrol: asse idraulico E PLC
        /// </summary>
        //AEH,
        /// <summary>
        /// CNC Mitrol: asse idraulico F PLC
        /// </summary>
        //AFH,
        /// <summary>
        /// CNC Mitrol: asse idraulico G PLC
        /// </summary>
        //AGH,
        /// <summary>
        /// CNC Mitrol: asse idraulico H PLC
        /// </summary>
        //AHH,
        /// <summary>
        /// CNC Mitrol: asse idraulico I PLC
        /// </summary>
        //AIH,
        /// <summary>
        /// CNC Mitrol: asse idraulico J PLC
        /// </summary>
        //AJH,
        */
        /// <summary>
        /// CNC Fanuc Robot: asse lineare Y
        /// </summary>
        Yrob = 201,
        /// <summary>
        /// CNC Fanuc Robot: asse lineare Z
        /// </summary>
        Zrob = 202,
        /// <summary>
        /// CNC Fanuc Robot: asse polso J
        /// </summary>
        J1 = 203,
        /// <summary>
        /// CNC Fanuc Robot: asse polso J
        /// </summary>
        J2 = 204,
        /// <summary>
        /// CNC Fanuc Robot: asse polso J
        /// </summary>
        J3 = 205,
        /// <summary>
        /// CNC Fanuc Robot: asse polso J
        /// </summary>
        J4 = 206,
        /// <summary>
        /// CNC Fanuc Robot: asse polso J
        /// </summary>
        J5 = 207,
        /// <summary>
        /// CNC Fanuc Robot: asse polso J
        /// </summary>
        J6 = 208,
    }
}
