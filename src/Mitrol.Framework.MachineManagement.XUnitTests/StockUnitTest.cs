namespace Mitrol.Framework.XUnitTests
{
    using FluentAssertions;
    using global::XUnitTests;
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Application.Models.Production;
    using Mitrol.Framework.MachineManagement.Application.RulesHandlers;
    using Mitrol.Framework.MachineManagement.Application.Services;
    using Mitrol.Framework.MachineManagement.Application.Validators;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Repositories;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class StockUnitTest : BaseUnitTest
    {
        public StockUnitTest() : base()
        {
            var services = new ServiceCollection();
            services.AddScoped<IQuantityBackLogRepository, QuantityBackLogRepository>();
            services.AddScoped<IDetailIdentifierRepository, DetailIdentifierRepository>();
            services.AddScoped<IStockService, StockService>();
            services.AddSingleton<IEntityHandlerFactory, EntityHandlerFactory>();
            services.AddScoped<IEntityValidator<StockItemToAdd, IMachineManagentDatabaseContext>
                            , StockValidator>();

            services.AddTransient<StockItemRulesHandler>();

            base.RegisterServices(services);
        }

        private IStockService InitializeService()
        {
            var scope = ServiceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IStockService>();
            service.SetSession(NullUserSession.InternalSessionInstance);
            return service;
        }

        [Fact()]
        public void GetStockListReturnsSomething()
        {
            var service = InitializeService();
            
            var list = service.GetStockItemList(MeasurementSystemEnum.MetricSystem);
            list.Any().Should().BeTrue();
            list.All(item => item.Identifiers.Values.Count() > 0).Should().BeTrue();
        }

        [Fact()]
        public void GetStockDetailsReturnsAStock()
        {
            var service = InitializeService();
            var result = service.GetStockItem(new StockItemFilter
                                            {
                                                ConversionSystemTo = MeasurementSystemEnum.ImperialSystem,
                                                Id = 4105
                                            });

            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.MaterialTypeId.Should().NotBe(MaterialTypeEnum.Undefined);
            result.Value.Groups.Any().Should().BeTrue();
            var profileCode =
                result.Value.Groups.SelectMany(group => group.Details)
                .SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.ProfileCode);
            if (profileCode != null)
            {
                result.Value.ProfileAttributes.Any().Should().BeTrue();
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData("3041d06b18d908023c701207472de9a1b0787520805ff86e8d077bf60d244c55")]
        public void GetStockFromHashCodeReturnsAValidStock(string hashCode)
        {
            var service = InitializeService();
            var result = service.GetStockItemByHashCode(hashCode);
            result.Success.Should().BeTrue();
            result.Value.Id.Should().BeGreaterThan(0);

        }

        [Fact()]
        public void GetFilteredMaterialCodesReturnsSomething()
        {
            var service = InitializeService();
            var dictionary = service.GetFilteredMaterialCodes();
            dictionary.Any().Should().BeTrue();
        }

        [Fact()]
        public void GetFilteredThicknessesReturnsSomething()
        {
            var service = InitializeService();
            var dictionary = service.GetFilteredThickness();
            dictionary.Any().Should().BeTrue();
        }

        [Fact()]
        public void GetFilteredProfileCodesReturnsSomething()
        {
            var service = InitializeService();
            var dictionary = service.GetFilteredProfileCodes(ProfileTypeEnum.P);
            dictionary.Any().Should().BeTrue();
        }

        [Fact]
        public void CreateStockReturnsValidId()
        {
            var service = InitializeService();
            var attributes = service.GetAttributeDefinitions(EntityTypeEnum.StockProfileP
                                    , MeasurementSystemEnum.MetricSystem
                                    , MeasurementSystemEnum.MetricSystem)
                        .Select(a =>
                        { 
                            if (a.EnumId == AttributeDefinitionEnum.MaterialCode)
                            {
                                a.Value.CurrentValue = JsonConvert.SerializeObject(new BaseInfoItem<long, string>
                                {
                                    Id = 4380,
                                    Value = "STEEL"
                                });
                            }
                            else if (a.AttributeKind == AttributeKindEnum.Number)
                            {
                                a.Value.CurrentValue = new Random().Next(1,20000);
                            }
                            return a;
                        });

            var result = service.CreateStockItem(new StockItemToAdd
            {
                ProfileTypeId = (long)ProfileTypeEnum.P,
                Attributes = attributes.ToDictionary(a => Enum.Parse<DatabaseDisplayNameEnum>(a.DisplayName)
                                                                , a => a.AttributeKind == AttributeKindEnum.Enum
                                                                        ? a.ControlType == ClientControlTypeEnum.ListBox 
                                                                            ? a.Value.CurrentValue
                                                                            : a.Value.CurrentValueId
                                                                        : a.Value.CurrentValue)
            });

            result.Success.Should().BeTrue();
            result.Value.Should().BeGreaterThan(0);
        }

        [Fact]
        public void RemoveStockReturnsOk()
        {
            var service = InitializeService();
            var lastStockId = service.GetStockItemList(MeasurementSystemEnum.MetricSystem)
                    .OrderByDescending(s => s.Id)
                    .Select(s => s.Id)
                    .FirstOrDefault();

            var result = service.RemoveStockItem(lastStockId);
            result.Success.Should().BeTrue();
        }
    }
}
