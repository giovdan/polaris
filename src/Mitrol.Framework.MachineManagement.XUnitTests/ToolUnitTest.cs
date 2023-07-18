﻿namespace Mitrol.Framework.XUnitTests
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


        public ToolUnitTest():base()
        {
            var services = new ServiceCollection();
            services.AddSingleton<DrillStatus>();
            services.AddSingleton<TorchOxyStatus>();
            services.AddSingleton<TorchPlaStatus>();
            services.AddSingleton<SawStatus>();

            services.AddScoped<IResolver<IBootableService>, ServiceForward<IBootableService, IMachineConfigurationService>>();

            services.AddTransient<IResolver<IToolStatus, PlantUnitEnum>, ToolStatusResolver>();
            services.AddScoped<IResolver<IRemoteToolService>, ServiceForward<IRemoteToolService, IToolService>>();
            services.AddScoped<IToolService, ToolService>();
            services.AddScoped<IDetailIdentifierRepository, DetailIdentifierRepository>();
            services.AddScoped<IMachineParameterRepository, MachineParameterRepository>();
            services.AddScoped<IEntityValidator<ToolDetailItem>, ToolValidator>();
            services.AddScoped<IExecutionService, ExecutionService>();
            services.AddScoped<IMachineParameterService, MachineParameterService>();
            services.AddSingleton<IEntityHandlerFactory, EntityHandlerFactory>();
            services.AddScoped<IResolver<IEntityRulesHandler<ToolDetailItem>>
                            , ToolRulesHandlerResolver>();
            services.AddTransient<ToolRulesHandler>();
            RegisterServices(services);
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

            toolDetail.Identifiers = toolDetail.Identifiers.Select(i =>
            {
                if (i.EnumId == AttributeDefinitionEnum.NominalDiameter
                    && int.TryParse(i.Value.CurrentValue.ToString(), out var intValue)
                    && intValue == 0)
                {
                    i.Value.CurrentValue = 20;
                }

                return i;
            }).ToList();

            toolDetail.Attributes = toolDetail.Attributes.Select(a =>
            {
                switch(a.EnumId)
                {
                    case AttributeDefinitionEnum.StartPosition:
                        a.Value.CurrentValue = 0.1;
                        break;
                    case AttributeDefinitionEnum.EndPosition:
                        a.Value.CurrentValue = 0.4;
                        break;
                    case AttributeDefinitionEnum.FastApproachPosition:
                        a.Value.CurrentValue = 0.5;
                        break;
                    case AttributeDefinitionEnum.ToolLength:
                        a.Value.CurrentValue = 150;
                        break;
                    case AttributeDefinitionEnum.RealDiameter:
                        a.Value.CurrentValue = 19.40;
                        break;
                    case AttributeDefinitionEnum.GrindingAngle:
                        a.Value.CurrentValue = 1;
                        break;
                    case AttributeDefinitionEnum.ForwardSpeed:
                        a.Value.CurrentValue = 1;
                        break;
                    case AttributeDefinitionEnum.CuttingSpeed:
                        a.Value.CurrentValue = 1;
                        break;
                    case AttributeDefinitionEnum.WarehouseId:
                        a.Value.CurrentValueId = 1;
                        break;
                }
                if (a.EnumId == AttributeDefinitionEnum.WarehouseId)
                {
                    if (a.Value.CurrentValueId == 0)
                    {
                        a.Value.CurrentValueId = 1;
                        a.Value.CurrentValue = 1;
                    }
                }
                return a;
            }).ToList();
            

            var result = service.CreateTool(toolDetail);
            result.Success.Should().BeTrue();
            result.Value.Id.Should().BeGreaterThan(0);
            result.Value.Attributes.Any().Should().BeTrue();
        }

        [Fact()]
        public void RemoveToolReturnsOk()
        {
            using var scope = ServiceProvider.CreateScope();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = scope.ServiceProvider.GetRequiredService<IToolService>();
            service.SetSession(NullUserSession.InternalSessionInstance);

            var toolId = service.GetAll().OrderByDescending(e => e.Id)
                .FirstOrDefault().InnerId;

            var result = service.Remove(toolId);
            result.Success.Should().BeTrue();
        }

        [Fact()]
        public void UpdateToolAttributeReturnsOk()
        {
            using var scope = ServiceProvider.CreateScope();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = scope.ServiceProvider.GetRequiredService<IToolService>();
            service.SetSession(NullUserSession.InternalSessionInstance);

            var result = service.GetByToolManagementId(1);

            result.Success.Should().BeTrue();

            result.Value.Attributes.Select(a =>
            {
                if (a.AttributeKind == AttributeKindEnum.Number
                    && decimal.TryParse(a.Value.CurrentValue.ToString(), out var decimalValue))
                {
                    a.Value.CurrentValue = decimalValue + (decimal)0.1;
                }

                return a;
            });

            service.UpdateTool(result.Value);
        }

        #region < Remote Service Unit Tests >
        [Fact()]
        public void GetToolsIdentifiersReturnsSomething()
        {
            using var scope = ServiceProvider.CreateScope();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = scope.ServiceProvider.GetRequiredService<IToolService>();
            service.SetSession(NullUserSession.InternalSessionInstance);

            var list = service.GetToolIdentifiers(new ToolItemIdentifiersFilter
            { 
                UnitType = PlantUnitEnum.DrillingMachine
            });
            list.Any().Should().BeTrue();
            list.Select(item => item.Attributes.Count()).Should().HaveCountGreaterThan(0);
            list.Select(item => item.Identifiers.Count()).Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public void GetToolReturnsSomething()
        {
            using var scope = ServiceProvider.CreateScope();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = scope.ServiceProvider.GetRequiredService<IToolService>();
            service.SetSession(NullUserSession.InternalSessionInstance);
            var result = service.GetTool(new ToolItemFilter
            {
                ToolManagementId = 1
            });

            result.Should().NotBeNull();
            (result.ToolManagementId == 1).Should().BeTrue();
            result.Identifiers.Values.Any().Should().BeTrue();
            result.Attributes.Values.Any().Should().BeTrue();
        }
        #endregion
    }
}
