namespace Mitrol.Framework.MachineManagement.Application.Models.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Dizionario delle configurazioni dei font di Marking, catalogato in base all'unità di marcatura e alla tipologia di scribing che deve essere utilizzato 
    /// </summary>
    public class FontsConfigurationMarkingUnitDictionary
    {
        /// <summary>
        /// La chiave del dizionario è composta da Tipologia unità di marcatura, tipologia di scribing da utilizzare e nome del font.
        /// </summary>
        private Dictionary<(MarkingUnitTypeEnum, ScribingMarkingTypeEnum, string), FontConfiguration> configurationDictionary;

        public FontConfiguration Get(MarkingUnitTypeEnum type, ScribingMarkingTypeEnum scribingunit, string fontName) => configurationDictionary.GetValueOrDefault((type,scribingunit, fontName));

        public void AddOrUpdate(MarkingUnitTypeEnum type, ScribingMarkingTypeEnum scribingunit, string fontName, FontConfiguration font)
        {
            if (configurationDictionary.ContainsKey((type, scribingunit, fontName)))
            {
                configurationDictionary.Remove((type, scribingunit, fontName));
            }
            configurationDictionary.Add((type, scribingunit, fontName), font);
        }

        public static FontsConfigurationMarkingUnitDictionary Instance { get; } = new FontsConfigurationMarkingUnitDictionary()
        {
            configurationDictionary = new Dictionary<(MarkingUnitTypeEnum, ScribingMarkingTypeEnum, string), FontConfiguration>()
        };
    }
}
