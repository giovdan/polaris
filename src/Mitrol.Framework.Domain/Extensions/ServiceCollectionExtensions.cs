namespace Mitrol.Framework.Domain.Extensions
{
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddClassesInheritingFrom<TBase>(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var baseType = typeof(TBase);
            foreach (var type in baseType.Assembly.GetTypes()
                .Where(type => type.IsAbstract is false && baseType.IsAssignableFrom(type)))
            {
                services.Add(new ServiceDescriptor(
                    serviceType: type,
                    implementationType: type,
                    lifetime: serviceLifetime));
            }

            return services;
        }
    }
}
