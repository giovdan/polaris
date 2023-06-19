namespace Mitrol.Framework.MachineManagement.Application.Models.Configuration
{
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class FontsConfiguration
    {
        [JsonProperty("MacroSets")]
        public Dictionary<string, Dictionary<string, string>> MacroSets { get; set; }

        [JsonProperty("Fonts")]
        private Dictionary<string, FontConfiguration> _fonts;

        public Dictionary<string,FontConfiguration> GetFonts()
        {
            foreach(var font in _fonts)
            {
                //font.Value.macroAlphabet = new Dictionary<string, string>();
                font.Value.Macros = new Dictionary<string,FontMacros>();
                if ((font.Value.CharactersSet!=null)&&(MacroSets.TryGetValue(font.Value.CharactersSet,out var alphabet)))
                {
                    //font.Value.macroAlphabet.AddRange(alphabet);
                    foreach(var character in alphabet)
                    {
                        var fontMacro = new FontMacros();
                        fontMacro.FileName = character.Value;
                        font.Value.Macros.Add(character.Key,fontMacro);
                    }
                }
            }
            return _fonts;
        }

    }
}
