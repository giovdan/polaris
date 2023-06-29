namespace Mitrol.Framework.XUnitTests
{
    using FluentAssertions;
    using global::XUnitTests;
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Application.Resolvers;
    using Mitrol.Framework.MachineManagement.Application.RulesHandlers;
    using Mitrol.Framework.MachineManagement.Application.Services;
    using Mitrol.Framework.MachineManagement.Application.Validators;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Repositories;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Xunit;

    [Trait("TestType", "Tool")]
    public class ToolUnitTest: BaseUnitTest
    {
        private Result Boot(IServiceScope scope)
        {
            var machineConfigurationService = scope.ServiceProvider.GetRequiredService<IMachineConfigurationService>();
            machineConfigurationService.SetSession(NullUserSession.InternalSessionInstance);
            return machineConfigurationService.Boot(NullUserSession.InternalSessionInstance);
        }

        public ToolUnitTest():base()
        {
            var services = new ServiceCollection();
            services.AddSingleton<DrillStatus>();
            services.AddSingleton<TorchOxyStatus>();
            services.AddSingleton<TorchPlaStatus>();
            services.AddSingleton<SawStatus>();

            services.AddScoped<IResolver<IBootableService>, ServiceForward<IBootableService, IMachineConfigurationService>>();

            services.AddTransient<IResolver<IToolStatus, PlantUnitEnum>, ToolStatusResolver>();
            services.AddScoped<IToolService, ToolService>();
            services.AddScoped<IDetailIdentifierRepository, DetailIdentifierRepository>();
            services.AddScoped<IMachineParameterRepository, MachineParameterRepository>();
            services.AddScoped<IEntityValidator<ToolDetailItem>, ToolValidator>();
            services.AddScoped<IExecutionService, ExecutionService>();
            services.AddScoped<IMachineConfigurationService, MachineConfigurationService>();
            services.AddScoped<IMachineParameterService, MachineParameterService>();
            services.AddSingleton<IEntityHandlerFactory, EntityHandlerFactory>();
            services.AddScoped<IResolver<IEntityRulesHandler<ToolDetailItem>>
                            , ToolRulesHandlerResolver>();
            services.AddTransient<ToolRulesHandler>();
            RegisterServices(services);

            // Boot
            using var scope = ServiceProvider.CreateScope();
            Boot(scope)
                .OnFailure(errors => Console.WriteLine(errors.ToString()));
                        
            
        }

        [Fact]
        public void GetAllToolsReturnsSomething()
        {
            using var scope = ServiceProvider.CreateScope();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = scope.ServiceProvider.GetRequiredService<IToolService>();
            service.SetSession(NullUserSession.InternalSessionInstance);
            var entities = service.GetAll();
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

        [Theory]
        [InlineData(1)]
        public void GetToolDetailReturnsSomething(int toolManagementId)
        {
            using var scope = ServiceProvider.CreateScope();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = scope.ServiceProvider.GetRequiredService<IToolService>();
            service.SetSession(NullUserSession.InternalSessionInstance);

            var result = service.GetByToolManagementId(toolManagementId);
            result.Success.Should().BeTrue();
            result.Value.InnerId.Should().BeGreaterThan(0);
            result.Value.CodeGenerators.Should().NotBeEmpty();
            result.Value.Code.Should().NotBeEmpty();
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

        [Theory]
        [InlineData(EntityTypeEnum.ToolTS33)]
        public void GetAttributeDefinitionsReturnsSomething(EntityTypeEnum entityType)
        {
            using var scope = ServiceProvider.CreateScope();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = scope.ServiceProvider.GetRequiredService<IToolService>();
            service.SetSession(NullUserSession.InternalSessionInstance);
            var attributeDefinitons = service.GetAttributeDefinitions(entityType);
            attributeDefinitons.Should().NotBeEmpty();
            attributeDefinitons.Any(a => !string.IsNullOrEmpty(a.DisplayName)).Should().BeTrue();
        }

        [Fact]
        public void CreateToolDetailReturnsValidId()
        {
            using var scope = ServiceProvider.CreateScope();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = scope.ServiceProvider.GetRequiredService<IToolService>();
            service.SetSession(NullUserSession.InternalSessionInstance);
            var toolDetail = service.GetToolTemplateForCreation(new AttributeDefinitionFilter
            {
                ToolType = ToolTypeEnum.TS33,
                ConversionSystem = NullUserSession.InternalSessionInstance.ConversionSystem
            });
            

            var result = service.CreateTool(toolDetail);
            result.Success.Should().BeTrue();
        }
    }
}
