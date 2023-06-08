using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mitrol.Framework.Domain.Models;
using Mitrol.Framework.MachineManagement.Application.Interfaces;
using Mitrol.Framework.MachineManagement.Application.Services;
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
            services.AddScoped<IToolService, ToolService>();
            services.AddScoped<IDetailIdentifierRepository, DetailIdentifierRepository>();
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
            var toolId = service.GetAll().LastOrDefault().Id;
            var result = service.Get(toolId);
            result.Success.Should().BeTrue();
            result.Value.Id.Should().BeGreaterThan(0);
            result.Value.Id.Should().Be(toolId);
        }
    }
}
