namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;

    public class MarkConfigurationValidator : AbstractValidator<MarkConfiguration>
    {
        public MarkConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleForEach(unit => unit.Units)
                 .SetValidator(serviceFactory.GetService<MarkUnitConfigurationValidator>());
        }
    }
}