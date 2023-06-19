using Mitrol.Framework.Domain.Configuration.Extensions;

namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Enums;
    using static ConfigurationExtensions;

    public class MarkUnitConfigurationValidator : AbstractValidator<MarkUnitConfiguration>
    {
        public MarkUnitConfigurationValidator()
        {
            RuleFor(unit => unit.Reverse)
                 .NotNull().WithErrorCode(s_errorMissingSetting)
                 .When(unit => unit.Type == MarkingUnitConfigurationEnum.Disk36
                             || unit.Type == MarkingUnitConfigurationEnum.Disk40);

            RuleFor(unit => unit.CharacterPositionOnDisk)
                 .NotEmpty().WithErrorCode(s_errorMissingSetting)
                 .When(unit => unit.Type == MarkingUnitConfigurationEnum.Disk36
                             || unit.Type == MarkingUnitConfigurationEnum.Disk40);
                 
        }
    }
}