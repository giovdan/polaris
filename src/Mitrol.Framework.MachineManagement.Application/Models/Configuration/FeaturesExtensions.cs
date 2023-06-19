namespace Mitrol.Framework.MachineManagement.Application.Enums
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Configuration;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Features;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class FeaturesExtensions
    {
        public static IEnumerable<KeyValuePair<MachineFeaturesEnum, object>> ToFeatures(this IRootConfiguration configurationRoot)
        {
            // Inizializzo le feature ai valori di default
            var features = Enum.GetValues<MachineFeaturesEnum>()
                .Select(featureEnum => new KeyValuePair<MachineFeaturesEnum, object>(featureEnum, featureEnum.GetEnumAttribute<ContentTypeAttribute>().DefaultValue));

            return features
                .Merge(configurationRoot.Machine.Type.ToFeatures())
                .Merge(configurationRoot.Production.ToFeatures());
        }

        public static IEnumerable<KeyValuePair<MachineFeaturesEnum, object>> ToFeatures(this MachineTypeEnum? machineType)
        {
            if (machineType is null)
            {
                throw new ArgumentNullException(nameof(machineType));
            }

            return new Dictionary<MachineFeaturesEnum, object>
            {
                //
                //DailyProductionWorkingMode
                //
                {
                    MachineFeaturesEnum.DailyProductionWorkingMode,
                    machineType switch
                    {
                        // Queue
                        MachineTypeEnum.TANG_GEMINI or MachineTypeEnum.TANG_PIASTRINA or MachineTypeEnum.TANG_F401P
                            => DailyProductionWorkingModeEnum.Plates,

                        // OneAtTime
                        MachineTypeEnum.TANG_BASE or MachineTypeEnum.TANG_CALIBRO or MachineTypeEnum.TANG_ENDEAVOUR or MachineTypeEnum.TANG_TIPOG
                            => DailyProductionWorkingModeEnum.SequenceAuto,

                        // Sequence
                        MachineTypeEnum.TANG_V11
                            => DailyProductionWorkingModeEnum.SequenceManual,

                        _ => DailyProductionWorkingModeEnum.None
                    }
                },
                //
                //SetupOrigins
                //
                {
                    MachineFeaturesEnum.SetupOrigins,
                    machineType switch
                    {
                        MachineTypeEnum.TANG_GEMINI => true,
                        _ => false
                    }
                }
            };
        }

        public static IEnumerable<KeyValuePair<MachineFeaturesEnum, object>> ToFeatures(this ProductionConfiguration handling)
        {
            return handling is null ? new Dictionary<MachineFeaturesEnum, object>()
                : new Dictionary<MachineFeaturesEnum, object>
                {
                    { MachineFeaturesEnum.AutomaticHandling, handling.IsoStartAuto },
                    { MachineFeaturesEnum.ProgramLoading, handling.LoadPrograms },
                    { MachineFeaturesEnum.ProgramReservation, handling.ReservePrograms }
                };
        }
    }
}
