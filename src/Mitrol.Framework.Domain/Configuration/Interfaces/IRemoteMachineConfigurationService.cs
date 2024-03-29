﻿namespace Mitrol.Framework.Domain.Configuration.Interfaces
{
    using Mitrol.Framework.Domain.Configuration.Enums;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System;

    public interface IRemoteMachineConfigurationService
    {
        IRootConfiguration ConfigurationRoot { get; }

        //string GetApplicationSettingValue(ApplicationSettingKeyEnum applicationSettingKey);
        
        //bool GetMacroEnabled(MacroConfigurationFilter macroConfigurationFilter);

        //FontConfiguration GetFontConfiguration(FontConfigurationFilter filter);

        void UpdateApplicationSettingValue(ApplicationSettingKeyEnum applicationSettingKey, string defaultValue);

    }
}
