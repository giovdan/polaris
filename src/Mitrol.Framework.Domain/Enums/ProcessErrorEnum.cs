namespace Mitrol.Framework.Domain.Enums
{
    #region < Definizione errori di elaborazione >

    public enum ProcessErrorEnum : int
    {
        /// <summary>
        /// NO_ERROR
        /// </summary>
        // NO_ERROR = 0,
        PROC_OK = 0,

        /// <summary>
        /// ERR001: Errore software interno
        /// </summary>
        ERR_PRC001 = 1,

        /// <summary>
        /// ERR013: Impianto in esecuzione
        /// </summary>
        ERR_PRC013 = 13,

        /// <summary>
        /// ERR036: Selezione utensile non OK
        /// </summary>
        ERR_PRC036 = 36,

        /// <summary>
        /// ERR124: Errore allocazione memoria
        /// </summary>
        ERR_PRC124 = 124,

        /// <summary>
        /// ERR130: Piano operazione non selezionato
        /// </summary>
        ERR_PRC130 = 130,

        /// <summary>
        /// ERR131: Ciclo foro doppio non possibile
        /// </summary>
        ERR_PRC131 = 131,

        /// <summary>
        /// ERR132: Libreria delle macro di taglio non trovata
        /// </summary>
        ERR_PRC132 = 132,

        /// <summary>
        /// ERR133: Errore caricamento librerie 
        /// </summary>
        ERR_PRC133 = 133,

        /// <summary>
        /// ERR134: Errore elaborazione macro: macro non abilitata 
        /// </summary>
        ERR_PRC134 = 134,

        /// <summary>
        /// ERR135: Quantità produzione = 0
        /// </summary>
        ERR_PRC135 = 135,

        /// <summary>
        /// ERR136: Sequenza non completa
        /// </summary>
        ERR_PRC136 = 136,

        /// <summary>
        /// ERR137: Diametro non specificato
        /// </summary>
        ERR_PRC137 = 137,

        /// <summary>
        /// ERR138: Quantità stock = 0
        /// </summary>
        ERR_PRC138 = 138,

        /// <summary>
        /// ERR139: Tipo di elaborazione non univoca per tutte le lastre
        /// </summary>
        ERR_PRC139 = 139,

        /// <summary>
        /// ERR140: Corrente di taglio non univoca per tutte le lastre
        /// </summary>
        ERR_PRC140 = 140,

        /// <summary>
        /// ERR141: Modalità di passata finale di tagli non univoca per tutte le lastre
        /// </summary>
        ERR_PRC141 = 141,

        // Inserire da qui fino al 160 compreso i nuovi errori di Polaris
        
        /// <summary>
        /// ERR142: Nesting non implementato per questo tipo di programma
        /// </summary>
        ERR_PRC142 = 142,

        /// <summary>
        /// ERR143: Nesting non valido: lunghezza accumulata supera la lunghezza della barra
        /// </summary>
        ERR_PRC143 = 143,

        /// <summary>
        /// ERR144: Nesting non valido: Numero pezzi supera il massimo consentito
        /// </summary>
        ERR_PRC144 = 144,

        /// <summary>
        /// ERR145: Nesting non valido: numero ripetizioni del pezzo non corrette 
        /// </summary>
        ERR_PRC145 = 145,

        /// <summary>
        /// ERR146: Librerie macro non compatibili con versione attuale del software   
        /// </summary>
        ERR_PRC146 = 146,

        /// <summary>
        /// ERR221: Utensile di taglio non OK in ToolMng
        /// </summary>
        ERR_PRC221 = 221,

        /// <summary>
        /// ERR222: Modalità di elaborazione non OK
        /// </summary>
        ERR_PRC222 = 222,

        /// <summary>
        /// ERR242: Quantità programma = 0
        /// </summary>
        ERR_PRC242 = 242,

        /// <summary>
        /// ERR245: Sintassi errata
        /// </summary>
        ERR_PRC245 = 245,

        /// <summary>
        /// ERR246: Raggio di curvatura < kerf
        /// </summary>
        ERR_PRC246 = 246,

        /// <summary>
        /// ERR247: Calcolo compensazione
        /// </summary>
        ERR_PRC247 = 247,

        /// <summary>
        /// ERR248: Circonferenze con centri coincidenti
        /// </summary>
        ERR_PRC248 = 248,

        /// <summary>
        /// ERR249: Retta/Circonferenza senza intersezioni
        /// </summary>
        ERR_PRC249 = 249,

        /// <summary>
        /// ERR250: Circonferenze senza intersezioni
        /// </summary>
        ERR_PRC250 = 250,

        /// <summary>
        /// ERR251: Raggio troppo piccolo
        /// </summary>
        ERR_PRC251 = 251,

        /// <summary>
        /// ERR252: Tratto di circonferenza nulla
        /// </summary>
        ERR_PRC252 = 252,

        /// <summary>
        /// ERR253: Kerf negativo
        /// </summary>
        ERR_PRC253 = 253,

        /// <summary>
        /// ERR256: Utensile di taglio non configurato
        /// </summary>
        ERR_PRC256 = 256,

        /// <summary>
        /// ERR257: Utensile non trovato in tabella ossitaglio
        /// </summary>
        ERR_PRC257 = 257,

        /// <summary>
        /// ERR264: Dati materiale non corretti
        /// </summary>
        ERR_PRC264 = 264,

        /// <summary>
        /// ERR271: Tipo di linea non ammessa
        /// </summary>
        ERR_PRC271 = 271,

        /// <summary>
        /// ERR273: Richiesta compensazione
        /// </summary>
        ERR_PRC273 = 273,

        /// <summary>
        /// ERR274: Valore errato X e/o Y
        /// </summary>
        ERR_PRC274 = 274,

        /// <summary>
        /// ERR275: Tipo di interpolazione non ammessa
        /// </summary>
        ERR_PRC275 = 275,

        /// <summary>
        /// ERR276: Utensile non trovato in tabella plasma
        /// </summary>
        ERR_PRC276 = 276,

        /// <summary>
        /// ERR298: Macro param. A non corretto
        /// </summary>
        ERR_PRC298 = 298,

        /// <summary>
        /// ERR299: Macro param. B non corretto
        /// </summary>
        ERR_PRC299 = 299,

        /// <summary>
        /// ERR300: Macro param. C non corretto
        /// </summary>
        ERR_PRC300 = 300,

        /// <summary>
        /// ERR301: Macro param. D non corretto
        /// </summary>
        ERR_PRC301 = 301,

        /// <summary>
        /// ERR302: Macro param. E non corretto
        /// </summary>
        ERR_PRC302 = 302,

        /// <summary>
        /// ERR303: Macro param. R non corretto
        /// </summary>
        ERR_PRC303 = 303,

        /// <summary>
        /// ERR304: Macro param. ALFA non corretto
        /// </summary>
        ERR_PRC304 = 304,

        /// <summary>
        /// ERR305: Macro param. BETA non corretto
        /// </summary>
        ERR_PRC305 = 305,

        /// <summary>
        /// ERR306: Macro non configurata
        /// </summary>
        ERR_PRC306 = 306,

        /// <summary>
        /// ERR307: Macro non OK per il profilo
        /// </summary>
        ERR_PRC307 = 307,

        /// <summary>
        /// ERR313: Macro param. F non corretto
        /// </summary>
        ERR_PRC313 = 313,

        /// <summary>
        /// ERR314: Macro param. G non corretto
        /// </summary>
        ERR_PRC314 = 314,

        /// <summary>
        /// ERR315: Macro param. H non corretto
        /// </summary>
        ERR_PRC315 = 315,

        /// <summary>
        /// ERR316: Macro param. I non corretto
        /// </summary>
        ERR_PRC316 = 316,

        /// <summary>
        /// ERR317: Macro param. L non corretto
        /// </summary>
        ERR_PRC317 = 317,

        /// <summary>
        /// ERR318: Macro param. M non corretto
        /// </summary>
        ERR_PRC318 = 318,

        /// <summary>
        /// ERR319: Macro param. S non corretto
        /// </summary>
        ERR_PRC319 = 319,

        /// <summary>
        /// ERR327: Tagli inclinati ali non eseguibili
        /// </summary>
        ERR_PRC327 = 327,

        /// <summary>
        /// ERR330: Programma non corretto
        /// </summary>
        ERR_PRC330 = 330,

        /// <summary>
        /// ERR345: Larghezza profilo oltre luce sega
        /// </summary>
        ERR_PRC345 = 345,

        /// <summary>
        /// ERR347: Operazione non ammessa con calibro
        /// </summary>
        ERR_PRC347 = 347,

        /// <summary>
        /// ERR350: Distanza tra pezzi non OK
        /// </summary>
        ERR_PRC350 = 350,

        /// <summary>
        /// ERR358: Tagli inclinati anima non eseguibili
        /// </summary>
        ERR_PRC358 = 358,

        /// <summary>
        /// ERR362: Carattere non in Setup marcatrice
        /// </summary>
        ERR_PRC362 = 362,

        /// <summary>
        /// ERR367: Macro non OK per il tipo di impianto
        /// </summary>
        ERR_PRC367 = 367,

        /// <summary>
        /// ERR373: Dati profilo non corretti
        /// </summary>
        ERR_PRC373 = 373,

        /// <summary>
        /// ERR375: Dati profilo non corretti
        /// </summary>
        ERR_PRC375 = 375,

        /// <summary>
        /// ERR378: Stringa di marcatura non OK
        /// </summary>
        ERR_PRC378 = 378,

        /// <summary>
        /// ERR382: Macro param. J non corretto
        /// </summary>
        ERR_PRC382 = 382,

        /// <summary>
        /// ERR383: Macro param. K non corretto
        /// </summary>
        ERR_PRC383 = 383,

        /// <summary>
        /// ERR384: Macro param. N non corretto
        /// </summary>
        ERR_PRC384 = 384,

        /// <summary>
        /// ERR385: Macro param. O non corretto
        /// </summary>
        ERR_PRC385 = 385,

        /// <summary>
        /// ERR386: Macro param. P non corretto
        /// </summary>
        ERR_PRC386 = 386,

        /// <summary>
        /// ERR387: Macro param. Q non corretto
        /// </summary>
        ERR_PRC387 = 387,

        /// <summary>
        /// ERR388: Macro Mk: font mancante
        /// </summary>
        ERR_PRC388 = 388,

        /// <summary>
        /// ERR389: Macro Mk: dimensioni font errate
        /// </summary>
        ERR_PRC389 = 389,

        /// <summary>
        /// ERR390: Macro Mk: errore macro carattere
        /// </summary>
        ERR_PRC390 = 390,

        /// <summary>
        /// ERR391: Macro Mk: direttorio macro mancante
        /// </summary>
        ERR_PRC391 = 391,

        /// <summary>
        /// ERR392: Mk-fresa: macro carattere non definita
        /// </summary>
        ERR_PRC392 = 392,

        /// <summary>
        /// ERR395: Diametro utensile non corretto
        /// </summary>
        ERR_PRC395 = 395,

        /// <summary>
        /// ERR399: Profondità cianfrino troppo grande
        /// </summary>
        ERR_PRC399 = 399,

        /// <summary>
        /// ERR400: Profondità di taglio nulla non ammessa
        /// </summary>
        ERR_PRC400 = 400,

        /// <summary>
        /// ERR405: Libreria delle macro di fresatura non presente
        /// </summary>
        ERR_PRC405 = 405,

        /// <summary>
        /// ERR407: Distanza minima tra V e W
        /// </summary>
        ERR_PRC407 = 407,

        /// <summary>
        /// ERR408: Operazione non ammessa
        /// </summary>
        ERR_PRC408 = 408,

        /// <summary>
        /// ERR409: Angolo utensile non OK
        /// </summary>
        ERR_PRC409 = 409,

        /// <summary>
        /// ERR412: Utensile foratura non trovato in TOOL
        /// </summary>
        ERR_PRC412 = 412,

        /// <summary>
        /// ERR413: Utensile plasma non trovato in TOOL
        /// </summary>
        ERR_PRC413 = 413,

        /// <summary>
        /// ERR414: Origini NON OK
        /// </summary>
        ERR_PRC414 = 414,

        /// <summary>
        /// ERR415: Utensile ossitaglio non trovato in TOOL
        /// </summary>
        ERR_PRC415 = 415,

        /// <summary>
        /// ERR434: Distanza minima tra le torce
        /// </summary>
        ERR_PRC434 = 434,

        /// <summary>
        /// ERR436: Percorso non eseguibile
        /// </summary>
        ERR_PRC436 = 436,

        /// <summary>
        /// ERR438: 
        /// </summary>
        ERR_PRC438 = 438,

        /// <summary>
        /// ERR442: Posizione pinza CC obbligatoria
        /// </summary>
        ERR_PRC442 = 442,

        /// <summary>
        /// ERR443: Asse Ausiliario non configurato
        /// </summary>
        ERR_PRC443 = 443,

        /// <summary>
        /// ERR444: Posizione pinze A-B non corrette
        /// </summary>
        ERR_PRC444 = 444,

        /// <summary>
        /// ERR448: Inclinazione pezzo non ammessa
        /// </summary>
        ERR_PRC448 = 448,

        /// <summary>
        /// ERR450: Percorso con B variabile non OK
        /// </summary>
        ERR_PRC450 = 450,

        /// <summary>
        /// ERR453: Kerf e/o velocità non corrette
        /// </summary>
        ERR_PRC453 = 453,

        /// <summary>
        /// ERR455: Operazione in ingombro pinza
        /// </summary>
        ERR_PRC455 = 455,

        /// <summary>
        /// ERR472: 
        /// </summary>
        ERR_PRC472 = 472,

        /// <summary>
        /// ERR474: Macro param. T non corretto
        /// </summary>
        ERR_PRC474 = 474
    }

    #endregion < Definizione errori di elaborazione >
}
