namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Configuration.Enums;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System.Collections.Generic;

    public interface IApplicationSettingRepository : IRepository<ApplicationSetting, IMachineManagentDatabaseContext>
    {
        IEnumerable<ApplicationSetting> GetAll();
        ApplicationSetting Get(ApplicationSettingKeyEnum settingKeyId);
        bool Update(ApplicationSettingKeyEnum keyId, string value);
    };
}
