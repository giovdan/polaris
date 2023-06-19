using Mitrol.Framework.Domain.Configuration.Extensions;

namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;
    using System.Linq;
    using static ConfigurationExtensions;

    public class OverrideConfigurationValidator : AbstractValidator<OverrideConfiguration>
    {
        public OverrideConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleFor(config => config.Steps)
                .Must(steps => steps.Count() >= 2).WithMessage(s_errorInvalidSetting)
                .When(config => config.Steps != null);
        }
    }
}