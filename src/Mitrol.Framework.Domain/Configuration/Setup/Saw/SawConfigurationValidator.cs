namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;

    public class SawConfigurationValidator : AbstractValidator<SawConfiguration>
    {
        public SawConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleForEach(unit => unit.Units)
                 .SetValidator(serviceFactory.GetService<SawUnitConfigurationValidator>());
        }
    }
}