namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Configuration.Enums;
    using Mitrol.Framework.Domain.Configuration.Interfaces;
    using Mitrol.Framework.Domain.Configuration.Models;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Macro;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Production.Models;
    using Mitrol.Framework.MachineManagement.Application.Enums;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Application.Models.Production.Pieces;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Contract of the machine configuration provider.
    /// </summary>
    public interface IMachineConfigurationService : IApplicationService, IBootableService, IRemoteMachineConfigurationService,IConfigurationService
    {
        Result<string> GetConfigurationFileContent(ApplicationSettingKeyEnum fileDefinitionName);
        IEnumerable<ApplicationSettingItem> GetApplicationSettings();
        MachineNotificationConfiguration GetNotificationOverridesConfiguration(string code);
        IEnumerable<BaseInfoItem<int, string>> GetTableTypeList();
        Dictionary<MachineFeaturesEnum, object> GetFeatures();
        Task<IEnumerable<BaseInfoItem<int, string>>> GetTableTypeListAsync();
        Task<Result<HashSet<MacroInfoItem>>> GetManagedMacrosAsync(MacroConfigurationFilter filter);
        Result<HashSet<AttributeConfigurationItem>> GetMacroAttributes(MacroConfigurationFilter filter);
        Task<IEnumerable<ListBoxItem>> GetFontsConfigurationForListBoxAsync(ToolTypeEnum toolType);
        // specific APIs for translations (can be accessed without authentication)
        IEnumerable<CultureItem> GetCultures();
        IDictionary<string, string> GetTranslations(string culture);
        BEVersionInfo GetBEVersions();
        IEnumerable<ListBoxItem> GetSources(AttributeDefinitionEnum attribute, Dictionary<AttributeDefinitionEnum, object> addtionalItems);
        IEnumerable<InfoItem<string>> GetManagedProgramTypes();
        string GetConfigurationDirectory();
        string GetParametersDirectory();
        Dictionary<MacroTypeEnum, List<MacroCluster>> GetMacrosConfiguration();
        
    }
}
