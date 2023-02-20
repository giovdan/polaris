namespace RepoDbVsEF.Application.Services
{
    using RepoDbVsEF.Application.Interfaces;
    using RepoDbVsEF.Application.Models;
    using System;
    using System.Collections.Generic;
    using RepoDbVsEF.Application.Core;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.EF.Data.Interfaces;
    using RepoDbVsEF.Domain.Models;

    public class EntityService : BaseService, IEntityService
    {
        protected IEFEntityRepository EntityRepository => ServiceFactory.GetService<IEFEntityRepository>();

        public EntityService(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }
        public Entity Create(Entity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ulong entityId)
        {
            throw new NotImplementedException();
        }

        public Entity Get(ulong entityId)
        {
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(UserSession);
                EntityRepository.Attach(uow);
                return Mapper.Map<Entity>(EntityRepository.Get(entityId));
            }

        }

        public IEnumerable<Entity> GetAll()
        {
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(UserSession);
                EntityRepository.Attach(uow);
                return Mapper.Map<IEnumerable<Entity>>(EntityRepository.GetAll());
            }
        }

        public Entity Update(Entity entity)
        {
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(UserSession);
                EntityRepository.Attach(uow);
                EntityRepository.Update(Mapper.Map<DatabaseEntity>(entity));
                return entity;
            }
        }
    }
}
