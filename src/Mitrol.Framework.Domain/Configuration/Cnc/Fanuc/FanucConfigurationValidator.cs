using Mitrol.Framework.Domain.Configuration.Extensions;

namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Enums;
    using static ConfigurationExtensions;

    public class FanucConfigurationValidator : AbstractValidator<FanucConfiguration>
    {
        public FanucConfigurationValidator()
        {
            RuleFor(config => config.IpAddress)
                .NotEmpty().When(config => config.Platform is null || config.Platform is PlatformEnum.RealDevice)
                .WithErrorCode(s_errorInvalidSetting);

            // When G270 true => G270Max > 0

            RuleFor(config => config.G270MAX)
                .NotEmpty().WithErrorCode(s_errorMissingSetting)
                .GreaterThan(0).WithErrorCode(s_errorInvalidSetting)
                .When(config => config.GFunctions.TryGetValue(GFunctionEnum.G270, out var enable) && enable is true);
        }
    }
}
