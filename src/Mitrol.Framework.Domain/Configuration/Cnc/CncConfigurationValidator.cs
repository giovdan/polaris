using Mitrol.Framework.Domain.Configuration.Extensions;
namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Interfaces;
    using System.Linq;
    using static ConfigurationExtensions;

    public class CncConfigurationValidator : AbstractValidator<CncConfiguration>
    {
        public CncConfigurationValidator(IServiceFactory serviceFactory)
        {
            
        }
    }
}