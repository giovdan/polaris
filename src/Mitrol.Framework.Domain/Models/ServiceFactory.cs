namespace Mitrol.Framework.Domain.Models.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class ServiceFactory : IServiceFactory
    {
        // Dependency-injection container reference for retrieving service objects.
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Default constructor for ServiceFactory class.
        /// </summary>
        /// <param name="serviceProvider">The IServiceProvider to retrieve the services from.</param>
        public ServiceFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        /// <summary>
        /// Get service of type serviceType from the IServiceProvider.
        /// </summary>
        /// <param name="serviceType">The type of service object to get.</param>
        /// <returns>A service object of type serviceType.</returns>
        public object GetService(Type serviceType) => _serviceProvider.GetService(serviceType);

        /// <summary>
        /// Get service of type TService from the IServiceProvider.
        /// </summary>
        /// <typeparam name="TService">The type of service object to get.</typeparam>
        /// <returns>A service object of type TService.</returns>
        public TService GetService<TService>() where TService : class
        {
            try
            {
                return _serviceProvider.GetRequiredService<TService>();
            }
            catch (ObjectDisposedException odex)
            {
                Debug.WriteLine($"{odex.Message}\n{odex.InnerException?.Message}:\n{odex.StackTrace} ReqType:{typeof(TService).Name}");
                throw;
            }
        }

        /// <summary>
        /// Invokes a resolver delegate and retrieves the corresponding service.
        /// </summary>
        /// <typeparam name="TResolver">The type of resolver to invoke for service retrieval.</typeparam>
        /// <typeparam name="TService">The type of service object to get.</typeparam>
        /// <returns>A service object of type TService.</returns>
        /// <remarks>
        /// It's a caller responsibility to assure that the can be called passing ServiceFactory as the only parameter. 
        /// </remarks>
        public TService Resolve<TService>() where TService : class
        {
            try
            {
                var resolver = _serviceProvider.GetRequiredService<IResolver<TService>>();
                return resolver.Resolve();
            }
            catch (ObjectDisposedException odex)
            {
                Debug.WriteLine($"{odex.Message}\n{odex.InnerException?.Message}:\n{odex.StackTrace} ReqType:{typeof(TService).Name}");
                throw;
            }
        }

        public TService Resolve<TService, TServiceKindEnum>(TServiceKindEnum serviceKind)
            where TService : class
            where TServiceKindEnum : Enum
        {
            try
            {
                var resolver = _serviceProvider.GetRequiredService<IResolver<TService, TServiceKindEnum>>();
                return resolver.Resolve(serviceKind);
            }
            catch (ObjectDisposedException odex)
            {
                Debug.WriteLine($"{odex.Message}\n{odex.InnerException?.Message}:\n{odex.StackTrace} ReqType:{typeof(TService).Name}");
                throw;
            }
        }

        public IEnumerable<TService> ResolveAll<TService>() where TService : class
        {
            foreach (var resolver in _serviceProvider.GetServices<IResolver<TService>>())
                yield return resolver.Resolve();
        }
    }
}
