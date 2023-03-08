namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using static ConfigurationExtensions;

    public class AxisConfigurationValidator : AbstractValidator<AxisConfiguration>
    {
        public AxisConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleFor(axis => axis.Name)
                .NotEmpty().WithErrorCode(s_errorMissingSetting);

            RuleFor(axis => axis.Category)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(axis => axis.Type)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(axis => axis.Index)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .GreaterThan((byte)0).WithErrorCode(s_errorMissingSetting);
            
            RuleFor(axis => axis.Path)
                .GreaterThan((byte)0).WithMessage("Must be greater than zero for Fanuc cnc machines")
                    .When(axis => axis.Category is AxisCategoryEnum.FANUC || axis.Category is AxisCategoryEnum.FANUC_ROB);

            RuleFor(axis => axis.Node)
                .SetValidator(serviceFactory.GetService<NodeReferenceValidator>());
        }
    }
}