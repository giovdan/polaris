using Mitrol.Framework.Domain.Configuration.Extensions;

namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using static ConfigurationExtensions;

    public class MachineConfigurationValidator : AbstractValidator<MachineConfiguration>
    {
        public MachineConfigurationValidator()
        {
            RuleFor(machine => machine.Type)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(machine => machine.FixedWire)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(machine => machine.OrderingType)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);
            
            RuleFor(machine => machine.CarriageType)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(machine => machine.ToolTypes)
                .NotEmpty().WithErrorCode(s_errorMissingSetting);

            RuleFor(machine => machine.Profiles)
                .NotEmpty().WithErrorCode(s_errorMissingSetting);
        }
    }
}