using Mitrol.Framework.Domain.Configuration.Extensions;

namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;
    using static ConfigurationExtensions;

    public class BacklogsConfigurationValidator : AbstractValidator<BacklogsConfiguration>
    {
        public BacklogsConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleFor(backlogs => backlogs.TimeRef)
                .GreaterThanOrEqualTo(0).WithErrorCode(s_errorInvalidSetting);

            RuleFor(backlogs => backlogs.MaxRecords)
                .GreaterThan(0).WithErrorCode(s_errorInvalidSetting);

            RuleFor(cnc => cnc.Timers)
                //.NotEmpty().WithErrorCode(s_errorMissingSetting)
                .ForEach(axis => axis.SetValidator(serviceFactory.GetService<BacklogTimerConfigurationValidator>()));
        }
    }
}