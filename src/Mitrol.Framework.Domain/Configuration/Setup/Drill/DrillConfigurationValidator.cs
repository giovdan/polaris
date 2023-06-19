namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;

    public class DrillConfigurationValidator : AbstractValidator<DrillConfiguration>
    {
        public DrillConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleForEach(unit => unit.Units)
                .SetValidator(serviceFactory.GetService<UnitConfigurationValidator>());
        }
    }
}
