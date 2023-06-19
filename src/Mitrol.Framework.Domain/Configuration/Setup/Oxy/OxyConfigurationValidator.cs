namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;

    public class OxyConfigurationValidator : AbstractValidator<OxyConfiguration>
    {
        public OxyConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleForEach(unit => unit.Torches)
                .SetValidator(serviceFactory.GetService<TorchUnitConfigurationValidator>())
                .SetValidator(serviceFactory.GetService<UnitConfigurationValidator>());
        }
    }
}