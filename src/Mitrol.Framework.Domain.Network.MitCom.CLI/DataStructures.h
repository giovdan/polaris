#pragma once

// Max num. di nodi configurabili:
#define ETC_MAX_NODE_INFO       64

#define STR_LEN_NAME            22
#define STR_LEN_SWVER           10
#define MOD_ETC_DS301       0x0000012D      // Modulo DS301
#define N_TESTE                 4

typedef struct
{
    long sysTimer;             // Temporizzatore del sistema                                             0
    char scansioneCanBusFAST;  // Scansione CanBus sincrono: 2 o 4 in base a FAST_SYS=                   4
    char FlgEmulator;          // Flag modalità EMULATORE (1=asincrono 2=sincrono 3=interno960)          5
    char scansioneINOUT;       // Scansione del ciclo INOUT[ms]: 10 o 20 in base a FAST_SYS=             6
    char scansioneIOMANAG;     // Scansione Ethercat, Iomanag e PLC fast                                 7

   // 0 - Tipo impianto ([CONFIG] TANG=xx)                                                                     8
    char tipo_ang;

    // 1 - Tipo di macchina ([CONFIG] D=xx)                                                                     9
    char tipo_mac;

    // 2 - Ordinamento X ([CONFIG] O=xx)                                                                        10
    //      0 decrescente - spinge
    //      1 crescente - tira
    char ordine_x;

    // 3 - Tipo di unità di taglio ([CONFIG] R=xx)                                                              11
    char tipo_sega;

    // 4 - Tipo di motrice ([CONFIG] MOT=xx)                                                                    12
    char tipo_mot;

    // 5 - Tipo marcatrice                                                                                      13
    char tipo_mk;

    // 6 - Tipo marcatrice a scrittura ([CONFIG] MK_508=xx)                                                     14
    char tipo_mk_508;

    // 7 - Doppia Slitta Angolari  ([CONFIG] DSLI=x)                                                            15
    bool DoppiaSlitta;

    // 8 - Num. cut foratrici ([CONFIG] CUT_A=xx, CUT_B=xx, CUT_C=xx, CUT_D=xx)                                 16
    char cut_for[N_TESTE];

    // 12 - Num. cut punzonatrici piano C ([CONFIG] CUT_PUN_C=xx)                                               20
    char cut_pun_c;

    // 13 - ROBx  - Presenza Robot di taglio                                                                    21
    char pres_rob;

    // 14 - Lingua corrente selezionata ([CONFIG] LINGUA=xxxx)                                                  22
    unsigned short lingua_sel;

    // 16 - DTORx  - Configurazione della Doppia Torcia OXY o PLA per TipoA31 ([CONFIG] DTOR=x)                 24
    char DoppiaTorcia;

    // 17 - STZx  - Presenza stozzatrici angolari ([CONFIG] STZ=0 assente STZ=1 standard, STZ=2 Rotante)        25
    char pres_stz;

    // 18 - Tipo di cut di punzonatura ([CONFIG] CUT_PUN_LINEARE=x)                                             26
    char CutPunLineare;

    // 19 - Presenza gestione punzoni lunghi ([SUP_P] PRES_PZ_LUNG=n !!! default = 1)                           27
    char pres_PzLung;

    // 20 - Tipo di ingombro pinza CA riferiti agli slot del CUT_PUN_LINEARE=3 (0=verso dec., 1=verso cres.)    28
    char TipoIngPzCut_CA;
    // 21 - Tipo di ingombro pinza CB riferiti agli slot del CUT_PUN_LINEARE=3 (0=verso dec., 1=verso cres.)    29
    char TipoIngPzCut_CB;
    // 22 - Tipo di ingombro pinza CC riferiti agli slot del CUT_PUN_LINEARE=3 (0=verso dec., 1=verso cres.)    30
    char TipoIngPzCut_CC;

    // 23 - Configurazione CNC in modalità 'dorsale' ([CONFIG] CNC_DORSALE=1)                                   31
    bool CncDorsale;

    // 24 - Presenza Assi Ausiliari longitudinali                                                               32
    bool ConfAux;

    // 25 - Configurazione utensili Punzonatura                                                                 33
    bool ConfPunzo;

    // 26 - Configurazione utensili Foratura                                                                    34
    bool ConfFora;

    // 27 - Configurazione utensili Maschiatura                                                                 35
    bool ConfMaschia;

    // 28 - Configurazione utensili Plasma                                                                      36
    bool ConfPla;

    // 29 - Configurazione utensili Ossitaglio                                                                  37
    bool ConfOxy;

    // 30 - Configurazione utensili Taglio con cesoia                                                           38
    bool ConfCesoia;

    // 31 - Abilitazione Taglio e Marcatura contemporanei ([CONFIG] MK_MUL=1)                                   39
    bool Mk_mul;

    // 32 - Modalità di lavoro Gantry Travi ([CONFIG] GANTRY_T=x) (vedi define)                                 40
    char ModoGantry;

    // 33 - Presenza testa rotante ([CONFIG] ORIENT_A=x, ORIENT_B=x, ORIENT_C=x, ORIENT_D=x)                    41
    bool Orient[N_TESTE];

    // 37 - Configurazione Tool Mng di taglio segatrice ([SUP_S] TOOL_MNG=1)                                    45
    bool ConfToolS;

    // 38 - Configurazione rotazione testa C libera                                                             46
    bool RotazioneLibera;

    // 39 - Flag negato rielaborazione barra successiva ([PROCESS] NOT_PRC_NEXT_BAR=1)                          47
    bool NotNextPrcBarra;

    // 40 - Tipo di ciclo di foratura                                                                           48
    bool NewFora;

    // 41 - Lavorazione dei profili piatti ed U negli impianti angolari ([CONFIG] ANG_PRF_PU=x)                 49
    char AngPrfPU;

    // 42 - Posizione fissa utensile matita rotante nel magazzino testa verticale ([CONFIG]POS_MAG_TS77=x)      50
    char PosMagTS77;

    // 43 - Posizione fissa utensile scribing rotante nel magazzino testa verticale ([CONFIG]POS_MAG_TS76=x)    51
    char PosMagTS76;

    // 44 - Presenza CNC Fanuc (Gemini/TipoG o Robot antropomorfo)                                              52
    bool CncFanuc;               // Flag di configurazione modalità FANUC

    char PlasmaH;                // Tipo di apparecchiatura plasma                                       53

    char spare1[46];       //                                                                      54

    //    tot = 100
}
SYS_GENERAL_CONF;

typedef struct
{
	unsigned char        NodeId;                 // Indice del nodo per scambio quota                        0
	unsigned char        spare[7];               //                                                          1
												 //                                                    tot = 8
}ETHERCAT_AXE_ROBOT;


typedef struct
{
	unsigned char act_all_num[40];	// Numero allarmi attivi                0
	unsigned char act_all_par[40];	// Parametro allarmi attivi             40
	unsigned char all_num;          // Numero allarme corrente              80
	unsigned char all_par;          // Paramentro allarme corrente          81
	unsigned short msg_num;         // Numero messaggio                     82
	unsigned short msg_par;         // Parametro messaggio                  84
	unsigned short msg_num_wr;      // Numero messaggio da plc              86
	unsigned short msg_par_wr;      // Parametro messaggio da plc           88
	char spare[2];					//										90
									//                                tot = 92
}ALL_DB;

//  Definizione delle informazioni per ogni nodo presente
typedef struct
{
	// Parte statica di configurazione
	unsigned long	typeID;                 // Tipo di modulo                                           0

	unsigned short	lastErrorCode;          // Ultimo valore di errore/emergency                        4
	unsigned char	lastErrorReg;           // Ultimo registro di errore/emergency                      6
	unsigned char	lastEmergencyData[5];   // Ultimi dati di emergency                                 7
	long			RuntimeErrorCode;       // Errore di runtime (valido se network_ok=Falso)           4
	unsigned char	spare1[6];              //                                                         16

		// Parte dinamica
	bool			RqPres;                 // Modulo presente nel file ENI                            22
	unsigned char   Conf;                   // Eventuali errori di configurazione in Pegaso            23
	bool			cfg;                    // Master: stato della config. iniziale (init commands)    24
	bool			pres;                   // Master: indica modulo presente ed operativo             25
	unsigned char   fwVerMajor;             // Num. Revisione firmware M                               26
	unsigned char   fwVerMinor;             // Num. Revisione firmware m                               27

	char			devname[STR_LEN_NAME];  // Nome                                                    28
	char			swversion[STR_LEN_SWVER]; // Stringa Versione software                             50

	unsigned long   prod;                   // Codice prodotto                                         60
	unsigned long   vendorID;               // Vendor ID prodotto del nodo rilevato (1018h,1h)         64
	unsigned long   productCode;            // Codice prodotto del nodo rilevato (1018h,2h)            68
	unsigned char   state;                  // Stato                                                   72
	unsigned char   posOnLine;              // Posizione del modulo lungo la linea                     73
	unsigned short  warning;                // Codici di warning (custom)                              74
	unsigned long   errSdoCode;             // Codice d'errore SDO                                     76
	unsigned short  errSdoIndex;            // Indice SDO errato                                       80
	unsigned char   errSdoSubI;             // Sotto indice SDO errato                                 82
	unsigned char   spare2;                 // Allineamento a 4 byte                                   83
											//                                                  tot =  84
}ETHERCAT_NODE_INFO;

typedef struct
{
	char				Pres;                   // Flag di EtherCat configurato impostato da Minosse            0
	char				ConfMst;                // Stato di configurazione del Master ETCMR                     1
	short				errorCode;              // Errore di inizializzarione del master                        2
	long				RuntimeErrorCode;       // Errore di runtime del master (valido se network_ok=Falso)    4

		// Comando di Resync di tutta la linea
	bool				RqResync;               // Comando                                                      8
	bool				AcqResync;              // Esito                                                        9
	unsigned short      DelayResync;            // Attesa per nodi pronti dopo resync                          10
	unsigned char       BootErrorCode;          // Codice d'errore all'avvio di ETCMR                          12

	unsigned char       spare1;                 //                                                             13

	unsigned short      ErrorFrames;             // Error frames rilevati dal driver ethercat                  14

	ETHERCAT_AXE_ROBOT	AxeRobot;      // Dati per scambio quote X con robot Fanuc                             16

	ETHERCAT_NODE_INFO  Node[ETC_MAX_NODE_INFO];  // Info. dei nodi ordinati per address (i=Addr-1)            24

			// Dati da visualizzare in monitor (F5 dalla videata)
	bool				spare2;                  //                                                          5400
	unsigned char       AddrInfo;                // Indirizzo del nodo in monitor                            5401

	unsigned char       spare3[2];               //                                                          5402

	char				strError[64];          // (Mitrol) Descrizione dell'errore di configurazione         5404
	char				strDebugNodeOut[1000]; // Visualizzazione dati in uscita scambiati                   5468
	char				strDebugNodeInp[1000]; // Visualizzazione dati in ingresso scambiati                 6468
	short				skipLinesOut;            // Linee da saltare nella visualizzazione dei dati di out   7468
	short				skipLinesInp;            // Linee da saltare nella visualizzazione dei dati di inp   7470
	unsigned short      FaultErrCode[6];         // Codice d'errore per i 6 assi Siemens di ogni CU          7472
	unsigned short      FaultInfo[6];            // informazioni aggiuntive per fault assi Siemens           7484
	char				PathEniFile[32];         // Percorso dei file di configurazione ethercat             7496
										         //                                                   tot =  7528
} ETHERCAT_INFO;


// Struttura parametri utensile TS71 o TS72 (maschiatura con fresa)
// N.B. La struttura deve essere allineata a 4 perché è in DualPort (non è però pubblicata in una DB)
typedef struct
{
    float               ppmin;              // Passo min maschiatura con fresa [mm]                     160     MINPM
    float               ppmax;              // Passo max maschiatura con fresa [mm]                     164     MAXPM
    unsigned char       nDenti;             // Numero di denti fresa ad inserti per maschiatura (TS71)  168     NDEN
    unsigned char       spare1[3];          // Allineamento a 4 byte                                    169
    unsigned char       spare2[8];          // Allineamento union                                       172
                                            //                                                    tot = 180
} STRU_TS_71_72;

// Struttura parametri utensile TS73 (foratura+svasatura)
// N.B. La struttura deve essere allineata a 4 perché è in DualPort (non è però pubblicata in una DB)
typedef struct
{
    float               SvaANG;             // Angolo di affilatura svasatore TS73 [gra]                160     SVAANG
    float               SvaHT;              // Altezza punta svasatore TS73 [mm]                        164     SVAHT
    float               SvaS;               // Velocità rotazione svasatore TS73 [rpm]                  168     SVAS
    float               SvaVA;              // Velocità di alimentazione svasatore TS73 [mm/min]        172     SVAVA
    float               SvaDNF;             // Diametro della punta per forare svasatore TS73 [mm]      176     SVADNF
                                            //                                                    tot = 180
} STRU_TS_73;

// Struttura parametri utensile TS74 (foratura+sbavatura)
// N.B. La struttura deve essere allineata a 4 perché è in DualPort (non è però pubblicata in una DB)
typedef struct
{
    float               rae;                // Accostamento sbavatura esterna TS74 [mm]                 160     SBARAE
    float               efe;                // Lavoro sbavatura esterna TS74 [mm]                       164     SBAEFE
    float               efi;                // Lavoro sbavatura interna TS74 [mm]                       168     SBAEFI
    float               rai;                // Accostamento sbavatura interna TS74 [mm]                 172     SBARAI
    float               spare;              // Allineamento union                                       176     
                                            //                                                    tot = 180
} STRU_TS_74;

/* Definizione struttura float + abilitazione (ex FLOAT_MAX) */
typedef struct
{
    bool            en;         // Abilitazione del valore                          0
    unsigned char   spare1[3];  // Allineamento a 4 byte                            1
    float           val;        // Valore float                                     4
                            //                                            tot = 8
} FLOATB;

// Struttura parametri di foratura
// N.B. La struttura deve essere allineata a 4 perché è in DualPort (non è però pubblicata in una DB)
typedef struct
{
    float               s;                  // Velocità rotazione [rpm]                                   0     S
    unsigned char       Spare1;             // Allineamento a 4 byte                                      4
    unsigned char       sfo;                // Sforzo sulla punta [%]                                     5     SFO
    unsigned char       spare2;             // Allineamento a 4 byte                                      6
    bool                sca;                // Scarico truciolo 1=scarico 0=rottura                       7     SCA
    float               va;                 // Velocità di alimentazione [mm/min]                         8     VA
    float               sf;                 // Posizione di start foro [mm]                              12     SF
    float               ra;                 // Posizione di accost. rapido [mm]                          16     RA
    float               ia;                 // Posizione di interruz. 1 [mm]                             20     IA
    float               ib;                 // Passo rottura truciolo [mm]                               24     IB
    float               iff;                // Pos. velocità fine foro [mm]                              28     IF
    float               ef;                 // Posizione di end foro [mm]                                32     EF
    float               ang;                // Angolo di affilatura punta [gra]                          36     ANG
    float               vai;                // Velocità avanz. entrata foro [mm/min]                     40     VAI
    float               svn;                // Quota di passaggio in vel. nom. di lavoro [mm]            44     SVN
    float               f;                  // Velocità di contornitura [mm/min]                         48     F
                                            //                                                     tot = 52
} STRU_FORA;

typedef struct
{
    //@@ Commentati gli elementi non utilizzati da APLR (Verificare allineamento)
    unsigned short      ts;                 // Tipo di utensile                                           0     TS        <-- Chiave
    bool                rqSave;             // Flag richiesta salvataggio su disco                        2
//  boolean             Stato;              // Abilitazione utensile (0=enable 1=disable)                 3     STATO
    float               dn;                 // Diametro nominale [mm]                                     4     DN        <-- Chiave
//  float               in;                 // Ingombro utensile DN/2 [mm]                                8     IN
    FLOATB              lt;                 // Lunghezza tool [mm]                                       12     LT
    float               tk;                 // Preset tool sbavatore [mm]                                20     TK
    float               tool_life;          // Vita utensile [mt]                                        24     LIF
    float               aff_life;           // Soglia affilatura punta [mt]                              28     AFF
    float               max_life;           // Massima vita utensile [mt]                                32     MLI
    unsigned char       aut_sens;           // Prenotaz. autom. sensitivo [byte]  0 - 1 - 2              36     SAUT
    unsigned char       lubri;              // Tipo di lubrificazione [n]                                37     LUB
//  boolean             manual;             // Utensile Manuale                                          38     MAN
//  byte                ptl;                // Coeff. vita utensile [n]                                  39     PTL
    STRU_FORA           fora;               // Struttura parametri foratura                              40
//  char                cod[ L_COD + 1 ];   // Codice utensile [str]                                     92     COD:    <-- Chiave
//  boolean             Aspira;             // Abilitazione aspiratore trucioli [bool]                  105     ASPI
//  byte                CustomTipo;         // Parametro custom Tipo di utensile (usato nelle macro)    106     CSTT
//  byte                Palpa;              // Abilitazione palpatura utensile [byte] 0-1-2             107     PALP
    float               lin;                // Lunghezza porta utensile [mm]                            108     LIN
    float               din;                // Diametro porta utensile [mm]                             112     DIN
    float               tool_raf;           // Valore corr. raffreddamento punta [mt]                   116     RAF
    float               lim_raf;            // Limite raffreddamento punta [mt]                         120     MRA
    float               pp;                 // Fresa: profondità di passata [mm]                        124     PP
//  float               dnfmin;             // Diametro min foro (TS67 o TS35) [mm]                     128     MINDN
//  float               dnfmax;             // Diametro max foro (TS67 o TS35) [mm]                     132     MAXDN
//  float               dnp;                // Fresa: diametro placchette TS65-66 [mm]                  136     DNP
//  float               dng;                // Fresa: diametro gambo fresa TS66 [mm]                    140     DNG
//  float               hg;                 // Fresa: altezza gambo fresa TS66 [mm]                     144     HG
//  float               rsp;                // Fresa: raggio spigolo placchette quadre TS63-64 [mm]     148     RSP
//  boolean             utA;                // Utensile per testa A                                     152     UTA
//  boolean             utB;                // Utensile per testa B                                     153     UTB
//  boolean             utC;                // Utensile per testa C                                     154     UTC
//  boolean             utD;                // Utensile per testa D                                     155     UTD
    float               dt;                 // Diametro utensile reale [mm]                             156     DT

    union
    {
        STRU_TS_71_72   ts71_72;            // Parametri utensile TS71 o TS72 maschiatura con fresa     160
        STRU_TS_73      ts73;               // Parametri utensile TS73 foratura+svasatura               160
        STRU_TS_74      ts74;               // Parametri utensile TS74 foratura+sbavatura               160
    } par;

    //  boolean             RotI;               // Ciclo con Rotazione inversa [bool]                       180     ROTI
    //  boolean             MagMan;             // Utensile ad assegnazione manuale nel magazzino           181     MAG
    //  word16              numOp;              // Numero di operazioni (usato solo in calcolo tempi)       182
    //  float               tool_lifet;         // Vita utensile [sec] (usato solo in calcolo tempi)        184
    //  float               ae;                 // Fresa: profondità di passata radiale [mm]                188
    //  float               aramp;              // Fresa: max. angolo rampa [gra]                           192
    //  float               pvt;                // Fresa: pivot [mm]                                        196
                                                //                                                    tot = 196
} STRU_TOOL_F;


typedef struct
{
    unsigned char   testa;
    unsigned char   punta;
    STRU_TOOL_F     tool;
} STRU_ID_TOOL_F;

typedef struct
{
    unsigned char       cut;                // Segnalaz. cambio utensile                          
    bool                Stato;              // Abilitazione slot (0=enable 1=disable)             
    unsigned short      IndTool;            // N° utensile nel Tool Mng                           
    float               MaxLut;             // Max LUT ammessa                                    

} STRU_SETUP_F;

typedef struct
{
    unsigned char   testa;
    unsigned char   punta;
    unsigned char   spare[2];           // Allineamento a 4 byte
    STRU_SETUP_F    data;
} STRU_ID_SETUP_F;


