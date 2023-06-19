namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Macro;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FontMacros
    {
        public FontMacros()
        {
            Operations = "";
        }

        /// <summary>
        /// Lista delle operazioni per realizzare il carattere scelto: viene caricato quando richiesto in base al nome e al path della macro da utilizzare
        /// Viene usata la stringa, cioè la versione serializzata della lista di operazioni, perchè non passa la lista delle interfacce non è possibile serializzarla
        /// </summary>
        [JsonProperty("Operations")]
        public string Operations { get; set; }

        /// <summary>
        /// Nome del file della macro da utilizzare
        /// </summary>
        [JsonProperty("FileName")]
        public string FileName { get; set; }
    }
    
    public class FontConfiguration
    {
        public FontConfiguration()
        {
            Macros = new Dictionary<string, FontMacros>();
        }

        /// <summary>
        /// Percorso dove trovare le Macro per il font corrente (è legato al tipo di scribing)
        /// </summary>
        [JsonProperty("ScribingPath")]
        public string ScribingPath { get; set; }

        /// <summary>
        /// Indice del font: da utilizzare per il salvataggio nel DB  
        /// </summary>
        [JsonProperty("Index")]
        public int Index { get; set; }

        /// <summary>
        /// Dizionario contenente le macro (nome macro e lista operazioni) per i caratteri presenti nel font corrente.
        /// </summary>
        [JsonProperty("Macros")]
        public Dictionary<string, FontMacros> Macros { get; set; }

        /// <summary>
        /// Nome del set di caratteri da utilizzare per risolvere l'associazione carattere-macro
        /// </summary>
        [JsonProperty("CharactersSet")]
        public string CharactersSet { get; set; }

        /// <summary>
        /// Caratteristiche geometriche del font
        /// </summary>
        [JsonProperty("Size")]
        public FontSize Size { get; set; }

        /// <summary>
        /// Restituisce la lista delle operazioni per il carattere passato (esegue il caricamento da file se non è già presente) 
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        public List<IMacroOperationItem> GetMacros(string characters)
        {
            // verifico che ci sia il carattere nell'alfabeto
            if (Macros.TryGetValue(characters, out var fontmacro))
            {
                // verifico che non sia già stato caricato, nel caso restituisco subito il valore
                if (!string.IsNullOrEmpty(fontmacro.Operations))
                {
                    return (JsonConvert.DeserializeObject<List<IMacroOperationItem>>(fontmacro.Operations, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    }));
                }
                // Se il file relativo alla Macro
                if ((ScribingPath != null) && (File.Exists($"{ScribingPath}\\{fontmacro.FileName}")))
                {
                    // Leggo la configurazione dal file
                    var lines = File.ReadAllLines($"{ScribingPath}\\{fontmacro.FileName}").ToList<string>();
                    //recupero le operazioni per la marcatura del carattere specifico 
                    var Operations = lines.Select(line => GenerateMacroOperation.Generate(line))
                         .Where(operation => operation != null)
                         .ToList<IMacroOperationItem>();

                    // serializzo per poterlo tenere in memoria..
                    fontmacro.Operations = JsonConvert.SerializeObject(Operations, Formatting.Indented, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                     
                    return Operations;
                }
            }
            return null;
        }


    }

    public class FontSize
    {
        /// <summary>
        /// Scala del font
        /// </summary>
        [JsonProperty("Scale")]
        public float? Scale { get; set; }

        /// <summary>
        /// Larghezza del carattere
        /// </summary>
        [JsonProperty("Width")]
        public float Width{ get; set; }

        /// <summary>
        /// Altezza del carattere
        /// </summary>
        [JsonProperty("Height")]
        public float Height { get; set; }
    }
}
