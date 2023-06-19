using Mitrol.Framework.Domain.Configuration.Extensions;

namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;
    using static ConfigurationExtensions;

    public class UnitConfigurationValidator : AbstractValidator<UnitConfiguration>
    {
        public UnitConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleFor(unit => unit.Unit)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(unit => unit.GridPosition)
                .NotNull().WithMessage(unit => $"{s_errorMissingSetting} for unit {unit.Unit}")
                .When(unit => unit.IsPresent);
        }
    }
}
