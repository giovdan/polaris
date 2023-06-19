using Mitrol.Framework.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mitrol.Framework.Domain.Enums
{
    public enum LineTypeEnum
    {
        /// <summary>
        /// Non definito
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Inizio programma
        /// </summary>
        StartProgram,

        /// <summary>
        /// Setup
        /// </summary>
        Setup,

        /// <summary>
        /// Controllo setup (M36)
        /// </summary>
        CheckSetup,

        /// <summary>
        /// Funzioni speciali (es. M31, M32)
        /// </summary>
        ProgramFunctionM,

        /// <summary>
        /// Macro funzioni di programma (es. generate dinamicamente dall'esecuzione come posizionamenti, esclusioni, etc.)
        /// </summary>
        MacroFunction,

        /// <summary>
        /// Operazione singola con testa di foratura: foratura passante
        /// </summary>
        [IsForGraphics(true)]
        DrillOperationHOL,

        /// <summary>
        /// Operazione singola con testa di foratura: bulinatura
        /// </summary>
        [IsForGraphics(true)]
        DrillOperationPOINT,

        /// <summary>
        /// Operazione singola con testa di foratura: maschiatura
        /// </summary>
        /// 
        [IsForGraphics(true)]
        DrillOperationTAP,

        /// <summary>
        /// Operazione singola con testa di foratura: sbavatura
        /// </summary>
        /// 
        [IsForGraphics(true)]
        DrillOperationDEBURR,

        /// <summary>
        /// Operazione singola con testa di foratura: lamatura
        /// </summary>
        /// 
        [IsForGraphics(true)]
        DrillOperationBORE,

        /// <summary>
        /// Operazione singola con testa di foratura: svasatura
        /// </summary>
        /// 
        [IsForGraphics(true)]
        DrillOperationFLARE,

        /// <summary>
        /// Operazione singola con testa di foratura: foratura con svasatura
        /// </summary>
        /// 
        [IsForGraphics(true)]
        DrillOperationFHOL,

        /// <summary>
        /// Operazione singola con testa di foratura: foratura con sbavatura
        /// </summary>
        /// 
        [IsForGraphics(true)]
        DrillOperationDHOL,

        /// <summary>
        /// Operazione di marcatura (cassetti o scrittura)
        /// </summary>
        [IsForGraphics(true)]
        Marking,

        /// <summary>
        /// Palpatura delle operazioni di foratura/fresatura (dispositivo meccanico o utensile speciale)
        /// </summary>
        DrillProbe,

        /// <summary>
        /// Palpatura delle operazioni di taglio plasma
        /// </summary>
        ProbePathCut,

        /// <summary>
        /// Sequenza di taglio (plasma od ossitaglio): attacco
        /// </summary>
        /// 
        [IsForGraphics(true)]
        StartPathCut,
        /// <summary>
        /// Sequenza di taglio (plasma od ossitaglio): taglio
        /// </summary>
        /// 
        [IsForGraphics(true)]
        MovePathCut,
        /// <summary>
        /// Sequenza di taglio (plasma od ossitaglio): fine taglio
        /// </summary>
        EndPathCut,

        /// <summary>
        /// Sequenza di fresatura: attacco
        /// </summary>
        /// 
        [IsForGraphics(true)]
        StartPathFre,
        /// <summary>
        /// Sequenza di fresatura: attacco intermedio
        /// </summary>
        /// 
        [IsForGraphics(true)]
        StartIPathFre,
        /// <summary>
        /// Sequenza di fresatura: fresatura
        /// </summary>
        /// 
        [IsForGraphics(true)]
        MovePathFre,
        /// <summary>
        /// Sequenza di fresatura: fine
        /// </summary>
        EndPathFre,
        /// <summary>
        /// Sequenza di fresatura: stop intermedio
        /// </summary>
        EndIPathFre,

        /// <summary>
        /// Sequenza di scribing: attacco
        /// </summary>
        /// 
        [IsForGraphics(true)]
        StartPathScr,
        /// <summary>
        /// Sequenza di scribing: attacco intermedio
        /// </summary>
        /// 
        [IsForGraphics(true)]
        StartIPathScr,
        /// <summary>
        /// Sequenza di scribing: scribing
        /// </summary>
        /// 
        [IsForGraphics(true)]
        MovePathScr,
        /// <summary>
        /// Sequenza di scribing: fine
        /// </summary>
        EndPathScr,
        /// <summary>
        /// Sequenza di scribing: stop intermedio (risalita tra un carattere e l'altro)
        /// </summary>
        EndIPathScr,

        /// <summary>
        /// Sequenza di tracciatura: attacco
        /// </summary>
        /// 
        [IsForGraphics(true)]
        StartPathTracciatura,
        /// <summary>
        /// Sequenza di tracciatura: taglio
        /// </summary>
        /// 
        [IsForGraphics(true)]
        MovePathTracciatura,
        /// <summary>
        /// Sequenza di tracciatura: fine
        /// </summary>
        EndPathTracciatura,

        /// <summary>
        /// Commento per l'operatore
        /// </summary>
        Comment,

        /// <summary>
        /// Emissione messaggio
        /// </summary>
        Message,

        /// <summary>
        /// Decremento pezzo
        /// </summary>
        DecPiece,

        /// <summary>
        /// Decremento programma
        /// </summary>
        DecProgram,

        /// <summary>
        /// Fine programma
        /// </summary>
        EndProgram,

        /// <summary>
        /// Punzonatura
        /// </summary>
        /// 
        [IsForGraphics(true)]
        Punching,

        /// <summary>
        /// Operazione singola con testa di foratura: foratura cieca
        /// </summary>
        [IsForGraphics(true)]
        DrillOperationBHOL,

        /// <summary>
        /// Linea dichiarazione QT da eseguire e pubblicazione di info di esecuzione
        /// </summary>
        QtProgram,

        /// <summary>
        /// Linea di funzione T
        /// </summary>
        ProgramFunctionT,

        /// <summary>
        /// Linea di taglio di separazione materiale
        /// </summary>
        SeparationCut,

        /// <summary>
        /// Operazione di marcatura a getto d'inchiostro
        /// </summary>
        [IsForGraphics(true)]
        MarkingInkJet,
    }

    public static class LineTypeEnumExtensions
    {
        /// <summary>
        /// Get Line Types for graphics
        /// </summary>
        /// <returns></returns>
        public static List<LineTypeEnum> GetLineTypesForGraphics()
        {
            var lineTypesForGraphics = new List<LineTypeEnum>();
            //Recupera le linee che sono gestite dalla grafica
            var lineEnums = Enum.GetValues(typeof(LineTypeEnum)).Cast<LineTypeEnum>();
            foreach (var lineEnum in lineEnums)
            {
                var isForGraphics = lineEnum.GetEnumAttribute<IsForGraphicsAttribute>();
                if (isForGraphics?.IsForGraphics ?? false)
                {
                    lineTypesForGraphics.Add(lineEnum);
                }
            }

            return lineTypesForGraphics;
        }

        /// <summary>
        /// Get related OperationType from Iso Code Line Type
        /// </summary>
        /// <param name="lineType"></param>
        /// <returns></returns>
        public static OperationTypeEnum GetOperationType(this LineTypeEnum lineType)
        {
            var operationType = OperationTypeEnum.Undefined;
            switch (lineType)
            {
                case LineTypeEnum.DrillOperationBHOL:
                    operationType = OperationTypeEnum.BHol;
                    break;
                case LineTypeEnum.DrillOperationHOL:
                    operationType = OperationTypeEnum.Hol;
                    break;
                case LineTypeEnum.DrillOperationPOINT:
                    operationType = OperationTypeEnum.Point;
                    break;
                case LineTypeEnum.DrillOperationTAP:
                    operationType = OperationTypeEnum.Tap;
                    break;
                case LineTypeEnum.DrillOperationDEBURR:
                    operationType = OperationTypeEnum.Deburr;
                    break;
                case LineTypeEnum.DrillOperationBORE:
                    operationType = OperationTypeEnum.Bore;
                    break;
                case LineTypeEnum.DrillOperationFLARE:
                    operationType = OperationTypeEnum.Flare;
                    break;
                case LineTypeEnum.DrillOperationFHOL:
                    operationType = OperationTypeEnum.FHol;
                    break;
                case LineTypeEnum.DrillOperationDHOL:
                    operationType = OperationTypeEnum.DHol;
                    break;
                case LineTypeEnum.Marking:
                case LineTypeEnum.MarkingInkJet:
                    operationType = OperationTypeEnum.Mark;
                    break;
                case LineTypeEnum.StartPathCut:
                    operationType = OperationTypeEnum.PathC;
                    break;
                case LineTypeEnum.StartPathScr:
                case LineTypeEnum.StartPathTracciatura:
                    operationType = OperationTypeEnum.PathS;
                    break;
                case LineTypeEnum.StartPathFre:
                    operationType = OperationTypeEnum.PathM;
                    break;
                case LineTypeEnum.MovePathCut:
                case LineTypeEnum.MovePathScr:
                case LineTypeEnum.MovePathFre:
                case LineTypeEnum.MovePathTracciatura:
                case LineTypeEnum.StartIPathFre:
                case LineTypeEnum.StartIPathScr:
                    operationType = OperationTypeEnum.PathVertex;
                    break;
            }

            return operationType;
        }
    }
}
