namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using static ConfigurationExtensions;

    public class EtherCatConfigurationValidator : AbstractValidator<EtherCATConfiguration>
    {
        public EtherCatConfigurationValidator()
        {
            RuleFor(etherCAT => etherCAT.Nodes)
                .NotNull().WithErrorCode(s_errorMissingSetting);
        }
    }
}