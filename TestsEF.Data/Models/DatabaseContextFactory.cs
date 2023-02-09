namespace RepoDbVsEF.EF.Data.Models
{
    using RepoDbVsEF.Domain.Interfaces;

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
