namespace XUnitTests.EFCore
{
    using Microsoft.Extensions.DependencyInjection;
    using RepoDbVsEF.Domain;
    using RepoDbVsEF.Domain.Attributes;
    using RepoDbVsEF.Domain.Enums;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.EF.Data.Interfaces;
    using RepoDbVsEF.EF.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using Xunit;
    using XUnitTests.Models;

    [Trait("TestType", "Entity")]
    public class EntityTest: BaseUnitTest
    {
        private Dictionary<EntityTypeEnum, IEnumerable<AttributeDefinition>> AttributeDefinitionsDictionary { get; set; }
        protected CancellationTokenSource CancellationTokenSource { get; private set; }

        #region < Private Methods >
        private void AddChildrenLinks(ulong id, IEnumerable<ulong> childrenIds, IServiceScope scope
                                                      , IUnitOfWork<IEFDatabaseContext> unitOfWork
                                                      , bool withBatch = true)
        {
            var linkChildRepository = scope.ServiceProvider.GetRequiredService<IChildLinkRepository>();

            linkChildRepository.Attach(unitOfWork);
            if (withBatch)
            {
                linkChildRepository.BatchInsert(childrenIds.Select((child, index) => new ChildLink
                {
                    ParentId = id,
                    ChildId = child,
                    Level = 1,
                    RowNumber = index + 1
                }));
            }
            else
            {
                linkChildRepository.BulkInsert(childrenIds.Select((child, index) => new ChildLink
                {
                    ParentId = id,
                    ChildId = child,
                    Level = 1,
                    RowNumber = index + 1
                }));

            }
        }

        private EntityToCreate GenerateEntity(EntityTypeEnum entityTypeEnum, IServiceScope scope)
        {
            var attributeDefinitions = Enumerable.Empty<AttributeDefinition>();
            if (AttributeDefinitionsDictionary.ContainsKey(entityTypeEnum))
            {
                attributeDefinitions = AttributeDefinitionsDictionary.GetValueOrDefault(entityTypeEnum);
            }
            else
            {
                var attributeDefinitionRepository = scope.ServiceProvider.GetRequiredService<IEFAttributeDefinitionRepository>();
                var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IEFDatabaseContext>>().GetOrCreate(NullUserSession.Instance);
                attributeDefinitionRepository.Attach(uow);
                attributeDefinitions = attributeDefinitionRepository.FindBy(a => a.EntityTypeId == entityTypeEnum).ToHashSet();
                AttributeDefinitionsDictionary.Add(entityTypeEnum, attributeDefinitions);
            }

            return new EntityToCreate
            {
                DisplayName = $"EFCORE_{DomainExtensions.RandomString(20)}",
                EntityType = entityTypeEnum,
                Attributes = GenerateAttributes(attributeDefinitions)
            };

        }

        private IEnumerable<AttributeItem> GenerateAttributes(IEnumerable<AttributeDefinition> attributeDefinitions)
        {
            var rnd = new Random();
            return attributeDefinitions
                       .Select(a => {
                           var attributeInfo = a.EnumId.GetEnumAttribute<AttributeInfoAttribute>();

                           var attributeValue = attributeInfo.AttributeKind == AttributeKindEnum.String
                                               ? (object)DomainExtensions.RandomString(10)
                                               : attributeInfo.AttributeKind == AttributeKindEnum.Date
                                                    ? DateTime.UtcNow.AddDays(rnd.Next(0, 20)).ToEpoch()
                                                    : rnd.NextDouble();

                           var attributeItem = new AttributeItem
                           {
                               AttributeDefinitionId = a.Id,
                               AttributeKind = attributeInfo.AttributeKind,
                               Value = new AttributeValueItem
                               {
                                   CurrentValue = attributeInfo.AttributeKind != AttributeKindEnum.Enum
                                               ? attributeValue : null,
                                   CurrentValueId = attributeInfo.AttributeKind == AttributeKindEnum.Enum
                                               ? int.Parse(attributeValue.ToString()) : 0
                               }
                           };

                           return attributeItem;
                       });

        }


        /// <summary>
        /// Creazione entità
        /// </summary>
        /// <param name="recordsNumber"></param>
        /// <param name="entityType"></param>
        /// <param name="attributeDefinitionRepository"></param>
        /// <returns></returns>
        private IEnumerable<EntityToCreate> GenerateEntities(int recordsNumber
                                    , IServiceScope scope
                                    , EntityTypeEnum entityTypeEnum = EntityTypeEnum.None)
        {
            var rnd = new Random();
            if (entityTypeEnum == EntityTypeEnum.None)
            {
                var entityType = rnd.Next(1, 16);

                while (!Enum.IsDefined(typeof(EntityTypeEnum), entityType))
                {
                    entityType = rnd.Next(1, 16);
                }

                if (Enum.TryParse(entityType.ToString(), out entityTypeEnum))
                {

                }
            }

            for (int i = 1; i <= recordsNumber; i++)
            {
                yield return GenerateEntity(entityTypeEnum, scope);
            }


        }
        /// <summary>
        /// Create single entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="scope"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        private Entity CreateEntity(EntityToCreate entity, IServiceScope scope
                            , bool withBatch = true)
        {
            var repository = scope.ServiceProvider.GetRequiredService<IEFEntityRepository>();
            var attributeRepository = scope.ServiceProvider.GetRequiredService<IEFAttributeValueRepository>();
            var dbEntity = Mapper.Map<Entity>(entity);

            try
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IEFDatabaseContext>>()
                                        .GetOrCreate(NullUserSession.Instance);
                repository.Attach(unitOfWork);
                attributeRepository.Attach(unitOfWork);
                dbEntity = repository.Add(dbEntity);

                var attributes = entity.Attributes.Select(a => {
                    var dbAttribute = Mapper.Map<AttributeValue>(a);
                    return dbAttribute.SetAttributeValue(a);
                });
                if (withBatch)
                {
                    attributes = attributes.Select(a => {
                        a.Entity = dbEntity;
                        return a;
                    });
                    attributeRepository.BatchInsert(attributes);
                }
                else
                {
                    unitOfWork.Commit();
                    attributes = attributes.Select(a => { a.EntityId = dbEntity.Id; return a; }).ToHashSet();
                    attributeRepository.BulkInsert(attributes);
                }

                return dbEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void UpdateEntity(IServiceScope scope)
        {
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork<IEFDatabaseContext>>();
            var repository = scope.ServiceProvider.GetRequiredService<IEFEntityRepository>();
            var attributeRepository = scope.ServiceProvider.GetRequiredService<IEFAttributeValueRepository>();

            repository.Attach(uow);
            attributeRepository.Attach(uow);

            var latest = repository.GetAll()
                            .OrderBy(e => e.UpdatedOn)
                            .LastOrDefault();

            if (latest == null)
            {
                return;
            }

            var rnd = new Random();
            var attributes = attributeRepository.FindBy(a => a.EntityId == latest.Id)
                .Select(a =>
                {
                    var attributeInfo = a.AttributeDefinition.EnumId.GetEnumAttribute<AttributeInfoAttribute>();
                    switch (attributeInfo.AttributeKind)
                    {
                        case AttributeKindEnum.Number:
                        case AttributeKindEnum.Enum:
                        case AttributeKindEnum.Bool:
                            {
                                a.Value = Convert.ToDecimal(rnd.NextDouble());
                                a.TextValue = string.Empty;
                            }
                            break;
                        case AttributeKindEnum.String:
                            {
                                a.TextValue = DomainExtensions.RandomString(20);
                                a.Value = 0;
                            }
                            break;
                        case AttributeKindEnum.Date:
                            {
                                a.Value = DateTime.UtcNow.AddDays(rnd.Next(-2, 5)).Ticks;
                                a.Value = 0;
                            }
                            break;
                    }

                    return a;
                });

            var oldRowVersion = latest.RowVersion;
            attributeRepository.BatchUpdate(attributes);
            uow.Commit();
        }

        private void InnerUpdateEntities(int recordsNumber)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(NullUserSession.Instance);

                uow.BeginTransaction();
                try
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    for (int i = 0; i <= recordsNumber; i++)
                    {
                        UpdateEntity(scope);
                    }
                    uow.CommitTransaction();
                    sw.Stop();
                    Trace.WriteLine($"EF Core - Updated {recordsNumber} entities - Elapsed time: {sw.ElapsedMilliseconds}");
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                }
            }
        }

        private int InnerCreateEntities(int recordsNumber)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(NullUserSession.Instance);
                var entities = GenerateEntities(recordsNumber, scope).ToHashSet();
                uow.BeginTransaction();
                try
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    int rowsNumber = 0;
                    foreach (var entity in entities)
                    {
                        rowsNumber += CreateEntity(entity, scope) != null ? 1 : 0;
                    }

                    uow.Commit();
                    uow.CommitTransaction();
                    sw.Stop();
                    Trace.WriteLine($"EF Core - Batch Insert {recordsNumber} entities - Elapsed time: {sw.ElapsedMilliseconds}");
                    AttributeDefinitionsDictionary.Clear();
                    return rowsNumber;
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    return -1;
                }
            }
        }

        private Entity InnerCreateEntity(EntityTypeEnum entityType)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(NullUserSession.Instance);
                uow.BeginTransaction();
                var sw = new Stopwatch();
                sw.Start();
                try
                {
                    var entity = CreateEntity(GenerateEntity(entityType, scope), scope);
                    uow.Commit();
                    uow.CommitTransaction();
                    return entity;
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    return null;
                }

                sw.Stop();
                Trace.WriteLine($"EF Core - Insert single Entity - Elapsed time: {sw.ElapsedMilliseconds}");
                AttributeDefinitionsDictionary.Clear();
            }
        }
        #endregion

        public EntityTest(): base()
        {
            CancellationTokenSource = new CancellationTokenSource();
            var services = new ServiceCollection();
            services.AddScoped<IEFEntityRepository, EFEntityRepostiory>();
            services.AddScoped<IEFAttributeDefinitionRepository, EFAttributeDefinitionRepository>();
            services.AddScoped<IEFAttributeValueRepository, EFAttributeValueRepository>();
            services.AddScoped<IChildLinkRepository, EFChildLinkRepository>();
            RegisterServices(services, isRepoDb: false);
        }

        [Fact]
        public void GetAllEntitiesReturnsSomething()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(NullUserSession.Instance);
                var repository = scope.ServiceProvider.GetRequiredService<IEFEntityRepository>();
                repository.Attach(uow);
                var suppliers = repository.GetAll();
                Assert.True(suppliers.Any());
            }
        }

        [Theory()]
        [InlineData(EntityTypeEnum.Customer)]
        public void CreateEntityReturnsOk(EntityTypeEnum entityType)
        {
            var entity = InnerCreateEntity(entityType);
            Assert.True((entity?.Id ?? 0) > 0);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(500)]
        public void CreateEntitiesReturnsOk(int recordNumber)
        {

        }
    }
}
