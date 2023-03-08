namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using static ConfigurationExtensions;

    public class BacklogTimerConfigurationValidator : AbstractValidator<BacklogTimerConfiguration>
    {
        public BacklogTimerConfigurationValidator()
        {
            RuleFor(timer => timer.Index)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .GreaterThan(0).WithErrorCode(s_errorInvalidSetting);

            RuleFor(timer => timer.LocalizationKey)
                .NotEmpty().WithErrorCode(s_errorMissingSetting);
        }
    }
}