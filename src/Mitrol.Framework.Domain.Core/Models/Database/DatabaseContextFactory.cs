namespace Mitrol.Framework.Domain.Core.Models
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;

    public class DatabaseContextFactory : IDatabaseContextFactory
    {
        private readonly IServiceFactory _serviceFactory;

        public DatabaseContextFactory(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IDatabaseContext Create()
        {
            return _serviceFactory.GetService<IDatabaseContext>();
        }
    }
}