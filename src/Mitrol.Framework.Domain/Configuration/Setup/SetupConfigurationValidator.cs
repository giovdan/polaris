namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;

    public class SetupConfigurationValidator : AbstractValidator<SetupConfiguration> {

        public SetupConfigurationValidator(IServiceFactory serviceFactory)
        {
            RuleFor(setup => setup.Oxy)
                .SetValidator(serviceFactory.GetService<OxyConfigurationValidator>());

            RuleFor(setup => setup.Mark)
                .SetValidator(serviceFactory.GetService<MarkConfigurationValidator>());

            RuleFor(setup => setup.Saw)
                .SetValidator(serviceFactory.GetService<SawConfigurationValidator>());
        }
    }
}