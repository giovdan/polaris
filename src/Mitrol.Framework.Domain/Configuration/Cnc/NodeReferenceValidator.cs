namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;
    using static ConfigurationExtensions;

    public class NodeReferenceValidator : AbstractValidator<NodeReference>
    {
        public NodeReferenceValidator(IServiceFactory serviceFactory)
        {
            RuleFor(node => node.Bus)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .IsInEnum().WithErrorCode(s_errorInvalidSetting);

            RuleFor(node => node.Id)
                .NotNull().WithErrorCode(s_errorMissingSetting)
                .GreaterThan(0).WithErrorCode(s_errorInvalidSetting);
        }
    }
}