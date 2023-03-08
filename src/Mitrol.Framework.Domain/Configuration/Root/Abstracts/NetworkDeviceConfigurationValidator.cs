namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Enums;
    using static ConfigurationExtensions;

    public class NetworkDeviceConfigurationValidator : AbstractValidator<NetworkDeviceConfiguration>
    {
        public NetworkDeviceConfigurationValidator()
        {
            RuleFor(config => config.IpAddress)
                .NotEmpty().When(config => config.Platform is null || config.Platform is PlatformEnum.RealDevice)
                .WithErrorCode(s_errorInvalidSetting);
        }
    }
}