namespace RepoDbVsEF.Application.Services
{
    using RepoDbVsEF.Application.Interfaces;
    using RepoDbVsEF.Application.Models;
    using System;
    using System.Collections.Generic;
    using RepoDbVsEF.Application.Core;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.EF.Data.Interfaces;

    public class EntityService : BaseService, IEntityService
    {
        private IEFEntityRepository EntityRepository => ServiceFactory.GetService<IEFEntityRepository>();

        public EntityService(IServiceFactory serviceFactory): base(serviceFactory)
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
            return Mapper.Map<Entity>(EntityRepository.Get(entityId));
        }

        public IEnumerable<Entity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Entity Update(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
