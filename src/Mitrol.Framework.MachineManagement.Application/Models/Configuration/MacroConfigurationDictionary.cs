namespace Mitrol.Framework.MachineManagement.Application.Models.Configuration
{
    using Mitrol.Framework.Domain.Macro;
    using Mitrol.Framework.MachineManagement.Application.Models.Production.Pieces;
    using System.Collections.Generic;

    public class MacroConfigurationDictionary
    {
        private Dictionary<MacroTypeEnum, List<MacroCluster>> ConfigurationDictionary;

        private Dictionary<MacroTypeEnum, List<MacroAttributeStructure>> StructureDictionary;

        public List<MacroCluster> Get(MacroTypeEnum type) => ConfigurationDictionary.GetValueOrDefault(type);
        public List<MacroAttributeStructure> GetStructure(MacroTypeEnum type) => StructureDictionary.GetValueOrDefault(type);

        public void AddOrUpdate(MacroTypeEnum macroType, List<MacroCluster> groups)
        {
            if (ConfigurationDictionary.ContainsKey(macroType))
                ConfigurationDictionary.Remove(macroType);
            ConfigurationDictionary.Add(macroType,groups);
        }

        public void AddOrUpdate(MacroTypeEnum macroType, List<MacroAttributeStructure> attributes)
        {
            if (StructureDictionary.ContainsKey(macroType))
                StructureDictionary.Remove(macroType);
            StructureDictionary.Add(macroType, attributes);
        }

        public static MacroConfigurationDictionary Instance { get; } = new MacroConfigurationDictionary()
        {
            ConfigurationDictionary = new Dictionary<MacroTypeEnum, List<MacroCluster>>(),
            StructureDictionary= new Dictionary<MacroTypeEnum, List<MacroAttributeStructure>>()
        };
    }
}
