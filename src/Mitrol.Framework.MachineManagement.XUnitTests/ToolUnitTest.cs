using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Mitrol.Framework.Domain.Models;
using Mitrol.Framework.MachineManagement.Application.Interfaces;
using Mitrol.Framework.MachineManagement.Application.Services;
using System.Globalization;
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
            RegisterServices(services);
        }

        [Fact]
        public void GetAllToolsReturnsSomething()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var service = ServiceProvider.GetRequiredService<IToolService>();
            service.SetSession(NullUserSession.InternalSessionInstance);
            var entities = service.GetAll();
            entities.Should().NotBeNullOrEmpty();
        }
    }
}
