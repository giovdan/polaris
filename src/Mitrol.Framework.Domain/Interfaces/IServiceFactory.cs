namespace Mitrol.Framework.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IServiceFactory
    {
        object GetService(Type serviceType);
        TService GetService<TService>() where TService : class;

        TService Resolve<TService>() where TService : class;
        TService Resolve<TService, TServiceKindEnum>(TServiceKindEnum serviceKind)
            where TService : class
            where TServiceKindEnum : Enum;

        IEnumerable<TService> ResolveAll<TService>() where TService : class;
    }
}