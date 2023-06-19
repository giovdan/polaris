namespace Mitrol.Framework.Domain.Cnc
{
    public class Constants
    {

        #region < Definizione generali per cnc Fanuc >

        /// <summary>
        /// Numero di canali CNC Fanuc (PATH1 e PATH2)
        /// </summary>
        public const short N_CAN_FANUC = 2;

        /// <summary>
        /// Dimensione del buffer di memorizzazione delle palpature
        /// </summary>
        public const short MAX_BUF_PALP = 1000;

        // Offset degli indici del tool Managment Fanuc interno (sono fissi in base agli slot del magazzino)
        public const short OFFSET_TOOLS_FORAC_FANUC = 0;
        public const short OFFSET_TOOLS_FORAD_FANUC = 100;
        public const short OFFSET_TOOLS_PLAC_FANUC = 200;
        public const short OFFSET_TOOLS_PLAD_FANUC = 210;
        public const short OFFSET_TOOLS_OXYC_FANUC = 220;
        public const short OFFSET_TOOLS_OXYD_FANUC = 230;

        /// <summary>
        /// Indice canale 1
        /// </summary>
        public const short IND_PATH1 = 0;

        /// <summary>
        /// Indice canale 2
        /// </summary>
        public const short IND_PATH2 = 1;

        /// <summary>
        /// Indice canale 2
        /// </summary>
        public const short IND_PATH3 = 2;

        /// <summary>
        /// Versione software Fanuc PLC Gemini minima di compatibilità
        /// </summary>
        public const double PLCVERSION_GEMINI_FANUC = 1;

        /// <summary>
        /// Lunghezza max. linea programma ISO per Fanuc
        /// </summary>
        public const short MAX_LEN_LINEA_ISO = 250;

        /// <summary>
        /// Pattern per la validazione della stringa che rappresenta un parametro del CNC Fanuc.
        /// </summary>
        public const string ParameterCodePattern = "^[#T](?:[0-9]+)(?:-1|-2|-1-2)(?:-(?:ms|sec|min))?$";

        /// <summary>
        /// Pattern per l'analisi della stringa che rappresenta un parametro variabile del CNC Fanuc.
        /// </summary>
        public const string VariableCodePattern = "^#(?<id>[0-9]+)(?:(?<path1>-1)|(?<path2>-2)|(?<path1>-1)(?<path2>-2)){1}$";

        /// <summary>
        /// Pattern per l'analisi della stringa che rappresenta un parametro timer del CNC Fanuc.
        /// </summary>
        public const string TimerCodePattern = "^T(?<id>[0-9]+)(?:(?<path1>-1)|(?<path2>-2)|(?<path1>-1)(?<path2>-2)){1}(?:-(?<type>ms|sec|min))$";

        public const string INVALID_CHANNEL_HANDLE = "INVALID_HANDLE";
        public const string OPERATION_NOT_POSSIBLE = "OPERATION_NOT_POSSIBLE";

        public const short NUM_CAR_DISCO_MK_TIPOG = 38;      // N° di caratteri della marcatrice a disco TipoG

        #endregion < Definizione generali per cnc Fanuc >

        #region < Definizione Macro Gxxx per Fanuc >

        public const int G_FANUC_0 = 0;       // Comando ISO Fanuc di movimentazione Rapido
        public const int G_FANUC_1 = 1;       // Comando ISO Fanuc di  Interpolazione Lineare
        public const int G_FANUC_2 = 2;       // Comando ISO Fanuc di  Interpolazione Circolare Oraria
        public const int G_FANUC_3 = 3;       // Comando ISO Fanuc di  Interpolazione Circolare Antioraria

        public const int G_FANUC_20 = 20;     // Unità di misura Pollici
        public const int G_FANUC_21 = 21;     // Unità di misura millimetri

        public const int G_FANUC_93 = 93;     // G93.2 - Comando ISO Fanuc di attivazione Rate Speed
        public const int G_FANUC_94 = 94;     // G94   - Comando ISO Fanuc di disattivazione Rate Speed

        public const int G_FANUC_201 = 201;   // Macro di Foratura e Maschiatura
        public const int G_FANUC_202 = 202;   // Macro di Bulinatura elicoidale (prog. + tempo)
        public const int G_FANUC_203 = 203;   // Macro di Foratura+Svasatura (TS73)
        public const int G_FANUC_208 = 208;   // Macro di Esecuzione Foro Fresato
        public const int G_FANUC_209 = 209;   // Macro di Esecuzione Maschiatura con Fresa (ad inserti o a pettine)

        public const int G_FANUC_213 = 213;   // Macro di Accensione Ossitaglio
        public const int G_FANUC_214 = 214;   // Macro di Spegnimento Ossitaglio
        public const int G_FANUC_215 = 215;   // Macro di Scarico: solo manuale per Gemini, manuale/automatico per TipoG
        public const int G_FANUC_216 = 216;   // Macro di Spegnimento Plasma e risalita parziale (tra un carattere e l'altro di mk plasma)
        public const int G_FANUC_217 = 217;   // Macro di Accensione Marcatura Plasma
        public const int G_FANUC_218 = 218;   // Macro di Accensione Ossitaglio Bevel Tripla
        public const int G_FANUC_219 = 219;   // Macro di Spegnimento Ossitaglio Bevel Tripla

        public const int G_FANUC_220 = 220;   // Macro di Palpatura Foratura / Fresatura con dispositivo laser

        public const int G_FANUC_221 = 221;   // Macro di Attacco Fresatura
        public const int G_FANUC_222 = 222;   // Macro di Sollevamento Parziale Fresatura
        public const int G_FANUC_223 = 223;   // Macro di Sollevamento Finale Fresatura
        public const int G_FANUC_224 = 224;   // Macro di Attacco Intermedio Fresatura

        public const int G_FANUC_227 = 227;   // Macro di Palpatura Sense per piastre con camber
        public const int G_FANUC_228 = 228;   // Macro di Palpatura Foratura / Fresatura con dispositivo meccanico
        public const int G_FANUC_229 = 229;   // Macro di Palpatura Foratura / Fresatura con utensile speciale di palpatura TS70

        public const int G_FANUC_230 = 230;   // Macro di Decremento Quantità

        public const int G_FANUC_231 = 231;   // Macro di Attacco fresatura TS75 (utensile in sagoma)
        public const int G_FANUC_232 = 232;   // Macro di attacco intermedio TS75 (utensile in sagoma)
        public const int G_FANUC_233 = 233;   // Macro di fine fresatura TS75 (utensile in sagoma): esce dal materiale
        public const int G_FANUC_234 = 234;   // Macro di Stop intermedio TS75 (utensile in sagoma): rimane nel materiale

        public const int G_FANUC_240 = 240;   // Macro di Home delle torce (usata solo internamente da Fanuc)

        public const int G_FANUC_241 = 241;   // Macro di Palpatura Plasma
        public const int G_FANUC_242 = 242;   // Macro di Accensione Plasma
        public const int G_FANUC_243 = 243;   // Macro di Spegnimento Plasma

        public const int G_FANUC_250 = 250;   // Macro di Spazzolatura (GEMINI: abilitata da .ini [FANUC] G250_1=1 o G250_2=1 o G250_3=1 - TipoG presente di default)
        public const int G_FANUC_251 = 251;   // Macro di posizionamento X in area pressore
        public const int G_FANUC_252 = 252;   // Macro di esclusione delle teste di foratura/taglio montate sull'asse Y o V
        public const int G_FANUC_253 = 253;   // Macro di sollevamento testa
        public const int G_FANUC_254 = 254;   // Macro di esclusione delle teste V di taglio montate sull'asse V in presenza anche dell'asse Y per le testa di foratura

        public const int G_FANUC_256 = 256;   // Macro di anticipo cambio utensile (abilitata da .ini [FANUC] G256=1)
        public const int G_FANUC_257 = 257;   // Macro di assegnazione del movimento X (per il plasma) alla testa 1, alla testa 2 o entrambe (copia)

        public const int G_FANUC_270 = 270;   // Macro di inizio sequenza DNC di foratura ottimizzata
        public const int G_FANUC_271 = 271;   // Macro di   fine sequenza DNC di foratura ottimizzata

        public const int G_FANUC_272 = 272;   // Macro di marcatura a scrittura (un carattere per ogni linea)
        public const int G_FANUC_273 = 273;   // Macro di   fine sequenza di marcatura a scrittura (dopo l'ultimo carattere)

        public const int G_FANUC_280 = 280;   // Macro di inizio stazione 'n'
        public const int G_FANUC_281 = 281;   // Macro di fine stazione 'n'

        public const int G_FANUC_292 = 292;   // Macro di inizio sequenza DNC di taglio al plasma ottimizzata
        public const int G_FANUC_293 = 293;   // Macro di   fine sequenza DNC di taglio al plasma ottimizzata

        public const int G_FANUC_294 = 294;   // Macro di inizio sequenza DNC di taglio con ossitaglio ottimizzata
        public const int G_FANUC_295 = 295;   // Macro di   fine sequenza DNC di taglio con ossitaglio ottimizzata

        public const int G_FANUC_301 = 301;   // Macro di riposizionamento pinze (TipoG)

        public const int G_FANUC_350 = 350;   // Macro marcatura a getto d'inchiostro
        public const int G_FANUC_352 = 352;   // Macro di inizio tracciatura a getto d'inchiostro
        public const int G_FANUC_353 = 353;   // Macro di   fine tracciatura a getto d'inchiostro

        #endregion < Definizione Macro Gxxx per Fanuc >

        #region < Definizione Macro Mxxx per Fanuc >

        public const int M_FANUC_30 = 30;     // M30 fine programma
        public const int M_FANUC_31 = 31;     // M31 di predisposizione
        public const int M_FANUC_32 = 32;     // M32 di carico lastra (TipoG)
        public const int M_FANUC_36 = 36;     // M36 di cambio utensile

        public const int M_FANUC_200 = 200;   // Disabilitazione lookahead

        public const int M_FANUC_231 = 231;   // Macro di accensione Plasma in sequenza ottimizzata
        public const int M_FANUC_232 = 232;   // Macro di spegnimento Plasma in sequenza ottimizzata

        public const int M_FANUC_235 = 235;   // Gestione Stand-off ON
        public const int M_FANUC_236 = 236;   // Gestione Stand-off OFF

        public const int M_FANUC_238 = 238;   // Spegnimento torcia

        public const int M_FANUC_240 = 240;   // Riduzione corrente ON
        public const int M_FANUC_241 = 241;   // Riduzione corrente OFF

        public const int M_FANUC_242 = 242;   // Macro di accensione Ossitaglio in sequenza ottimizzata
        public const int M_FANUC_243 = 243;   // Macro di spegnimento Ossitaglio in sequenza ottimizzata

        public const int M_FANUC_270 = 270;   // Disabilitazione palpatore OXY bevel
        public const int M_FANUC_271 = 271;   // Abilitazione palpatore OXY bevel

        public const int M_FANUC_278 = 278;   // Attivazione creep per Ossitaglio (vengono attivate le pressioni HIGH)
        public const int M_FANUC_279 = 279;   // Disattivazione creep per Ossitaglio (vengono riattivate le pressioni di lavoro)

        public const int M_FANUC_295 = 295;   // Riduzione velocità ON
        public const int M_FANUC_296 = 296;   // Riduzione velocità OFF

        public const int M_FANUC_999 = 999;   // Attivazione taglio in doppio (copia)
        public const int P_FANUC_12 = 12;     // Parametro di attivazione taglio in doppio (copia)

        #endregion < Definizione Macro Mxxx per Fanuc >

        #region < Definizione Variabili xxx per Fanuc >

        public const int V_FANUC_507 = 507;      // Comando esecuzione linea ISO da Polaris (G219 con programma CAM)
        public const int V_FANUC_508 = 508;      // Esito esecuzione linea ISO da Polaris (G219 con programma CAM)

        public const ushort V_FANUC_509 = 509;      // Comando M36
        public const int V_FANUC_510 = 510;      // Esito Comando M36
        public const int V_FANUC_515 = 515;      // Codice ultimo errore apparecchiatura plasma

        public const int V_FANUC_521 = 521;      // Numero di linea di programma in esecuzione

        public const int V_FANUC_527 = 527;      // Esecuzione linea DNC
        public const int V_FANUC_527_START = 0;  // Invio linea DNC
        public const int V_FANUC_527_CYCLE = 1;  // Inizio ciclo linea DNC
        public const int V_FANUC_527_END = 2;    // Fine OK ciclo linea DNC

        public const int V_FANUC_530 = 530;      // N° del programma o della stazione in esecuzione

        public const int V_FANUC_531 = 531;      // Lunghezza lastra o tubo tondo
        public const int V_FANUC_532 = 532;      // Larghezza lastra o tubo tondo
        public const int V_FANUC_533 = 533;      // Spessore lastra o tubo tondo

        public const int V_FANUC_534 = 534;      // Delta torcia 2 ossitaglio
        public const int V_FANUC_535 = 535;      // Distanza reale tra la torcia main e la slave 1 di Ossitaglio
        public const int V_FANUC_536 = 536;      // Delta torcia 3 ossitaglio
        public const int V_FANUC_537 = 537;      // Distanza reale tra la torcia main e la slave 2 di Ossitaglio

        public const int V_FANUC_538 = 538;      // Stato della scrittura dati verso Hypertherm (se configurata la prestazione di anticipo)
        public const int V_FANUC_538_START = 1;  // Inizio invio dati
        public const int V_FANUC_538_END_OK = 2; // Invio OK
        public const int V_FANUC_538_END_NOK = 3;// Invio NOT OK

        public const int V_FANUC_544 = 544;      // Modalità di eliminazione trucioli

        public const int V_FANUC_553 = 553;      // Origine NX stazione 1
        public const int V_FANUC_554 = 554;      // Origine NY stazione 1
        public const int V_FANUC_555 = 555;      // Origine NA stazione 1

        public const int V_FANUC_556 = 556;      // Origine NX stazione 2
        public const int V_FANUC_557 = 557;      // Origine NY stazione 2
        public const int V_FANUC_558 = 558;      // Origine NA stazione 2

        public const int V_FANUC_559 = 559;      // Origine NX stazione 3
        public const int V_FANUC_560 = 560;      // Origine NY stazione 3
        public const int V_FANUC_561 = 561;      // Origine NA stazione 3

        public const int V_FANUC_562 = 562;      // Origine NX stazione 4
        public const int V_FANUC_563 = 563;      // Origine NY stazione 4
        public const int V_FANUC_564 = 564;      // Origine NA stazione 4

        public const int V_FANUC_565 = 565;      // Origine NX stazione 5
        public const int V_FANUC_566 = 566;      // Origine NY stazione 5
        public const int V_FANUC_567 = 567;      // Origine NA stazione 5

        public const int V_FANUC_568 = 568;      // Origine NX stazione 6
        public const int V_FANUC_569 = 569;      // Origine NY stazione 6
        public const int V_FANUC_570 = 570;      // Origine NA stazione 6

        public const int V_FANUC_571 = 571;      // Origine NX stazione 7
        public const int V_FANUC_572 = 572;      // Origine NY stazione 7
        public const int V_FANUC_573 = 573;      // Origine NA stazione 7

        public const int V_FANUC_574 = 574;      // Origine NX stazione 8
        public const int V_FANUC_575 = 575;      // Origine NY stazione 8
        public const int V_FANUC_576 = 576;      // Origine NA stazione 8

        public const int V_FANUC_586 = 586;      // Delta torcia 2 plasma
        public const int V_FANUC_587 = 587;      // Distanza reale tra la torcia main e la slave di Plasma

        public const int V_FANUC_588 = 588;      // Comando verso il PLC Fanuc di cambio linea programma ISO (#588=1) o rielaborazione (#588=2)

        public const int V_FANUC_589 = 589;      // Delta torcia 4 ossitaglio
        public const int V_FANUC_590 = 590;      // Distanza reale tra la torcia main e la slave 4 di Ossitaglio

        public const int V_FANUC_667 = 667;      // Variabile ad uso comandi cicli plc (carico/scarico magazzino o mandrino)

        public const int V_FANUC_760 = 760;      // Valore di palpatura Z nel ciclo (G229) di Palpatura Foratura / Fresatura con utensile di palpatura TS70

        public const int V_FANUC_768 = 768;      // Delta torcia 5 ossitaglio
        public const int V_FANUC_769 = 769;      // Distanza reale tra la torcia main e la slave 5 di Ossitaglio

        public const int V_FANUC_770 = 770;      // Coordinata X del punto di interruzione taglio plasma
        public const int V_FANUC_771 = 771;      // Coordinata Y del punto di interruzione taglio plasma

        public const int V_FANUC_782 = 782;      // Funzione G modale asincrona
        public const int V_FANUC_783 = 783;      // Parametro A della funzione G modale asincrona
        public const int V_FANUC_784 = 784;      // Parametro B della funzione G modale asincrona
        public const int V_FANUC_785 = 785;      // Parametro C della funzione G modale asincrona

        public const int V_FANUC_786 = 786;      // Tipo di profilo (lastra o tubo tondo)

        public const int V_FANUC_790 = 790;      // Posizione pinza CA (TipoG)
        public const int V_FANUC_791 = 791;      // Posizione pinza CB (TipoG)
        public const int V_FANUC_792 = 792;      // Posizione pinza CC ausiliaria (TipoG)
        public const int V_FANUC_793 = 793;      // LCR corrente (TipoG)

        public const int V_FANUC_795 = 795;      // LBR corrente (TipoG)

        public const int V_FANUC_799 = 799;      // Numero della stazione in esecuzione

        public const int V_FANUC_98026 = 98026;  // Origine NX stazione 1 (Multistazione SLAVE)
        public const int V_FANUC_98027 = 98027;  // Origine NY stazione 1 (Multistazione SLAVE)
        public const int V_FANUC_98028 = 98028;  // Origine NA stazione 1 (Multistazione SLAVE)

        public const int V_FANUC_98029 = 98029;  // Origine NX stazione 2 (Multistazione SLAVE)
        public const int V_FANUC_98030 = 98030;  // Origine NY stazione 2 (Multistazione SLAVE)
        public const int V_FANUC_98031 = 98031;  // Origine NA stazione 2 (Multistazione SLAVE)

        public const int V_FANUC_98032 = 98032;  // Origine NX stazione 3 (Multistazione SLAVE)
        public const int V_FANUC_98033 = 98033;  // Origine NY stazione 3 (Multistazione SLAVE)
        public const int V_FANUC_98034 = 98034;  // Origine NA stazione 3 (Multistazione SLAVE)

        public const int V_FANUC_98035 = 98035;  // Origine NX stazione 4 (Multistazione SLAVE)
        public const int V_FANUC_98036 = 98036;  // Origine NY stazione 4 (Multistazione SLAVE)
        public const int V_FANUC_98037 = 98037;  // Origine NA stazione 4 (Multistazione SLAVE)

        public const int V_FANUC_98038 = 98038;  // Origine NX stazione 5 (Multistazione SLAVE)
        public const int V_FANUC_98039 = 98039;  // Origine NY stazione 5 (Multistazione SLAVE)
        public const int V_FANUC_98040 = 98040;  // Origine NA stazione 5 (Multistazione SLAVE)

        public const int V_FANUC_98041 = 98041;  // Origine NX stazione 6 (Multistazione SLAVE)
        public const int V_FANUC_98042 = 98042;  // Origine NY stazione 6 (Multistazione SLAVE)
        public const int V_FANUC_98043 = 98043;  // Origine NA stazione 6 (Multistazione SLAVE)

        public const int V_FANUC_98044 = 98044;  // Origine NX stazione 7 (Multistazione SLAVE)
        public const int V_FANUC_98045 = 98045;  // Origine NY stazione 7 (Multistazione SLAVE)
        public const int V_FANUC_98046 = 98046;  // Origine NA stazione 7 (Multistazione SLAVE)

        public const int V_FANUC_98047 = 98047;  // Origine NX stazione 8 (Multistazione SLAVE)
        public const int V_FANUC_98048 = 98048;  // Origine NY stazione 8 (Multistazione SLAVE)
        public const int V_FANUC_98049 = 98049;  // Origine NA stazione 8 (Multistazione SLAVE)

        #endregion < Definizione Variabili xxx per Fanuc >

        #region < Definizione blocchi ISO per Fanuc >

        public const string BLK_FANUC_N = "N";      // Numero di linea
        public const string BLK_FANUC_MK = "MK";    // Marker
        public const string BLK_FANUC_P = "P";      // Blocco funzione M999 P12
        public const string BLK_FANUC_PN = "PN";    // Indice di palpatura
        public const string BLK_FANUC_M = "M";      // Funzione M
        public const string BLK_FANUC_G = "G";      // Macro G

        public const string BLK_FANUC_X = "X";      // Asse X
        public const string BLK_FANUC_U = "U";      // Asse U
        public const string BLK_FANUC_Y = "Y";      // Asse Y
        public const string BLK_FANUC_YD = "YD";    // Asse YD
        public const string BLK_FANUC_YS = "YS";    // Asse YS
        public const string BLK_FANUC_V = "V";      // Asse V
        public const string BLK_FANUC_VD = "VD";    // Asse VD
        public const string BLK_FANUC_VS = "VS";    // Asse VS
        public const string BLK_FANUC_A = "A";      // Asse A: macro di attacco taglio/fresatura : posizione asse A testa Bevel plasma / XMIN oppure LCUT nelle linee di fine fresatura
        public const string BLK_FANUC_B = "B";      // Asse B: macro di attacco taglio/fresatura: posizione asse B testa Bevel plasma / XMAX

        public const string BLK_FANUC_AY = "AY";    // Angolo di rotazione tubo (TipoG)

        public const string BLK_FANUC_C = "C";      // Parametro C: macro di attacco taglio: compensazione
        public const string BLK_FANUC_G301_C = "C"; // Parametro C riposizionamento: posizione pinza CC (se programmata)
        public const string BLK_FANUC_G301_D = "D"; //Parametro D riposizionamento: posizione LCR di riposizionamento

        public const string BLK_FANUC_D = "D";      // Parametro D: macro di attacco ossitaglio: XMIN
        public const string BLK_FANUC_E = "E";      // Parametro E: macro di attacco ossitaglio Bevel: sequenza di preriscaldo materiale
        public const string BLK_FANUC_F = "F";      // Parametro F: macro di attacco taglio: override F
        public const string BLK_FANUC_H = "H";      // Parametro H
        public const string BLK_FANUC_I = "I";      // Parametro I: macro di attacco taglio/fresatura: YMIN
        public const string BLK_FANUC_J = "J";      // Parametro J: macro di attacco taglio/fresatura: YMAX
        public const string BLK_FANUC_K = "K";      // Parametro K: macro di attacco ossitaglio: XMAX
        public const string BLK_FANUC_Q = "Q";      // Parametro Q: macro di attacco taglio plasma: XMIN
        public const string BLK_FANUC_R = "R";      // Parametro R: macro di attacco taglio plasma: XMAX
        public const string BLK_FANUC_S = "S";      // Parametro S
        public const string BLK_FANUC_T = "T";      // Parametro T
        public const string BLK_FANUC_W = "W";      // Parametro W
        public const string BLK_FANUC_Z = "Z";      // Parametro Z

        public const string BLK_FANUC_PF_Z = "Z";   // Parametro macro di foratura: override EF
        public const string BLK_FANUC_PF_J = "J";   // Parametro macro di foratura: override IA

        public const string BLK_FANUC_PFF_D = "D"; // Parametro macro di foro fresato: diametro foro esterno
        public const string BLK_FANUC_PFF_J = "J"; // Parametro macro di foro fresato: diametro pre-foro

        public const string BLK_FANUC_PM_D = "D";   // Parametro macro di maschiatura con fresa: diametro foro esterno
        public const string BLK_FANUC_PM_J = "J";   // Parametro macro di maschiatura con fresa: diametro pre-foro
        public const string BLK_FANUC_PM_K = "K";   // Parametro macro di maschiatura con fresa: passo di maschiatura

        #endregion < Definizione blocchi ISO per Fanuc >

        #region < Definizione generali per cnc Mitrol >

        // Indicatori di uso flags cut del setup
        public const byte UT_NO_USO = (byte)' ';    // non richiesto
        public const byte UT_OK = (byte)'*';        // richiesto al posto giusto
        public const byte UT_NOK = (byte)'#';       // richiesto da cambiare
        public const byte UT_WAR = (byte)'!';       // WARNING
        public const byte UT_WAR_A = (byte)'x';     // WARNING arancione (LUT da misurare)

        #endregion < Definizione generali per cnc Mitrol >

        #region < Definizione blocchi ISO per cnc Mitrol >

        public const string BLK_MITROL_NP = "NP";       // Numero del pezzo
        public const string BLK_MITROL_MK = "MK";       // Marker di esecuzione

        public const string BLK_MITROL_B = "B:";        // Nome del programma
        public const string BLK_MITROL_MSG = "MSG";     // Numero del messaggio
        public const string BLK_MITROL_PAR = "PAR";     // Parametro del messaggio

        public const string BLK_MITROL_X = "X";         // Asse X
        public const string BLK_MITROL_Y = "Y";         // Asse Y
        public const string BLK_MITROL_Z = "Z";         // Asse Z
        public const string BLK_MITROL_V = "V";         // Asse V
        public const string BLK_MITROL_W = "W";         // Asse W
        public const string BLK_MITROL_R = "R";         // Asse R
        public const string BLK_MITROL_E = "E";         // Asse E (truschino sega dal basso)

        public const string BLK_MITROL_G = "G";         // Funzione G
        public const string BLK_MITROL_M = "M";         // Funzione M
        public const string BLK_MITROL_T = "T";         // Funzione T

        public const string BLK_MITROL_ANGA = "ANGA";   // Angolo di taglio anima
        public const string BLK_MITROL_ANGB = "ANGB";   // Angolo di taglio ala

        public const string BLK_MITROL_INGF = "INGF";   // Ingombro lato filo fisso
        public const string BLK_MITROL_INGM = "INGM";   // Ingombro lato filo mobile
        public const string BLK_MITROL_INGN = "INGN";   // Ingombro del pezzo successivo (massimo tra INGF ed INGM successivo)
        public const string BLK_MITROL_BMF = "BMF";     // Baricentro dal lato filo fisso del pezzo in scarico
        public const string BLK_MITROL_BMM = "BMM";     // Baricentro dal lato filo mobile del pezzo in scarico
        public const string BLK_MITROL_DPX = "DPX";     // Delta movimento pinzino
        public const string BLK_MITROL_DFF = "DFF";     // Distanza di attacco del taglio dal filo fisso

        public const string BLK_MITROL_SFR = "SFR";     // Indica se è un taglio di uno sfrido

        public const string BLK_MITROL_LOT = "LOT:";    // Pezzo nel nesting: codice del lotto
        public const string BLK_MITROL_NLOT = "NLOT";   // Pezzo nel nesting: n° di pezzo nel lotto
        public const string BLK_MITROL_TLOT = "TLOT";   // Pezzo nel nesting: n° tot. di pezzi nel lotto
        public const string BLK_MITROL_LAV = "LAV";     // Pezzo nel nesting: codice della lavorazione extra 
        public const string BLK_MITROL_CNW = "CNW";     // Codice della successiva destinazione
        public const string BLK_MITROL_BP = "BP:";      // Pezzo nel nesting: nome della barra dopo il taglio di separazione
        public const string BLK_MITROL_CSBZ = "CSBZ";   // Codice di sabbiatura

        public const string BLK_MITROL_ASSEMBLY = "ASB:"; // Identificativo pezzo: Assembly
        public const string BLK_MITROL_CONTRACT = "CNT:"; // Identificativo pezzo: Contract
        public const string BLK_MITROL_DRAWING = "DRW:";  // Identificativo pezzo: Drawing
        public const string BLK_MITROL_PART = "PRT:";     // Identificativo pezzo: Part
        public const string BLK_MITROL_PROJECT = "PRJ:";  // Identificativo pezzo: Project

        public const string BLK_MITROL_CST = "CST";     // Blocco di definizione della geometria del taglio su una scantonatura di fresatura
        public const string BLK_MITROL_PCST = "PCST";   // Blocco di definizione della geometria del taglio su una scantonatura di fresatura

        #endregion < Definizione blocchi ISO per cnc Mitrol >

        #region < Definizione Macro Mxxx per Mitrol >

        public const short M_00 = 0;        // Stop incondizionato
        public const short M_01 = 1;        // Stop programmato
        public const short M_12 = 12;       // Lavorazione piano esterno ala A (utensile matita rotante TS77)
        public const short M_13 = 13;       // Lavorazione piano esterno ala B (utensile matita rotante TS77)
        public const short M_14 = 14;       // Disattivazione congelamento assi ausiliari
        public const short M_15 = 15;       // Sbavatura esterna
        public const short M_16 = 16;       // Sbavatura interna
        public const short M_17 = 17;       // Lavorazione piano interno ala A (utensile scribing rotante TS76 e matita rotante TS77)
        public const short M_18 = 18;       // Lavorazione piano interno ala B (utensile scribing rotante TS76 e matita rotante TS77)
        public const short M_19 = 19;       // Lavorazione di foratura con congelamento asse aux al MIN
        public const short M_20 = 20;       // Lavorazione di foratura con congelamento asse aux al MAX
        public const short M_25 = 25;       // Foro doppio 1
        public const short M_26 = 26;       // Foro doppio 2
        public const short M_27 = 27;       // Lavorazione piano E (testa inizio trave GANTRY_T Orient) o piano A Endeavour Orient
        public const short M_28 = 28;       // Lavorazione piano F (coda fine trave GANTRY_T Orient)    o piano B Endeavour Orient
        public const short M_30 = 30;       // Fine programma
        public const short M_31 = 31;       // Predisposizione
        public const short M_32 = 32;       // Carico
        public const short M_33 = 33;       // Rilevamento lunghezza barra
        public const short M_34 = 34;       // Stop per girare il pezzo
        public const short M_36 = 36;       // Cambio Utensile
        public const short M_37 = 37;       // Rilevamento dimensioni profilo
        public const short M_38 = 38;       // Ricerca pezzo
        public const short M_40 = 40;       // Wait 40
        public const short M_41 = 41;       // Wait 41
        public const short M_42 = 42;       // Wait 42

        public const short M_51 = 51;       // Esecuzione scribing con matita fissa sul piano DC nella mezzeria verso DA
        public const short M_52 = 52;       // Esecuzione scribing con matita fissa sul piano DC nella mezzeria verso DB

        public const int M_90 = 90;       // Funzione M che spezza il multitasking (dopo la linea con M90) ed implicitamente NON ottimizza (es. corridoio)
        public const int M_91 = 91;       // Funzione M ciclo con contrasti (TANG_GANTRY_T)
        public const int M_92 = 92;       // Funzione M che NON spezza il multitasking, NON altera la sequenza delle operazioni nel Canonico e NON fa ottimizzazioni (es. corridio)
        public const int M_98 = 98;       // Decremento pezzo o barra
        public const int M_99 = 99;       // Decremento pezzo di nesting (pezzo a misura)
        public const int M_102 = 102;     // Comando zona zero giù per passate angolari

        #endregion < Definizione Macro Mxxx per Mitrol >


        #region < Definizione blocchi ISO per tutti i cnc >

        public const string BLKISO_SUP_SUP00 = "SUP00"; // Linea di inizio programma
        
        public const string BLKISO_SUP_SUP01 = "SUP01"; // Setup generale
        public const string BLKISO_SUP_G = "G";         // Setup generale: tipo di programma
        public const string BLKISO_SUP_CP = "CP:";      // Setup generale: Codice profilo
        public const string BLKISO_SUP_Ps = "P:";       // Setup generale: Nome profilo
        public const string BLKISO_SUP_CM = "CM";       // Setup generale: Codice materiale
        public const string BLKISO_SUP_Ms = "M:";       // Setup generale: Nome materiale

        public const string BLKISO_SUP_LB = "LB";       // Setup generale: Lunghezza barra nominale
        public const string BLKISO_SUP_LBR = "LBR";     // Setup generale: Lunghezza barra rilevata
        public const string BLKISO_SUP_RI = "RI";       // Setup generale: Angolo anima trave iniziale
        public const string BLKISO_SUP_RF = "RF";       // Setup generale: Angolo anima trave finale

        public const string BLKISO_SUP_NPM = "NPM";     // Setup generale: Numero di pezzi tot. nella barra
        public const string BLKISO_SUP_PU = "PU";       // Setup generale: Profili U con ali sui rulli (=1)
        public const string BLKISO_SUP_PT = "PT";       // Setup generale: Indicazione di profilo trasversale (90°)
        public const string BLKISO_SUP_BND = "BND";     // Setup generale: Indicazione profili U di tipo Bended

        public const string BLKISO_SUP_SA = "SA";       // Setup generale: Dimensione profilo SA
        public const string BLKISO_SUP_TA = "TA";       // Setup generale: Dimensione profilo TA
        public const string BLKISO_SUP_SB = "SB";       // Setup generale: Dimensione profilo SB
        public const string BLKISO_SUP_TB = "TB";       // Setup generale: Dimensione profilo TB
        public const string BLKISO_SUP_R = "R";         // Setup generale: Dimensione profilo R
        public const string BLKISO_SUP_ANG = "ANG";     // Setup generale: Dimensione profilo ANG (profili V)
        public const string BLKISO_SUP_SBA = "SBA";     // Setup generale: Dimensione profilo SBA
        public const string BLKISO_SUP_TBA = "TBA";     // Setup generale: Dimensione profilo TBA
        public const string BLKISO_SUP_SBB = "SBB";     // Setup generale: Dimensione profilo SBB
        public const string BLKISO_SUP_TBB = "TBB";     // Setup generale: Dimensione profilo TBB
        public const string BLKISO_SUP_SAB = "SAB";     // Setup generale: Dimensione profilo SAB
        public const string BLKISO_SUP_DSA = "DSA";     // Setup generale: Dimensione profilo DSA
        public const string BLKISO_SUP_DSB = "DSB";     // Setup generale: Dimensione profilo DSB

        public const string BLKISO_SUP_SAR = "SAR";     // Setup generale: Dimensione profilo SA rilevata

        public const string BLKISO_SUP_MOT = "MOT";     // Setup generale: Tipo di motrice

        public const string BLKISO_SUP_WS = "WS";       // Setup generale: Peso specifico
        public const string BLKISO_SUP_WL = "WL";       // Setup generale: Peso lineare
        public const string BLKISO_SUP_SZ = "SZ";       // Setup generale: Sezione

        public const string BLKISO_SUP_CA = "CA";       // Setup generale: Posizione pinza A
        public const string BLKISO_SUP_CB = "CB";       // Setup generale: Posizione pinza B
        public const string BLKISO_SUP_CC = "CC";       // Setup generale: Posizione pinza C

        public const string BLKISO_SUP_XLD = "XLD";     // Setup generale: Coordinata di carico
        public const string BLKISO_SUP_LCR = "LCR";     // Setup generale: coordinata primo riposizionamento

        public const string BLKISO_SUP_CRTR = "CRTR";   // Setup generale: Criterio di eliminazione trucioli
        public const string BLKISO_SUP_MHOL = "MHOL";   // Setup generale: Limite n° di fori per spazzolatura
        public const string BLKISO_SUP_STZ = "STZ";     // Setup generale: Numero di stazione corrente

        public const string BLKISO_SUP_QT = "QT";       // Linea QT: n° ripetizioni del programma senza rielaborazione

        public const string BLKISO_SUP_SUFA = "SUFA";   // Setup utensili: foratura testa A
        public const string BLKISO_SUP_SUFB = "SUFB";   // Setup utensili: foratura testa A
        public const string BLKISO_SUP_SUFC = "SUFC";   // Setup utensili: foratura testa A
        public const string BLKISO_SUP_SUFD = "SUFD";   // Setup utensili: foratura testa A
        public const string BLKISO_SUP_UT = "UT";       // Setup utensili: Indice utensile
        public const string BLKISO_SUP_WT = "WT";       // Setup utensili: Tipo di lavorazione

        public const string BLKISO_SUP_SPLA = "SPLA";   // Setup utensili: taglio plasma
        public const string BLKISO_SUP_SOXY = "SOXY";   // Setup utensili: taglio con ossitaglio
        public const string BLKISO_SUP_IT = "IT";       // Setup utensili: indice della tabella di taglio plasma
        public const string BLKISO_SUP_MK = "MK";       // Setup utensili: indice della tabella di marcatura plasma
        public const string BLKISO_SUP_TH = "TH";       // Setup utensili: indice della tabella di truehole
        public const string BLKISO_SUP_BV = "BV";       // Setup utensili: indice della tabella di bevel
        public const string BLKISO_SUP_TK = "TK";       // Setup utensili: spessore di taglio
        public const string BLKISO_SUP_DTA = "DTA";     // Setup utensili: distanza della torcia A
        public const string BLKISO_SUP_DTB = "DTB";     // Setup utensili: distanza della torcia B
        public const string BLKISO_SUP_DTC = "DTC";     // Setup utensili: distanza della torcia C
        public const string BLKISO_SUP_DTD = "DTD";     // Setup utensili: distanza della torcia D
        public const string BLKISO_SUP_BVHA = "BVHA";   // Setup utensili: Valore prog. altezza cianfrino alto
        public const string BLKISO_SUP_BVAA = "BVAA";   // Setup utensili: Valore prog. angolo  cianfrino alto
        public const string BLKISO_SUP_BVHB = "BVHB";   // Setup utensili: Valore prog. altezza cianfrino basso
        public const string BLKISO_SUP_BVAB = "BVAB";   // Setup utensili: Valore prog. angolo  cianfrino basso

        public const string BLKISO_SUP_SUPS = "SUPS";   // Setup utensili: segatrice

        public const string BLKISO_SUP_SUMK = "SUMK";   // Setup utensili: marcatrice a scrittura

        public const string BLKISO_XMK = "XMK";         // X di fine marcatura ReaJet
        public const string BLKISO_AMK = "AMK";         // Angolo marcatura
        public const string BLKISO_NMK = "NMK:";        // Stringa di marcatura
        public const string BLKISO_HMK = "HMK";         // Altezza caratteri marcatura ReaJet
        public const string BLKISO_RMK = "RMK";         // Angolo di rotazione marcatura ReaJet
        public const string BLKISO_TMK = "TMK";         // Tipo di marcatura ReaJet
        public const string BLKISO_LMK = "LMK";         // Lato di marcatura ReaJet

        #endregion < Definizione blocchi ISO per tutti i cnc >

    }
}
