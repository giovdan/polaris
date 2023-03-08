namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using System;
    using System.Collections.Generic;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System.Linq;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.MachineManagement.Application.Core;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;

    public class EntityService : BaseService, IEntityService
    {
        protected IEFEntityRepository EntityRepository => ServiceFactory.GetService<IEFEntityRepository>();
        protected IEFAttributeValueRepository AttributeValueRepository => ServiceFactory.GetService<IEFAttributeValueRepository>();
        protected IChildLinkRepository ChildLinkRepository => ServiceFactory.GetService<IChildLinkRepository>();
        protected IEFAttributeDefinitionRepository AttributeDefinitionRepository => ServiceFactory.GetService<IEFAttributeDefinitionRepository>();
        #region < Private Methods > 
        private long[] InnerBatchCreate(IEnumerable<EntityItem> entities, IUnitOfWorkFactory<IEFDatabaseContext> factory)
        {
            HashSet<long> entityIds = new HashSet<long>();
            foreach (var entity in entities)
            {
                var result = InnerBatchCreate(entity, factory);
                if (result.Success)
                {
                    entityIds.Add(result.Value.Id);
                }
                else
                {
                    throw new Exception(ErrorCodesEnum.ERR_GEN007.ToString());
                }
            }

            return entityIds.ToArray();
        }

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
        private Result<MasterEntity> InnerBatchCreate(EntityItem entity, IUnitOfWorkFactory<IEFDatabaseContext> factory)
        {
            try
            {
                var uow = factory.GetOrCreate(UserSession);
                EntityRepository.Attach(uow);
                AttributeValueRepository.Attach(uow);
                var dbEntity = EntityRepository.Add(Mapper.Map<MasterEntity>(entity));
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
                return Result.Fail<MasterEntity>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private Result<MasterEntity> InnerBulkCreate(EntityItem entity, IUnitOfWorkFactory<IEFDatabaseContext> factory)
        {
            try
            {
                var uow = factory.GetOrCreate(UserSession);
                EntityRepository.Attach(uow);
                AttributeValueRepository.Attach(uow);
                var dbEntity = EntityRepository.Add(Mapper.Map<MasterEntity>(entity));
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
                return Result.Fail<MasterEntity>(ex.InnerException?.Message ?? ex.Message);
            }
        }

        #endregion

        public EntityService(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

        public Result<EntityItem> Create(EntityItem entity)
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
                        return Result.Fail<EntityItem>(ErrorCodesEnum.ERR_GEN007.ToString());
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
        public Result<EntityItem> Get(long entityId)
        {
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(UserSession);
                AttributeValueRepository.Attach(uow);

                var attributes = AttributeValueRepository.FindBy(a => a.EntityId == entityId);

                if (attributes.Any())
                {
                    IGrouping<MasterEntity, AttributeValue> entity =
                        attributes
                                .GroupBy(a => a.Entity)
                                .Single();

                    var result = Mapper.Map<EntityItem>(entity);

                    result.Attributes = result.Attributes.Select(a =>
                    {
                        var attributeInfo = a.EnumId.GetEnumAttribute<AttributeInfoAttribute>();
                        a.AttributeKind = attributeInfo.AttributeKind;
                        return a;
                    });

                    return Result.Ok(result);
                }
                else
                {
                    return Result.Fail<EntityItem>(ErrorCodesEnum.ERR_GEN002.ToString());
                }
            }
        }

        public IEnumerable<EntityListItem> GetAll()
        {
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(UserSession);
                EntityRepository.Attach(uow);
                return Mapper.Map<IEnumerable<EntityListItem>>(EntityRepository.GetAll());
            }
        }

        public Result<EntityItem> Update(EntityItem entity)
        {
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                try
                {
                    var uow = factory.GetOrCreate(UserSession);
                    EntityRepository.Attach(uow);
                    EntityRepository.Update(Mapper.Map<MasterEntity>(entity));
                    uow.Commit();
                    return Result.Ok(entity);
                }
                catch (Exception ex)
                {
                    return Result.Fail<EntityItem>(ex.InnerException?.Message ?? ex.Message);
                }
            }
        }

        public Result<long[]> BulkCreate(IEnumerable<EntityItem> entities)
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

        public Result<long[]> BatchCreate(IEnumerable<EntityItem> entities)
        {
            using (var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(UserSession);
                try
                {
                    uow.BeginTransaction();
                    var entityIds = InnerBatchCreate(entities, factory);
                    uow.Commit();
                    uow.CommitTransaction();
                    return Result.Ok(entityIds);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    return Result.Fail<long[]>(ex.InnerException?.Message ?? ex.Message);
                }
            }
        }

        public IEnumerable<AttributeItem> GetAttributesByType(EntityTypeEnum type)
        {
            using(var factory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(UserSession);
                AttributeDefinitionRepository.Attach(uow);
                return Mapper.Map<IEnumerable<AttributeItem>>(AttributeDefinitionRepository.FindBy(a => a.EntityTypeId == type))
                        .Select(a =>
                        {
                            var attributeInfo = a.EnumId.GetEnumAttribute<AttributeInfoAttribute>();
                            a.AttributeKind = attributeInfo.AttributeKind;
                            return a;
                        });
            }
        }
    }
}
