using Mitrol.Framework.Domain.Configuration.Extensions;

namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;
    using static ConfigurationExtensions;

    public class RootConfigurationValidator : AbstractValidator<RootConfiguration>
    {
        public RootConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleFor(root => root.Name)
                .NotEmpty().WithErrorCode(s_errorMissingSetting);

            RuleFor(root => root.Machine)
                .NotNull().WithErrorCode(s_errorMissingSection)
                .SetValidator(serviceFactory.GetService<MachineConfigurationValidator>());

            RuleFor(root => root.Programming)
                .NotNull().WithErrorCode(s_errorMissingSection)
                .SetValidator(serviceFactory.GetService<ProgrammingConfigurationValidator>());

            RuleFor(root => root.Cnc)
                .NotNull().WithErrorCode(s_errorMissingSection)
                .SetValidator(serviceFactory.GetService<CncConfigurationValidator>());

            RuleFor(root => root.Plc)
                .NotNull().WithErrorCode(s_errorMissingSection)
                .SetValidator(serviceFactory.GetService<PlcConfigurationValidator>());

            RuleFor(root => root.Setup)
                .NotNull().WithErrorCode(s_errorMissingSection)
                .SetValidator(serviceFactory.GetService<SetupConfigurationValidator>());

            RuleFor(root => root.Plant)
                .NotNull().WithErrorCode(s_errorMissingSection)
                //.SetValidator(serviceFactory.GetService<PlantConfigurationValidator>())
                ;

            RuleFor(root => root.Backlogs)
                .NotNull().WithErrorCode(s_errorMissingSection)
                .SetValidator(serviceFactory.GetService<BacklogsConfigurationValidator>());

            RuleFor(root => root.CleanUp)
                .NotNull().WithErrorCode(s_errorMissingSection)
                .SetValidator(serviceFactory.GetService<CleanUpConfigurationValidator>());
        }
    }
}
