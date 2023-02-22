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
    using System.Linq;
    using RepoDbVsEF.Domain.Enums;

    public class EntityService : BaseService, IEntityService
    {
        protected IEFEntityRepository EntityRepository => ServiceFactory.GetService<IEFEntityRepository>();
        protected IEFAttributeValueRepository AttributeValueRepository => ServiceFactory.GetService<IEFAttributeValueRepository>();
        protected IChildLinkRepository ChildLinkRepository => ServiceFactory.GetService<IChildLinkRepository>();

        #region < Private Methods > 
        private void AddChildrenLinks(long id, IEnumerable<long> childrenIds
                                    , IUnitOfWorkFactory<IEFDatabaseContext> factory
                                    , bool withBatch = true)
        {
            var unitOfWork = factory.GetOrCreate(UserSession);
            ChildLinkRepository.Attach(unitOfWork);
            if (withBatch)
            {
                ChildLinkRepository.BatchInsert(childrenIds.Select((child, index) => new ChildLink
                {
                    ParentId = id,
                    ChildId = child,
                    Level = 1,
                    RowNumber = index + 1
                }));
            }
            else
            {
                ChildLinkRepository.BulkInsert(childrenIds.Select((child, index) => new ChildLink
                {
                    ParentId = id,
                    ChildId = child,
                    Level = 1,
                    RowNumber = index + 1
                }));

            }
        }

        /// <summary>
        /// InnerCreate
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        private Result<DatabaseEntity> InnerBatchCreate(Entity entity, IUnitOfWorkFactory<IEFDatabaseContext> factory)
        {
            try
            {
                var uow = factory.GetOrCreate(UserSession);
                EntityRepository.Attach(uow);
                AttributeValueRepository.Attach(uow);
                var dbEntity = EntityRepository.Add(Mapper.Map<DatabaseEntity>(entity));
                var attributes = entity.Attributes.Select(a => {
                    var dbAttribute = Mapper.Map<AttributeValue>(a);
                    return dbAttribute.SetAttributeValue(a);
                });
                attributes = attributes.Select(a => {
                    a.Entity = dbEntity;
                    return a;
                });
                AttributeValueRepository.BatchInsert(attributes);
                return Result.Ok(dbEntity);
            }
            catch (Exception ex)
            {
                return Result.Fail<DatabaseEntity>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private Result<DatabaseEntity> InnerBulkCreate(Entity entity, IUnitOfWorkFactory<IEFDatabaseContext> factory)
        {
            try
            {
                var uow = factory.GetOrCreate(UserSession);
                EntityRepository.Attach(uow);
                AttributeValueRepository.Attach(uow);
                var dbEntity = EntityRepository.Add(Mapper.Map<DatabaseEntity>(entity));
                var attributes = entity.Attributes.Select(a => {
                    var dbAttribute = Mapper.Map<AttributeValue>(a);
                    return dbAttribute.SetAttributeValue(a);
                });
                uow.Commit();

                attributes = attributes.Select(a => {
                    a.EntityId = dbEntity.Id;
                    return a;
                });
                AttributeValueRepository.BulkInsert(attributes);
                return Result.Ok(dbEntity);
            }
            catch (Exception ex)
            {
                return Result.Fail<DatabaseEntity>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        #endregion

        public EntityService(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

        public Result<Entity> Create(Entity entity)
        {
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(UserSession);
                uow.BeginTransaction();
                try
                {
                    var result = InnerBatchCreate(entity, factory);
                    if (result.Success)
                    {
                        uow.Commit();
                        uow.CommitTransaction();
                        entity.Id = result.Value.Id;
                    }
                    else
                    {
                        uow.RollBackTransaction();
                        return Result.Fail<Entity>(ErrorCodesEnum.ERR_GEN007.ToString());
                    }
                    
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Result.Fail(ex.InnerException?.Message ?? ex.Message);
                }

                return Result.Ok(entity);
            }
        }

        /// <summary>
        /// Create entity with children
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Result<EntityWithChildren> CreateEntityWithChildren(EntityWithChildren entity)
        {
            var result = Result.Ok(entity);
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(UserSession);
                uow.BeginTransaction();
                try
                {
                    result = InnerBatchCreate(entity, factory)
                            .OnSuccess(dbEntity =>
                            {
                                uow.Commit();
                                entity.Id = dbEntity.Id;

                                return  BatchCreate(entity.Children)
                                        .OnSuccess(identifiers =>
                                        {
                                            AddChildrenLinks(entity.Id, identifiers, factory);
                                            uow.Commit();
                                            uow.CommitTransaction();
                                            return Result.Ok(entity);
                                        });
                            })
                            .OnFailure(failureResult =>
                            {
                                uow.RollBackTransaction();
                                return Result.Fail<EntityWithChildren>(failureResult.Errors);
                            });

                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Result.Fail(ex.InnerException?.Message ?? ex.Message);
                }

                return result;
            }
        }

        public Result Delete(long entityId)
        {
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(UserSession);
                EntityRepository.Attach(uow);

                var dbEntity = EntityRepository.Get(entityId);

                if (dbEntity == null)
                {
                    return Result.Fail(ErrorCodesEnum.ERR_GEN002.ToString());
                }

                EntityRepository.Remove(dbEntity);
                return Result.Ok();

            }
        }
        public Entity Get(long entityId)
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

        public Result<Entity> Update(Entity entity)
        {
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                try
                {
                    var uow = factory.GetOrCreate(UserSession);
                    EntityRepository.Attach(uow);
                    EntityRepository.Update(Mapper.Map<DatabaseEntity>(entity));
                    uow.Commit();
                    return Result.Ok(entity);
                }
                catch (Exception ex)
                {
                    return Result.Fail<Entity>(ex.InnerException?.Message ?? ex.Message);
                }
            }
        }

        public Result<long[]> BulkCreate(IEnumerable<Entity> entities)
        {
            HashSet<long> entityIds = new HashSet<long>();
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(UserSession);
                try
                {
                    uow.BeginTransaction();
                    foreach(var entity in entities)
                    {
                        var result = InnerBulkCreate(entity, factory);
                        if (result.Success)
                        {
                            entityIds.Add(result.Value.Id);
                        }
                        else
                        {
                            throw new Exception(ErrorCodesEnum.ERR_GEN007.ToString());
                        }
                    }
                    uow.Commit();
                    uow.CommitTransaction();
                    return Result.Ok(entityIds.ToArray());
                }
                catch(Exception ex)
                {
                    uow.RollBackTransaction();
                    return Result.Fail<long[]>(ex.InnerException?.Message ?? ex.Message);
                }
            }
        }

        public Result<long[]> BatchCreate(IEnumerable<Entity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
