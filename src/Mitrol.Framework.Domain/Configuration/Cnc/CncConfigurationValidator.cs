namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;
    using System.Linq;
    using static ConfigurationExtensions;

    public class CncConfigurationValidator : AbstractValidator<CncConfiguration>
    {
        public CncConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleFor(cnc => cnc.Axes)
                .NotEmpty().WithErrorCode(s_errorMissingSetting)
                .Must((cnc, axes, context) =>
                {
                    var duplicates = axes.Duplicates(x => x.Name);

                    if (duplicates.Any())
                    {
                        context.MessageFormatter.AppendArgument("duplicates", string.Join(",", duplicates));
                        return false;
                    }
                    return true;
                }).WithMessage(s_errorDuplicates)
                .ForEach(axis => axis.SetValidator(serviceFactory.GetService<AxisConfigurationValidator>()));

            RuleFor(cnc => cnc.Mitrol)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .SetValidator(serviceFactory.GetService<NetworkDeviceConfigurationValidator>());

            RuleFor(cnc => cnc.Fanuc)
                .Custom((fanuc, context) => context.ParentContext.RootContextData.Add("Fanuc.IsPresent", fanuc?.IsPresent ?? false ))
                .SetValidator(serviceFactory.GetService<NetworkDeviceConfigurationValidator>())
                .When(cnc => cnc.Fanuc != null);

            RuleFor(cnc => cnc.InOutCycleTime)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(cnc => cnc.FastCycleTime)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(cnc => cnc.EtherCAT)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .SetValidator(serviceFactory.GetService<EtherCatConfigurationValidator>());

            RuleFor(cnc => cnc.AxesOverride)
               .SetValidator(serviceFactory.GetService<OverrideConfigurationValidator>())
               .When(cnc => cnc.AxesOverride != null);

            RuleFor(cnc => cnc.SpindlesOverride)
               .SetValidator(serviceFactory.GetService<OverrideConfigurationValidator>())
               .When(cnc => cnc.SpindlesOverride != null);
        }
    }
}