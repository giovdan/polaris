namespace Mitrol.Framework.Domain.Macro
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel;
    
    /// <summary>
    /// Enumerativo degli attributi che vengono condivisi tra Be e DLL esterna (ad per elaborazione Macro)
    /// </summary>
    public enum ExternalInterfaceNameEnum
    {
        NotDefined=0,
        /// <summary>
        /// Attributo relativo all'oggetto Macro [oggetto MacroParameters]
        /// </summary>
        [Description("Attributo relativo all'oggetto Macro ")]
        Macro =4,

        /// <summary>
        /// Attributo relativo all'oggetto Pezzo [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo all'oggetto Pezzo")]
        Piece =5,

        /// <summary>
        /// Attributo relativo all'oggetto Program [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo all'oggetto Programma")]
        Program = 6,

        /// <summary>
        /// Nome della Macro [string]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Nome della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut|MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.MacroName)]
        Name=7,

        /// <summary>
        /// Parametro A della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Parametro A della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                               , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                               , databaseDisplayName: DatabaseDisplayNameEnum.A)]
        ParamA = 8,

        /// <summary>
        /// Parametro B della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Parametro B della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                               , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                               , databaseDisplayName: DatabaseDisplayNameEnum.B)]
        ParamB = 9,

        /// <summary>
        /// Parametro C della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Parametro C della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.C)]
        ParamC = 10,

        /// <summary>
        /// Parametro D della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Parametro D della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.D)]
        ParamD = 11,

        /// <summary>
        /// Parametro E della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Parametro E della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.E)]
        ParamE = 12,

        /// <summary>
        /// Parametro F della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Parametro F della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.F)]
        ParamF = 13,

        /// <summary>
        /// Parametro G della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Parametro G della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.G)]
        ParamG = 14,

        /// <summary>
        /// Parametro H della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Parametro H della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.H)]
        ParamH = 15,

        /// <summary>
        /// Parametro I della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Parametro I della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.I)]
        ParamI = 16,

        /// <summary>
        /// Parametro J della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Parametro J della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.J)]
        ParamJ = 17,

        /// <summary>
        /// Parametro K della Macro [float]
        /// </summary>
        /// <remarks>Definito in Macro per MacroMill</remarks>
        [Description("Parametro K della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.K)]
        ParamK = 18,

        /// <summary>
        /// Parametro L della Macro [float]
        /// </summary>
        /// <remarks>Definito in Macro per MacroMill</remarks>
        [Description("Parametro L della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.L)]
        ParamL = 19,

        /// <summary>
        /// Parametro M della Macro [float]
        /// </summary>
        /// <remarks>Definito in Macro per MacroMill</remarks>
        [Description("Parametro M della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.M)]
        ParamM = 20,

        /// <summary>
        /// Parametro N della Macro [float]
        /// </summary>
        /// <remarks>Definito in Macro per MacroMill</remarks>
        [Description("Parametro N della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.N)]
        ParamN = 21,

        /// <summary>
        /// Parametro O della Macro [float]
        /// </summary>
        /// <remarks>Definito in Macro per MacroMill</remarks>
        [Description("Parametro O della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                               , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                               , databaseDisplayName: DatabaseDisplayNameEnum.O)]
        ParamO = 22,

        /// <summary>
        /// Parametro P della Macro [float]
        /// </summary>
        /// <remarks>Definito in Macro per MacroMill</remarks>
        [Description("Parametro P della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.P)]
        ParamP = 23,

        /// <summary>
        /// Parametro Q della Macro [float]
        /// </summary>
        /// <remarks>Definito in Macro per MacroMill</remarks>
        [Description("Parametro Q della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.Q)]
        ParamQ = 24,

        /// <summary>
        /// Parametro R della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroMill;
        /// Definito in Macro per MacroCut;
        /// </remarks>
        [Description("Parametro R della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.R)]
        ParamR = 25,

        /// <summary>
        /// Parametro S della Macro [float]
        /// </summary>
        /// <remarks>Definito in Macro per MacroMill</remarks>
        [Description("Parametro S della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.S)]
        ParamS = 26,

        /// <summary>
        /// Parametro T della Macro [float]
        /// </summary>
        /// <remarks>Definito in Macro per MacroMill</remarks>
        [Description("Parametro T della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.T)]
        ParamT = 27,

        /// <summary>
        /// Parametro ALFA della Macro [float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroCut;
        /// Definito in Macro per MacroMill;
        /// </remarks>
        [Description("Parametro ALFA della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.Alfa)]
        ParamAlfa = 28,

        /// <summary>
        /// Parametro BETA della Macro [..,float]
        /// </summary>
        /// <remarks>Definito in Macro per MacroMill</remarks>
        [Description("Parametro BETA della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.Beta)]
        ParamBeta = 29,

        /// <summary>
        /// Posizione X della Macro [mm,float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroCut;
        /// Definito in MarkingTool per MacroCut;
        /// Definito in Macro per MacroMill;
        /// </remarks>
        [Description("Posizione X della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.X)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut 
                                , macroContextDataEnum: ExternalInterfaceNameEnum.MarkingTool
                                , databaseDisplayName: DatabaseDisplayNameEnum.MarkX)]
        X = 30,

        /// <summary>
        /// Posizione Y della Macro [mm,float]
        /// </summary>
        /// <remarks>
        /// Definito in Macro per MacroCut;
        /// Definito in MarkingTool per MacroCut;
        /// Definito in Macro per MacroMill
        /// </remarks>
        [Description("Posizione Y della Macro")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.Y)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut
                                , macroContextDataEnum: ExternalInterfaceNameEnum.MarkingTool
                                , databaseDisplayName: DatabaseDisplayNameEnum.MarkY)]
        Y = 31,

        /// <summary>
        /// Numero di ripetizioni lungo X [AsIs,intero]
        /// </summary>
        /// <remarks>Definito in Macro per MacroCut</remarks>
        [Description("Numero di ripetizioni lungo X")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut
                               , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                               , databaseDisplayName: DatabaseDisplayNameEnum.RepetitionOnX)]
        RPX = 32,

        /// <summary>
        /// Numero di ripetizioni lungo Y [AsIs,intero]
        /// </summary>
        /// <remarks>Definito in Macro per MacroCut</remarks>
        [Description("Numero di ripetizioni lungo Y")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.RepetitionOnY)]
        RPY = 33,

        /// <summary>
        /// Distanza tra le ripetizioni [mm,float]
        /// </summary>
        /// <remarks>Definito in Macro per MacroCut</remarks>
        [Description("Distanza tra le ripetizioni")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.RepetitionDistance)]
        DIST = 34,

        /// <summary>
        /// Lunghezza pezzo [mm,float]
        /// </summary>
        /// <remarks>Definito in Piece(old "lp") per MacroCut</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Piece)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Program)]
        Length =35,

        /// <summary>
        /// Spessore [mm,float]
        /// </summary>
        /// <remarks>
        /// Definito in Piece per MacroCut;
        /// Definito in Piece per MacroMill(TA);
        /// </remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut|MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Program)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Piece)]
        Thickness = 36,

        /// <summary>
        /// Larghezza pezzo [mm,float]
        /// </summary>
        /// <remarks>Definito in Piece(old "sa") per MacroCut</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Piece)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Program)]
        Width = 37,

        /// <summary>
        /// Attributo relativo al 1° utensile di fresatura [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo al 1° utensile di fresatura")]
        Tool_1 = 38,

        /// <summary>
        /// Attributo relativo al 2° utensile di fresatura [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo al 2° utensile di fresatura")]
        Tool_2 = 39,

        /// <summary>
        /// Attributo relativo al 3° utensile di fresatura [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo al 3° utensile di fresatura")]
        Tool_3 = 40,

        /// <summary>
        /// Attributo relativo al 4° utensile di fresatura [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo al 4° utensile di fresatura")]
        Tool_4 = 41,

        /// <summary>
        /// Attributo relativo al 5° utensile di fresatura [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo al 5° utensile di fresatura")]
        Tool_5 = 42,

        /// <summary>
        /// Attributo relativo al 6° utensile di fresatura [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo al 6° utensile di fresatura")]
        Tool_6 = 43,

        /// <summary>
        /// Attributo relativo all'utensile A per Preforo [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo all'utensile A per Preforo")]
        Tool_PreForoA = 44,

        /// <summary>
        /// Attributo relativo all'utensile B per Preforo [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo all'utensile B per Preforo")]
        Tool_PreForoB = 45,

        /// <summary>
        /// Attributo relativo all'utensile C per Preforo [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo all'utensile C per Preforo")]
        Tool_PreForoC = 46,

        /// <summary>
        /// Attributo relativo all'utensile di taglio [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo all'utensile di taglio")]
        Tool_Cut = 47,

        /// <summary>
        /// TS utensile [AsIs,Enum ToolTypeEnum]
        /// </summary>
        /// <remarks>
        /// Definito in  Tool_Cut, Tool_PreForoC per MacroCut;
        /// Definito in Tool_1,Tool_2,Tool_3,Tool_4,Tool_5,Tool_6,Tool_Probe per MacroMill;
        /// </remarks>
        [Description("TS utensile")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_Cut
                                , databaseDisplayName: DatabaseDisplayNameEnum.TS)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_Probe)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut| MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_PreForoC
                                , databaseDisplayName: DatabaseDisplayNameEnum.PreHoleTS)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_1)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_2)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_3)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_4)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_5)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_6)]
        TS = 48,

        /// <summary>
        /// Diametro utensile teorico [mm,float]
        /// </summary>
        /// <remarks>
        /// Definito in Tool_PreForoC per MacroCut;
        /// Definito in Tool_1,Tool_2,Tool_3,Tool_4,Tool_5,Tool_6 per MacroMill;
        /// </remarks>
        [Description("Diametro utensile teorico")]     
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut|MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_PreForoC
                                , databaseDisplayName: DatabaseDisplayNameEnum.PreHoleDN)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_1)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_2)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_3)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_4)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_5)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_6)]
        DN = 49,

        /// <summary>
        /// Codice utensile [string]
        /// </summary>
        /// <remarks>
        /// Definito in Tool_PreForoC per MacroCut;
        /// Definito in Tool_1,Tool_2,Tool_3,Tool_4,Tool_5,Tool_6 per MacroMill;
        /// </remarks>
        [Description("Codice utensile")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_PreForoC
                                , databaseDisplayName: DatabaseDisplayNameEnum.PreHoleCOD)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_1)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_2)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_3)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_4)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_5)]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_6)]
        COD = 50,

        /// <summary>
        /// Attributo per oggetto Marking [oggetto MacroItemAttributes]
        /// </summary> 
        [Description("Marking ")]
        MarkingTool = 51,

        /// <summary>
        /// Angolo Marcatura [gradi, numero]
        /// </summary> 
        /// <remarks>Definito in MarkingTool per MacroCut</remarks>
        [Description("Angolo marcatura ")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut
                                , macroContextDataEnum: ExternalInterfaceNameEnum.MarkingTool
                                , databaseDisplayName: DatabaseDisplayNameEnum.MarkAngle)]
        Angle = 52,

        /// <summary>
        /// Codice Marcatura [string]
        /// </summary> 
        /// <remarks>Definito in MarkingTool per MacroCut</remarks>
        [Description("Codice marcatura ")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut
                                , macroContextDataEnum: ExternalInterfaceNameEnum.MarkingTool
                                , databaseDisplayName: DatabaseDisplayNameEnum.MarkingCodeString)]
        MarkingCode = 53,

        /// <summary>
        /// Font per Marcatura [numero,intero]
        /// </summary> 
        /// <remarks>Definito in MarkingTool per MacroCut</remarks>
        [Description("Font per marcatura ")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut
                                , macroContextDataEnum: ExternalInterfaceNameEnum.MarkingTool
                                , databaseDisplayName: DatabaseDisplayNameEnum.FontType)]
        MarkingFont = 54,

        /// <summary>
        /// Attributo relativo all'utensile di palpatura [oggetto MacroItemAttributes]
        /// </summary>
        [Description("Attributo relativo all'utensile di palpatura")]
        Tool_Probe = 55,

        /// <summary>
        /// M di palpatura programmata [AsIs,ProbeCodeEnum]
        /// </summary> 
        /// <remarks>Definito in Macro per MacroMill</remarks>
        [Description("M di palpatura programmata")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.ProbeM)]
        PalpM = 56,
        /*
        /// <summary>
        ///  [mm]
        /// </summary>
        /// <remarks></remarks>
        [Description("")]
        EC = 57,
        */
        /// <summary>
        /// Attributo relativo alla posizione di X fornita dall'elaborazione della Macro [mm,float]
        /// </summary>
        [Description("Attributo relativo alla posizione di X")]
        Pos_X = 58,

        /// <summary>
        /// Attributo relativo alla posizione di X di palpatura fornita dall'elaborazione della Macro [mm,float]
        /// </summary>
        [Description("Attributo relativo alla posizione di X di palpatura")]
        Palp_X = 59,

        /// <summary>
        /// Attributo relativo alla posizione di Y di palpatura fornita dall'elaborazione della Macro [mm,float]
        /// </summary>
        [Description("Attributo relativo alla posizione di Y di palpatura")]
        Palp_Y = 60,

        /// <summary>
        ///Attributo relativo ai parametri addizionali di Macro
        /// </summary>
        [Description("Attributo relativo ai parametri addizionali di Macro")]
        MacroAdditional = 61,

        /// <summary>
        /// Attributo relativo ai parametri additionali del Programma
        /// </summary>
        [Description("Attributo relativo ai parametri additionali del Programma")]
        ProgramAdditional = 62,

        ///// <summary>
        ///// Attributo Diametro Reale [float,mm]
        ///// </summary>
        ///// <remarks>Attributo per tool</remarks>
        //[Description("Attributo Diametro Reale")]
        //RealDiameter = 63,

        /// <summary>
        /// Attributo Kerf [float,mm]
        /// </summary>
        /// <remarks>Attributo per tool</remarks>
        [Description("Attributo Kerf")]
        Kerf = 64,

        /// <summary>
        /// Attributo Corrente di plasma [float,A]
        /// </summary>
        /// <remarks>Attributo Corrente di plasma</remarks>
        [Description("Attributo corrente di plasma")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_Cut
                                , databaseDisplayName: DatabaseDisplayNameEnum.AM)]
        PlasmaCurrent =65,

        /// <summary>
        /// Attributo Identificatore ossitaglio [string,]
        /// </summary>
        /// <remarks>Attributo Identificatore ossitaglio</remarks>
        [Description("Attributo Identificatore ossitaglio")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Tool_Cut
                                , databaseDisplayName: DatabaseDisplayNameEnum.NS)]
        OXYCode = 66,

        /// <summary>
        /// Attributo profondità fresatura(ex EF)
        /// </summary>
        /// <remarks></remarks>
        [Description("Attributo profondità fresatura(ex EF)")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Macro
                                , databaseDisplayName: DatabaseDisplayNameEnum.Depth)]
        Depth = 67,

        /// <summary>
        /// Attributo lato su cui si vuole operare [stringa,"DC","DA","DB","DC"]
        /// </summary>
        /// <remarks></remarks>
        [Description("Attributo piano (ex DA DB DC)")]
        Side = 68,

        /// <summary>
        /// Attributo velocità di contornitura(ex. VEL????) [float,mm/min, tools.ini -> F]
        /// </summary>
        /// <remarks>Attributo per tool(61,62,67,69,71,75,68,76,77,78,79)</remarks>
        [Description("Attributo velocità")]
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ContouringSpeed)]
        ContouringSpeed = 69,

        /// <summary>
        /// Attributo che rappresenta l'operazione da eseguire [Enumerativo OperationSideEnum]
        /// </summary>
        /// <remarks></remarks>
        [Description("Attributo che rappresenta l'operazione da eseguire")]
        OperationType=70,

        /// <summary>
        /// Attributo che rappresenta un generico diametro (ad es. richiesto per foro fresato..) [float,mm]
        /// </summary>
        /// <remarks></remarks>
        [Description("Attributo che rappresenta un generico diametro")]
        Diameter = 71,

        /// <summary>
        /// Attributo che rappresenta il diametro del preforo (ad es. richiesto per foro fresato..) [float,mm]
        /// </summary>
        /// <remarks></remarks>
        [Description("Attributo che rappresenta il diametro del preforo")]
        PreHoleDiameter = 72,

        /// <summary>
        /// Attributo che il valore dell'attributo SR (ad es. 1433)[intero]
        /// </summary>
        /// <remarks></remarks>
        [Description("Attributo che il valore dell'attributo SR (ad es.1433)")]
        SR = 73,

        /// <summary>
        /// Attributo che rappresenta la quota X di sicurezza per la fresa [float,mm]
        /// </summary>
        /// <remarks></remarks>
        [Description("Attributo che rappresenta la quota X di sicurezza per la fresa")]
        XOut = 74,

        /// <summary>
        /// Attributo che rappresenta la quota Y di sicurezza per la fresa [float,mm]
        /// </summary>
        /// <remarks></remarks>
        [Description("Attributo che rappresenta la quota Y di sicurezza per la fresa")]
        YOut = 75,

        /// <summary>
        /// Attributo che rappresenta la tipologia di asse ausiliario da usare [int]  
        /// </summary>
        /// <remarks>
        /// =0 lavorazione senza assi ausiliari, viene usato l'asse X;
        /// =1 lavorazione con assi ausiliari senza congelamento (default);
        /// </remarks>
        [Description("Attributo che rappresenta la tipologia di asse ausiliario da usare")]
        Aux = 76,

        /// <summary>
        /// Attributo che rappresenta l'indice del punto di palpatura [int]  
        /// </summary>
        [Description("Attributo che rappresenta l'indice del punto di palpatura")]
        PN = 77,

        /// <summary>
        /// Attributo che rappresenta la modalità di attacco della fresa(LEAD) [int]  
        /// </summary>
        /// <remarks> 
        /// =0 standard;
        /// =1 ciclo di fresatura senza salita della testa;
        /// </remarks>
        [Description("Attributo che rappresenta la modalità di attacco della fresa")]
        LT = 78,

        /// <summary>
        /// Attributo Diametro Reale [float,mm, tools.ini -> DT]
        /// </summary>
        /// <remarks>Attributo per tool(TUTTI I TOOL)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                                , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                                , databaseDisplayName: DatabaseDisplayNameEnum.RealDn)]
        RealDn = 79,

        /// <summary>
        /// Attributo Lunghezza utensile  [float,mm, tools.ini -> LT]
        /// </summary>
        /// <remarks>Attributo per tool(TUTTI I TOOL)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                                , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                                , databaseDisplayName: DatabaseDisplayNameEnum.ToolLength)]
        ToolLength = 80,


        /// <summary>
        /// Attributo Codice del ToolHolder [int]
        /// </summary>
        /// <remarks>Attributo per tool(TUTTI I TOOL)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ToolHolderCode)]
        ToolHolderCode = 81,

        /// <summary>
        /// Attributo Angolo Affilatura punta  [float,mm, tools.ini -> ANG]
        /// </summary>
        /// <remarks>Attributo per tool(32,33,62,74,75)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.GrindingAngle)]
        GrindingAngle = 82,

        /// <summary>
        /// Attributo Preset tool sbavatore  [float,mm, tools.ini -> TK]
        /// </summary>
        /// <remarks>Attributo per tool(36)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.PresetDeburringTool)]
        PresetDeburringTool = 83,

        /// <summary>
        /// Attributo Diametro placchete  [float,mm, tools.ini -> DNP]
        /// </summary>
        /// <remarks>Attributo per tool(61,62,67,69,71)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MillingInsertInscribedCircle)]
        MillingInsertInscribedCircle = 84,

        /// <summary>
        /// Attributo Raggio spigolo placchete quadre  [float,mm, tools.ini -> RSP]
        /// </summary>
        /// <remarks>Attributo per tool(61,62,67,69,71)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MillingCornerRadius)]
        MillingCornerRadius = 85,

        /// <summary>
        /// Attributo Diametro gambo fresa  [float,mm, tools.ini -> DNG]
        /// </summary>
        /// <remarks>Attributo per tool(61,62,67,69,71,75)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MillingShankDiameter)]
        MillingShankDiameter = 86,

        /// <summary>
        /// Attributo Altezza gambo fresa  [float,mm, tools.ini -> HG]
        /// </summary>
        /// <remarks>Attributo per tool(61,62,67,69,71,75)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MillingShankHeight)]
        MillingShankHeight = 87,

        /// <summary>
        /// Attributo Lunghezza del tagliente  [float,mm]
        /// </summary>
        /// <remarks>Attributo per tool(62,75)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CuttingEdge)]
        CuttingEdge = 88,

        /// <summary>
        /// Attributo offset tagliente  [float,mm]
        /// </summary>
        /// <remarks>Attributo per tool(62,75)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CuttingEdgeOff)]
        CuttingEdgeOff = 89,

        /// <summary>
        /// Attributo Max angolo rampa  [float,mm]
        /// </summary>
        /// <remarks>Attributo per tool(62)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MaxRampAngle)]
        MaxRampAngle = 90,

        /// <summary>
        /// Attributo Ciclo con rotazione inversa [bool, tools.ini -> ROTI]
        /// </summary>
        /// <remarks>Attributo per tool(62,67,75)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ReverseRotationCycle)]
        ReverseRotationCycle = 91,

        /// <summary>
        /// Attributo Passo min maschiatura con fresa [float, mm, tools.ini -> PPMIN]
        /// </summary>
        /// <remarks>Attributo per tool(71)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MinTappingPitch)]
        MinTappingPitch = 92,

        /// <summary>
        /// Attributo Passo max maschiatura con fresa [float, mm, tools.ini -> PPMAX]
        /// </summary>
        /// <remarks>Attributo per tool(71)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MaxTappingPitch)]
        MaxTappingPitch = 93,

        /// <summary>
        /// Attributo Numero di denti fresa ad inserti per maschiatura [int, asIs, tools.ini -> NDEN]
        /// </summary>
        /// <remarks>Attributo per tool(71)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MillingCutterTeeth)]
        MillingCutterTeeth = 94,

        /// <summary>
        /// Attributo Angolo di affilatura svasatore [float, gradi, tools.ini -> SVAANG]
        /// </summary>
        /// <remarks>Attributo per tool(73)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CountersinkAngle)]
        CountersinkAngle = 95,

        /// <summary>
        /// Attributo Altezza punta svasatore [float, mm, tools.ini -> SVAHT]
        /// </summary>
        /// <remarks>Attributo per tool(73)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CountersinkHeight)]
        CountersinkHeight = 96,

        /// <summary>
        /// Attributo Diametro della punta  per forare dello svasatore [float, mm, tools.ini -> SVADNF]
        /// </summary>
        /// <remarks>Attributo per tool(73)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CountersinkDrillingDiameter)]
        CountersinkDrillingDiameter = 97,

        /// <summary>
        /// Attributo accostamento sbavatura esterna [float, mm, tools.ini -> SBARAE]
        /// </summary>
        /// <remarks>Attributo per tool(74)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ApproachExternalDeburr)]
        ApproachExternalDeburr = 98,

        /// <summary>
        /// Attributo Lavoro sbavatura esterna [float, mm, tools.ini -> SBAEFE]
        /// </summary>
        /// <remarks>Attributo per tool(74)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.WorkExternalDeburr)]
        WorkExternalDeburr = 99,

        /// <summary>
        /// Attributo accostamento sbavatura interna [float, mm, tools.ini -> SBARAI]
        /// </summary>
        /// <remarks>Attributo per tool(74)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ApproachInternalDeburr)]
        ApproachInternalDeburr = 100,

        /// <summary>
        /// Attributo lavoro sbavatura interna [float, mm, tools.ini -> SBAEFI]
        /// </summary>
        /// <remarks>Attributo per tool(74)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.WorkInternalDeburr)]
        WorkInternalDeburr = 101,


        /// <summary>
        /// Attributo velocità di taglio [float, mt/min]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CuttingSpeed)]
        CuttingSpeed = 102,

        /// <summary>
        /// Attributo velocità di avanzamento [float, mm/rev]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ForwardSpeed)]
        ForwardSpeed = 103,

        /// <summary>
        /// Attributo sforzo sulla punta [float, %, tools.ini -> SFO]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.TorqueFeedRate)]
        TorqueFeedRate = 104,

        /// <summary>
        /// Attributo coeff. vita utensile [float, tools.ini -> PLT]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ToolLifeCoefficient)]
        ToolLifeCoefficient = 105,

        /// <summary>
        /// Attributo tipo di lubrificazione [intero ]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.LubrificationType)]
        LubrificationType = 106,

        /// <summary>
        /// Attributo frequenza di lubrificazione [intero ]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.LubrificationFrequency)]
        LubrificationFrequency = 107,

        /// <summary>
        /// Attributo scarico truciolo [bool, tools.ini -> SCA]
        /// </summary>
        /// <remarks>Attributo per tool(32,33,35,74)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.PeckFeed)]
        PeckFeed = 108,

        /// <summary>
        /// Attributo Posizione di interruzione [float,mm, tools.ini -> IA]
        /// </summary>
        /// <remarks>Attributo per tool(32,33,35,74)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.FirstChipBreakagePosition)]
        FirstChipBreakagePosition = 109,

        /// <summary>
        /// Attributo Passo rottura truciolo [float,mm, tools.ini -> IB]
        /// </summary>
        /// <remarks>Attributo per tool(32,33,35,74)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ChipPitchPosition)]
        ChipPitchPosition = 110,

        /// <summary>
        /// Attributo Quota di passaggio in vel.nom.di lavoro [float,mm, tools.ini -> SVN]
        /// </summary>
        /// <remarks>Attributo per tool(32,33,35,74)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ApproachPositionNominalSpeed)]
        ApproachPositionNominalSpeed = 111,

        /// <summary>
        /// Attributo posizione velocità fine foro [float,mm, tools.ini -> IF]
        /// </summary>
        /// <remarks>Attributo per tool(32,33,35,74)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.EndPositionFinalSpeed)]
        EndPositionFinalSpeed = 112,

        /// <summary>
        /// Attributo velocità avanzamento in entrata [float,mm/rev]
        /// </summary>
        /// <remarks>Attributo per tool(32,33,35,74)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.StartForwardSpeed)]
        StartForwardSpeed = 113,

        /// <summary>
        /// Attributo Profondità di passata [float,mm, tools.ini -> PP]
        /// </summary>
        /// <remarks>Attributo per tool(61,62,67,69,71,75,68,76,77,78,79)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CuttingDepth)]
        CuttingDepth = 115,

        /// <summary>
        /// Attributo diametro min foro [float,mm, tools.ini -> DNFMIN]
        /// </summary>
        /// <remarks>Attributo per tool(35,62)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MinHoleDiameter)]
        MinHoleDiameter = 116,

        /// <summary>
        /// Attributo diametro max foro [float,mm, tools.ini -> DNFMAX]
        /// </summary>
        /// <remarks>Attributo per tool(35,62)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MaxHoleDiameter)]
        MaxHoleDiameter = 117,

        /// <summary>
        /// Attributo ricoprimento radiale [float,mm]
        /// </summary>
        /// <remarks>Attributo per tool(62)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.RadialOverlap)]
        RadialOverlap = 118,

        /// <summary>
        /// Attributo velocità di avanzamento radiale [float,mm/min]
        /// </summary>
        /// <remarks>Attributo per tool(62)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.RadialFeed)]
        RadialFeed = 119,

        /// <summary>
        /// Attributo profondità di passata radiale [float,mm]
        /// </summary>
        /// <remarks>Attributo per tool(62)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.RadialDepth)]
        RadialDepth = 120,

        /// <summary>
        /// Attributo velocità di taglio svasatore [float,mt/min]
        /// </summary>
        /// <remarks>Attributo per tool(73)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CountersinkCuttingSpeed)]
        CountersinkCuttingSpeed = 121,

        /// <summary>
        /// Attributo velocità avanzamento svasatore [float,mm/rev]
        /// </summary>
        /// <remarks>Attributo per tool(73)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CountersinkForwardSpeed)]
        CountersinkForwardSpeed = 122,

        /// <summary>
        /// Attributo Posizione start foro [float,mm, tools.ini -> SF]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.StartPosition)]
        StartPosition = 123,

        /// <summary>
        /// Attributo Posizione accostamento rapido [float,mm, tools.ini -> RA]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.FastApproachPosition)]
        FastApproachPosition = 124,

        /// <summary>
        /// Attributo Posizione di Fine Foro [float,mm, tools.ini -> EF]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.EndPosition)]
        EndPosition = 125,

        /// <summary>
        /// Attributo tipi di materiale abilitati [num]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MaterialTypeEnable)]
        MaterialTypeEnable = 126,

        /// <summary>
        /// Attributo soglia affilatura punta [float,mt, tools.ini -> AFFLIFE]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.WarningToolLife)]
        WarningToolLife = 127,

        /// <summary>
        /// Attributo massima vita utensile [float,mt, tools.ini -> MAXLIFE]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.MaxToolLife)]
        MaxToolLife = 128,

        /// <summary>
        /// Attributo vita utensile corrente [float,mt, tools.ini -> TOOLLIFE]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ToolLife)]
        ToolLife = 129,

        /// <summary>
        /// Attributo limite raffreddamento punta [float,mt, tools.ini -> LIMRAF]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CoolingLimit)]
        CoolingLimit = 130,

        /// <summary>
        /// Attributo raffreddamento punta (valore corrente) [float,mt, tools.ini -> TOOLRAF]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CoolingValue)]
        CoolingValue = 131,

        /// <summary>
        /// Attributo prenotazione automatica del sensitivo [bool, tools.ini -> SENS]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.AutoSensitiveEnable)]
        AutoSensitiveEnable = 132,

        /// <summary>
        /// Attributo utensile con caricamento manuale dall'operatore [bool, tools.ini -> UTMAN]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ManualLoadingTool)]
        ManualLoadingTool = 133,

        /// <summary>
        /// Attributo utensile ad assegnazione manuale nel magazzino [bool, tools.ini -> MAGMAN]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ManualMagazineAssignmentTool)]
        ManualMagazineAssignmentTool = 134,

        /// <summary>
        /// Attributo abilitazione aspirazione trucioli [bool, tools.ini -> ASPI]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ChipExtractionEnable)]
        ChipExtractioneEnable = 135,

        /// <summary>
        /// Attributo tipologia di ciclo dell'utensile custom [num, tools.ini -> CST]
        /// </summary>
        /// <remarks>Attributo per tool(62)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.CustomToolType)]
        CustomToolType = 136,

        /// <summary>
        /// Attributo abilitazione utilizzo testa A [bool, tools.ini -> UTA]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ToolEnableA)]
        ToolEnableA = 137,

        /// <summary>
        /// Attributo abilitazione utilizzo testa B [bool, tools.ini -> UTB]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ToolEnableB)]
        ToolEnableB = 138,

        /// <summary>
        /// Attributo abilitazione utilizzo testa C [bool, tools.ini -> UTC]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ToolEnableC)]
        ToolEnableC = 139,

        /// <summary>
        /// Attributo abilitazione utilizzo testa D [bool, tools.ini -> UTD]
        /// </summary>
        /// <remarks>Attributo per tool(tutti)</remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroAll
                               , macroContextDataEnum: ExternalInterfaceNameEnum.ToolManagement
                               , databaseDisplayName: DatabaseDisplayNameEnum.ToolEnableD)]
        ToolEnableD = 140,

        ToolManagement =141,

        /// <summary>
        /// Offset X del pezzo [mm,float]
        /// </summary>
        /// <remarks>
        /// </remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Piece)]
        OffsetX = 142,

        /// <summary>
        /// Offset Y del pezzo [mm,float]
        /// </summary>
        /// <remarks>
        /// </remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Piece)]
        OffsetY = 143,

        /// <summary>
        /// Angolo del pezzo [mm,float]
        /// </summary>
        /// <remarks>
        /// </remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Piece)]
        OffsetAngle = 144,

        /// <summary>
        /// Origine X del programma [mm,float]
        /// </summary>
        /// <remarks>
        /// </remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Program)]
        OriginX = 145,

        /// <summary>
        /// Origine Y del programma [mm,float]
        /// </summary>
        /// <remarks>
        /// </remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Program)]
        OriginY = 146,

        /// <summary>
        /// Angolo del programma [mm,float]
        /// </summary>
        /// <remarks>
        /// </remarks>
        [MacroContext(macroTypeEnum: MacroTypeEnum.MacroCut | MacroTypeEnum.MacroMill
                                , macroContextDataEnum: ExternalInterfaceNameEnum.Program)]
        OriginAngle = 147,

        /// <summary>
        /// Attributo che rappresenta la lavorazione da utilizzare [Enum-> ToolWorkTypeEnum]
        /// </summary>
        /// <remarks></remarks>
        [Description("Attributo che rappresenta la lavorazione da utilizzare")]
        WorkType = 148,

    }
}
