namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;
    using static ConfigurationExtensions;

    public class SawUnitConfigurationValidator : AbstractValidator<SawUnitConfiguration>
    {
        public SawUnitConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleFor(unit => unit.Platform)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(unit => unit.Unit)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(unit => unit.Type)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(unit => unit.BladeType)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(unit => unit.BladeForwardSpeedType)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(unit => unit.BladeReferenceOrigin)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);
        }
    }
}