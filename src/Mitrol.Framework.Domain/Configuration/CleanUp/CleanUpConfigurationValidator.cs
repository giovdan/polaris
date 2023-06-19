using Mitrol.Framework.Domain.Configuration.Extensions;

namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using static ConfigurationExtensions;

    public class CleanUpConfigurationValidator : AbstractValidator<CleanUpConfiguration>
    {
        public CleanUpConfigurationValidator()
        {
            RuleFor(cleanUp => cleanUp.MaintenanceLimit)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .GreaterThan(0).WithErrorCode(s_errorInvalidSetting);

            RuleFor(cleanUp => cleanUp.PiecesLimit)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .GreaterThan(0).WithErrorCode(s_errorInvalidSetting);

            RuleFor(cleanUp => cleanUp.ProgramsLimit)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .GreaterThan(0).WithErrorCode(s_errorInvalidSetting);

            RuleFor(cleanUp => cleanUp.StocksLimit)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .GreaterThan(0).WithErrorCode(s_errorInvalidSetting);
        }
    }
}