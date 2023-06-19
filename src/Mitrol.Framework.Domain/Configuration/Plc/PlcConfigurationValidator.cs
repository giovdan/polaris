using Mitrol.Framework.Domain.Configuration.Extensions;

namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Enums;
    using System.IO;
    using static ConfigurationExtensions;

    public class PlcConfigurationValidator : AbstractValidator<PlcConfiguration>
    {
        public PlcConfigurationValidator()
        {
            RuleFor(plc => plc.CompilerPath)
                .NotEmpty().WithErrorCode(s_errorMissingSetting)
                .Must(path => File.Exists(path)).WithErrorCode(s_errorFileNotFoundOrInvalidPath)
                .When(plc => plc.Platform is PlatformEnum.RealDevice);

            RuleFor(plc => plc.ApplicationPath)
                .NotEmpty().WithErrorCode(s_errorMissingSetting)
                .Must(path => File.Exists(path)).WithErrorCode(s_errorFileNotFoundOrInvalidPath)
                .When(plc => plc.Platform is PlatformEnum.RealDevice);

            RuleFor(plc => plc.ProjectName)
                .NotEmpty().WithErrorCode(s_errorMissingSetting)
                .When(plc => plc.Platform is PlatformEnum.RealDevice);
        }
    }
}