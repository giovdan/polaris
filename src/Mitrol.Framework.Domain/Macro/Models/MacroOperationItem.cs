using Mitrol.Framework.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mitrol.Framework.Domain.Macro
{
    /// <summary>
    /// Classe base che rappresenta la singola operazione ottenuta durante l'operazione di elaborazione di una macro
    /// </summary>
    public class MacroOperationItem : ExternalBaseData, IMacroOperationItem
    {  
        public MacroOperationItem(MacroOperationTypeEnum type) : base()
        {
            Type = type;
        }

        /// <summary>
        /// Tipo di operazione (enumerativo)
        /// </summary>
        [JsonProperty("Type")] 
        public MacroOperationTypeEnum Type { get; set; }

        /// <summary>
        /// Posizione X dell'operazione da eseguire
        /// </summary>
        [JsonProperty("X")] 
        public float? X { get; set; }

        /// <summary>
        /// Posizione Y dell'operazione da eseguire
        /// </summary>
        [JsonProperty("Y")] 
        public float? Y { get; set; }

        /// <summary>
        /// Rappresenta il lato su cui si deve lavorare [stringa,"DC","DA","DB","DC"]
        /// </summary>
        [JsonProperty("Side")] 
        public string Side { get; set; }     
    }

    /// <summary>
    /// Classe per l'operazione di Lead
    /// </summary>
    public class MacroLeadOperationItem:MacroOperationItem
    {
        public MacroLeadOperationItem():
            base(MacroOperationTypeEnum.Lead)
        {
            TS = ToolTypeEnum.NotDefined;
            // Default lavorazione = Ramping in caso di MacroMill
           WorkType = ToolWorkTypeEnum.Ramping;
        }

        /// <summary>
        /// Profondità del Lead (ex Pegaso: EC)
        /// </summary>
        [JsonProperty("Depth")]
        public float? Depth { get; set; }

        /// <summary>
        /// Tipo di compensazione: = 40 -> nessuna compensazione
        ///                        = 41 -> compensazione sinistra
        ///                        = 42 -> compensazione destra
        /// </summary>
        [JsonProperty("GCompensationType")]
        public int? GCompensationType { get; set; }

        /// <summary>
        /// Override di velocità (ex Pegaso: F)
        /// </summary>
        [JsonProperty("VelocityOverride")] 
        public float? VelocityOverride { get; set; }

        /// <summary>
        /// Tipo di tool da utilizzare
        /// </summary>
        [JsonProperty("TS")] 
        public ToolTypeEnum TS { get; set; }

        /// <summary>
        /// Diametro del Tool da utilizzare
        /// </summary>
        [JsonProperty("DN")] 
        public float DN { get; set; }

        /// <summary>
        /// Codice del Tool da utilizzare
        /// </summary>
        [JsonProperty("COD")] 
        public string COD { get; set; }

        /// <summary>
        /// Lavorazione
        /// </summary>
        [JsonProperty("WorkType")]
        public ToolWorkTypeEnum WorkType { get; set; }

        public override string ToString()
        { 
            string operation=$"LEAD SIDE:{Side} TS:{TS} DN:{DN} COD:{COD} X:{X} Y:{Y} DEPTH:{Depth} WORKTYPE:{WorkType} ";
            if (VelocityOverride.HasValue)
                operation += $"F:{VelocityOverride} ";
            if (GCompensationType.HasValue)
                operation += $"G:{GCompensationType} ";
            foreach (var a in this.Attributes)
            { operation += $"{a.Key.ToString()}:{a.Value.ToString()} "; }
            return operation;
        }
    }

    /// <summary>
    /// Classe per l'operazione di Cut
    /// </summary>
    public class MacroCutOperationItem : MacroOperationItem
    {
        public MacroCutOperationItem() :
            base(MacroOperationTypeEnum.Cut)
        {
        }

        /// <summary>
        /// Raggio del utensile di taglio
        /// </summary>
        [JsonProperty("Radius")]
        public float? Radius { get; set; }

        /// <summary>
        /// Profondità del taglio (ex Pegaso: EC)
        /// </summary>
        [JsonProperty("Depth")] 
        public float? Depth { get; set; }

        /// <summary>
        /// Override di velocità (ex Pegaso: F)
        /// </summary>
        [JsonProperty("VelocityOverride")] 
        public float? VelocityOverride { get; set; }

        /// <summary>
        /// Tipo di interpolazione: 72 per G72/interpolazione continua tra i punti, 73 per G73/interpolazione punto a punto(rampa di salita e discesa tra i punti)
        /// </summary>
        [JsonProperty("GInterpolationType")] 
        public int? GInterpolationType { get; set; }
        public override string ToString()
        {
            string operation = $"CUT SIDE:{Side} X:{X} Y:{Y} ";
            if (Radius.HasValue)
                operation += $"R:{Radius} ";
            if (Depth.HasValue)
                operation += $"DEPTH:{Depth} ";
            if (VelocityOverride.HasValue)
                operation += $"F:{VelocityOverride} ";
            if (GInterpolationType.HasValue)
                operation += $"G:{GInterpolationType} ";

            foreach (var a in this.Attributes)
            { operation += $"{a.Key.ToString()}:{a.Value.ToString()} "; }
            return operation; 
        }
    }

    /// <summary>
    /// Classe per l'operazione di Hol
    /// </summary>
    public class MacroHolOperationItem : MacroOperationItem
    {      
        public MacroHolOperationItem() :
            base(MacroOperationTypeEnum.Hol)
        {
            TS = ToolTypeEnum.NotDefined;
        }

        /// <summary>
        /// Diametro del foro 
        /// </summary>
        [JsonProperty("Diameter")] 
        public float? Diameter { get; set; }

        /// <summary>
        /// Tipo di tool da utilizzare
        /// </summary>
        [JsonProperty("TS")] 
        public ToolTypeEnum TS { get; set; }

        /// <summary>
        /// Diametro del Tool da utilizzare
        /// </summary>
        [JsonProperty("DN")] 
        public float DN { get; set; }

        /// <summary>
        /// Codice del Tool da utilizzare
        /// </summary>
        [JsonProperty("COD")] 
        public string COD { get; set; }
        public override string ToString()
        {
            string operation = $"HOL SIDE:{Side} TS:{TS} DN:{DN} COD:{COD} X:{X} Y:{Y} ";
            if (Diameter.HasValue)
                operation += $"DIA:{Diameter} ";
         
            foreach (var a in this.Attributes)
            { operation += $"{a.Key.ToString()}:{a.Value.ToString()} "; }
            return operation;
        }
    }

    public static class GenerateMacroOperation
    {
        private const string patternCanWithException = @"(?<command>[A-Z]{1,4})\:?(?<value>[\S]*)\s?";
        private static Regex _regexCanException = new Regex(patternCanWithException);
        public const string BLK_FNC_X = "X";
        public const string BLK_FNC_Y = "Y";
        public const string BLK_FNC_R = "R";
        public const string BLK_FNC_G = "G";

        public static MacroOperationItem Generate(String leadCutOperation)
        {
            var matches = _regexCanException.Matches(leadCutOperation)
                   .Cast<Match>()
                  .GroupBy(match => match.Groups[1].Value, match => match.Groups[2].Value)
                  .ToDictionary(mm => mm.Key, mm => mm.ToList());
            
            var isLEAD = matches.TryGetValue("LEAD", out var val);

            var isCUT = matches.TryGetValue("CUT", out val);
            var X = matches.FindProperty<float?>(BLK_FNC_X);
            var Y = matches.FindProperty<float?>(BLK_FNC_Y);
            var R = matches.FindProperty<float?>(BLK_FNC_R);
            var G = matches.FindProperty<int?>(BLK_FNC_G);
           
            if (isLEAD)
            {
                var leadop=  new MacroLeadOperationItem();
                leadop.X = X;
                leadop.Y = Y;
                return leadop;
            }
            else if (isCUT)
            {
                var cutOp = new MacroCutOperationItem();
                cutOp.X = X;
                cutOp.Y = Y;
                cutOp.Radius = R;
                if (G != null) 
                    cutOp.GInterpolationType = G.Value;
                return cutOp;
            }

            return null;
        }
    }
}
