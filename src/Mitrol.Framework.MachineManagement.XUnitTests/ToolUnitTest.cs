using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mitrol.Framework.Domain.Enums;
using Mitrol.Framework.Domain.Interfaces;
using Mitrol.Framework.Domain.Models;
using Mitrol.Framework.MachineManagement.Application.Interfaces;
using Mitrol.Framework.MachineManagement.Application.Models;
using Mitrol.Framework.MachineManagement.Application.Resolvers;
using Mitrol.Framework.MachineManagement.Application.Services;
using Mitrol.Framework.MachineManagement.Application.Validators;
using Mitrol.Framework.MachineManagement.Data.MySQL.Repositories;
using Mitrol.Framework.MachineManagement.Domain.Interfaces;
using System.Globalization;
using System.Linq;
using System.Threading;
using Xunit;
using XUnitTests;

namespace Mitrol.Framework.XUnitTests
{
    [Trait("TestType", "Tool")]
    public class ToolUnitTest: BaseUnitTest
    {
        public ToolUnitTest():base()
        {
            var services = new ServiceCollection();
            services.AddSingleton<DrillStatus>();
            services.AddSingleton<TorchOxyStatus>();
            services.AddSingleton<TorchPlaStatus>();
            services.AddSingleton<SawStatus>();

            services.AddTransient<IResolver<IToolStatus, PlantUnitEnum>, ToolStatusResolver>();
            services.AddScoped<IToolService, ToolService>();
            services.AddScoped<IDetailIdentifierRepository, DetailIdentifierRepository>();
            services.AddScoped<IMachineParameterRepository, MachineParameterRepository>();
            services.AddScoped<IEntityValidator<ToolDetailItem>, ToolValidator>();
            services.AddScoped<IExecutionService, ExecutionService>();
            services.AddScoped<IMachineConfigurationService, MachineConfigurationService>();
            services.AddScoped<IMachineParameterService, MachineParameterService>();

            RegisterServices(services);
        }

        [Fact]
        public void GetAllToolsReturnsSomething()
        {
            using var scope = ServiceProvider.CreateScope();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = scope.ServiceProvider.GetRequiredService<IToolService>();
            service.SetSession(NullUserSession.InternalSessionInstance);
            var entities = service.GetAll().ToList();
            // Checks
            entities.Should().NotBeNullOrEmpty();
            entities.Any(e => e.CodeGenerators.Any()).Should().BeTrue();
            entities.SelectMany(e => e.CodeGenerators).Any(c => string.IsNullOrEmpty(c.DisplayName))
                        .Should().BeFalse();
            entities.SelectMany(e => e.CodeGenerators).Any(c => string.IsNullOrEmpty(c.UMLocalizationKey))
                        .Should().BeFalse();
            entities.Any(e => e.Identifiers.Any()).Should().BeTrue();
            entities.SelectMany(e => e.Identifiers).Any(i => string.IsNullOrEmpty(i.DisplayName))
                        .Should().BeFalse();
        }

        [Fact]
        public void GetToolDetailReturnsSomething()
        {
            using var scope = ServiceProvider.CreateScope();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = scope.ServiceProvider.GetRequiredService<IToolService>();
            service.SetSession(NullUserSession.InternalSessionInstance);
            var tools = service.GetAll();
            tools.Should().NotBeEmpty();
            var toolId = tools.LastOrDefault().InnerId;
            var result = service.Get(toolId);
            result.Success.Should().BeTrue();
            result.Value.InnerId.Should().BeGreaterThan(0);
            result.Value.InnerId.Should().Be(toolId);
            result.Value.CodeGenerators.Should().NotBeEmpty();
            result.Value.Identifiers.Should().NotBeEmpty();
            result.Value.Identifiers.Select(i => i.AttributeKind != Domain.Enums.AttributeKindEnum.Enum
                    ? i.Value.CurrentValue
                    : i.Value.CurrentValueId)
                .Should().NotBeNull();
                        
            result.Value.Attributes.Should().NotBeEmpty();
            // Check DisplayName
            result.Value.Attributes.Any(a => !string.IsNullOrEmpty(a.DisplayName)).Should().BeTrue();
            // Check valore
            result.Value.Attributes.Select(i => i.AttributeKind != Domain.Enums.AttributeKindEnum.Enum
                                ? i.Value.CurrentValue
                                : i.Value.CurrentValueId)
                                .Should().NotBeNull();

        }
    }
}
