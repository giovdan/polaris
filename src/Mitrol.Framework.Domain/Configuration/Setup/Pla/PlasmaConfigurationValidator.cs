namespace Mitrol.Framework.Domain.Configuration.Setup.Pla
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;

    public class PlaConfigurationValidator : AbstractValidator<PlaConfiguration>
    {
        public PlaConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleForEach(unit => unit.Torches)
                .SetValidator(serviceFactory.GetService<TorchUnitConfigurationValidator>())
                .SetValidator(serviceFactory.GetService<UnitConfigurationValidator>());
        }
    }
}
