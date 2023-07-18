namespace Mitrol.Framework.XUnitTests
{
    using FluentAssertions;
    using global::XUnitTests;
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Application.Models.Production;
    using Mitrol.Framework.MachineManagement.Application.RulesHandlers;
    using Mitrol.Framework.MachineManagement.Application.Services;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Repositories;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
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
    }
}
