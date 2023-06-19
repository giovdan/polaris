namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Text;

    public static class MachineNotificationExtensions
    {
        public static MachineNotification ApplyNotificationOverrides(this MachineNotification machineNotification, IServiceFactory serviceFactory)
        {
            // Per ogni stringa di additional info collegata
            var notificationOverrides = serviceFactory.GetService<IMachineConfigurationService>()
                .GetNotificationOverridesConfiguration(machineNotification.NotificationCode);

            if (notificationOverrides is not null)
            {
                var localizationKeyOverride = notificationOverrides.LocalizationKey is not null
                    ? new StringBuilder(notificationOverrides.LocalizationKey)
                    : null;
                var descriptionLocalizationKeyOverride = notificationOverrides.DescriptionLocalizationKey is not null
                    ? new StringBuilder(notificationOverrides.DescriptionLocalizationKey)
                    : null;

                // TO DO
                //if (notificationOverrides.CausesAndSolutions is not null) {

                //    machineNotification.CausesAndSolutions = notificationOverrides.CausesAndSolutions;
                //}

                if (notificationOverrides.Arguments is not null)
                {
                    // Per ogni argomento richiesto presente negli override
                    foreach (var argument in notificationOverrides.Arguments)
                    {
                        // Se un resolver è registrato per l'argomento specificato
                        if (TryResolve(argument, out var invokableResolver))
                        {
                            // Invocazione del resolver per ottenere il valore da comunicare insieme
                            // alla notifica
                            var value = invokableResolver(serviceFactory, machineNotification);

                            // Il valore ottenuto viene conservato nella notifica stessa per poter
                            // mantenere i valori al momento della sua generazione
                            machineNotification.Arguments.Add(argument, value);

                            // Vengono applicate le eventuali sostituzioni anche alle chiavi di
                            // localizzazione
                            if (localizationKeyOverride is not null) { localizationKeyOverride = localizationKeyOverride.Replace($"%{argument}%", value); }
                            if (descriptionLocalizationKeyOverride is not null) { descriptionLocalizationKeyOverride = descriptionLocalizationKeyOverride.Replace($"%{argument}%", value); }
                        }
                    }
                }

                // Rimpiazzo le chiavi di localizzazione se un override è stato configurato
                if (localizationKeyOverride is not null) { machineNotification.LocalizationKey = localizationKeyOverride.ToString(); }
                if (descriptionLocalizationKeyOverride is not null) { machineNotification.DescriptionLocalizationKey = descriptionLocalizationKeyOverride.ToString(); }

                machineNotification.Source = notificationOverrides.Source;
                machineNotification.Severity = notificationOverrides.Severity;
            }

            return machineNotification;
        }

        public static bool TryResolve(string argument, out Func<IServiceFactory, MachineNotification, string> resolver)
        {
            _ = Enum.TryParse<NotificationArgumentsEnum>(argument, out var argumentsEnum);
            resolver = argumentsEnum switch
            {
                NotificationArgumentsEnum.NotificationParameter
                    => (_, notification) => notification.Parameter.ToString(),
                NotificationArgumentsEnum.AxisNameByIndex
                    => (serviceFactory, notification) => serviceFactory.GetService<IMachineConfigurationService>().ConfigurationRoot.Cnc.Axes
                                          .SingleWhenOnlyOrDefault(axisConfig => axisConfig.Index == notification.Parameter)?.Name ?? string.Empty,
                NotificationArgumentsEnum.NodeInfoByAxisIndex
                    => (serviceFactory, notification) => ResolveMitComMacro(serviceFactory, notification, "AN"/* NotificationArgumentsEnum.NodeInfoByAxisIndex.ToString()*/),
                NotificationArgumentsEnum.EtherCatNodeNameById
                    => (serviceFactory, notification) => ResolveMitComMacro(serviceFactory, notification, "NE"/*NotificationArgumentsEnum.EtherCatNodeNameById.ToString()*/),
                NotificationArgumentsEnum.CanBusNodeNameById
                    => (serviceFactory, notification) => ResolveMitComMacro(serviceFactory, notification, "NP"/*NotificationArgumentsEnum.CanBusNodeNameById.ToString()*/),
                _
                    => (serviceFactory, notification) => ResolveMitComMacro(serviceFactory, notification, argument)
            };
            return resolver is not null;
        }

        private static string ResolveMitComMacro(IServiceFactory serviceProvider, MachineNotification notification, string macroPattern)
        {
            // TO DO
            //var result = serviceProvider.Resolve<IMitCom>().SendReadMacroCommand(READ_COMMAND.MC_RD_MACRO_COMMAND,
            //                        (ushort)notification.Number, (ushort)notification.Parameter, macroPattern);
            //return result.Success ? string.Join(Environment.NewLine, result.Value) : string.Empty;
            return string.Empty;
        }
    }

    public class MachineNotificationConfiguration
    {
        public MachineNotificationConfiguration()
        {
            Arguments = Array.Empty<string>();
            CausesAndSolutions = Array.Empty<CauseSolutionPair>();
            Source = NotificationSourceEnum.Machine;
            Severity = 0;
        }

        [JsonProperty("Arguments")]
        public string[] Arguments { get; set; }

        [JsonProperty("DescriptionLocalizationKey")]
        public string DescriptionLocalizationKey { get; set; }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }

        [JsonProperty("CausesAndSolutions")]
        public CauseSolutionPair[] CausesAndSolutions { get; internal set; }

        [JsonProperty("Owner")]
        public NotificationSourceEnum Source { get; set; }

        [JsonProperty("Severity")]
        public int Severity { get; set; }
    }

}
