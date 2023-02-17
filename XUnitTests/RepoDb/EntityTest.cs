namespace XUnitTests.RepoDb
{
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using RepoDbVsEF.Data.Interfaces;
    using RepoDbVsEF.Domain;
    using RepoDbVsEF.Domain.Attributes;
    using RepoDbVsEF.Domain.Enums;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.Domain.Threading;
    using RepoDbVsEF.RepoDb.Data.Interfaces;
    using RepoDbVsEF.RepoDb.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
    using XUnitTests.Models;

    public class EntityTest: BaseUnitTest
    {
        private CancellationTokenSource CancellationTokenSource;
        private Dictionary<EntityTypeEnum, IEnumerable<AttributeDefinition>> AttributeDefinitionsDictionary { get; set; }

        #region < Private Methods >
        private void AddChildrenLinks(ulong id, IEnumerable<ulong> childrenIds, IServiceScope scope
                                                      , bool withBatch = true)
        {
            var linkChildRepository = scope.ServiceProvider.GetRequiredService<IChildLinkRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>()
                            .GetOrCreate(NullUserSession.Instance);
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

        private IEnumerable<DatabaseEntity> CreateEntities(IEnumerable<EntityToCreate> entities, IServiceScope scope
                                                    , bool withBatch = true)
        {
            foreach (var entity in entities)
            {
                yield return CreateEntity(entity, scope, withBatch);
            }
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
                                    , IUnitOfWork<IRepoDbDatabaseContext> unitOfWork
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

        private EntityToCreate GenerateEntity(EntityTypeEnum entityTypeEnum, IServiceScope scope)
        {
            var attributeDefinitions = Enumerable.Empty<AttributeDefinition>();
            if (AttributeDefinitionsDictionary.ContainsKey(entityTypeEnum))
            {
                attributeDefinitions = AttributeDefinitionsDictionary.GetValueOrDefault(entityTypeEnum);
            }
            else
            {
                var attributeDefinitionRepository = scope.ServiceProvider.GetRequiredService<IRepoDbAttributeDefinitionRepository>();
                attributeDefinitions = attributeDefinitionRepository.FindBy(a => a.EntityTypeId == entityTypeEnum);
                AttributeDefinitionsDictionary.TryAdd(entityTypeEnum, attributeDefinitions);
            }

            return new EntityToCreate
            {
                DisplayName = $"REPODB_{DomainExtensions.RandomString(20)}",
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
                                    , IServiceScope scope)
        {
            var rnd = new Random();
            var entityType = rnd.Next(1, 16);

            while (!Enum.IsDefined(typeof(EntityTypeEnum), entityType))
            {
                entityType = rnd.Next(1, 16);
            }

            var entityTypeEnum = EntityTypeEnum.Supplier;
            if (Enum.TryParse(entityType.ToString(), out entityTypeEnum))
            {

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
        private DatabaseEntity CreateEntity(EntityToCreate entity, IServiceScope scope
                            , bool withBatch = true)
        {
            var repository = scope.ServiceProvider.GetRequiredService<IRepoDbEntityRepository>();
            var attributeRepository = scope.ServiceProvider.GetRequiredService<IRepoDbAttributeValueRepository>();
            var dbEntity = Mapper.Map<DatabaseEntity>(entity);
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>()
                            .GetOrCreate(NullUserSession.Instance);
            try
            {
                repository.Attach(unitOfWork);
                attributeRepository.Attach(unitOfWork);
                dbEntity.SetAuditableFields(unitOfWork.UserSession?.Username ?? "MITROL");
                dbEntity.RowVersion = Guid.NewGuid().ToString();
                dbEntity = repository.Add(dbEntity);

                var attributes = entity.Attributes.Select(a => {
                    var dbAttribute = Mapper.Map<AttributeValue>(a);
                    dbAttribute.EntityId = dbEntity.Id;
                    dbAttribute.Entity = dbEntity;
                    dbAttribute.SetAuditableFields(unitOfWork.UserSession?.Username ?? "MITROL");
                    dbAttribute.RowVersion = Guid.NewGuid().ToString();
                    return dbAttribute.SetAttributeValue(a);
                });

                var result = withBatch ? attributeRepository.BatchInsert(attributes): attributeRepository.BulkInsert(attributes);
                return dbEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private int InnerCreateEntities(int recordsNumber)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
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

        private DatabaseEntity InnerCreateEntity(EntityTypeEnum entityType)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
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

        public EntityTest():base()
        {
            CancellationTokenSource = new CancellationTokenSource();
            AttributeDefinitionsDictionary = new Dictionary<EntityTypeEnum, IEnumerable<AttributeDefinition>>();
            var services = new ServiceCollection();
            //services.AddScoped<IRepoDbEntityRepository, RepoDbEntityRepository>();
            services.AddScoped<IRepoDbEntityRepository>(service => new RepoDbEntityRepository(ConnectionString));
            services.AddScoped<IRepoDbAttributeDefinitionRepository, RepoDbAttributeDefinitionRepository>(service => new RepoDbAttributeDefinitionRepository(ConnectionString));
            services.AddScoped<IRepoDbAttributeValueRepository, RepoDbAttributeValueRepository>(service => new RepoDbAttributeValueRepository(ConnectionString));
            services.AddScoped<IChildLinkRepository, RepoDbChildLinkRepository>(service => new RepoDbChildLinkRepository(ConnectionString));
            base.RegisterServices(services);
        }


        [Theory(), InlineData(EntityTypeEnum.Customer)]
        public void InsertEntityReturnsSomething(EntityTypeEnum entityType)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(NullUserSession.Instance);
                uow.BeginTransaction();
                var sw = new Stopwatch();
                sw.Start();
                try
                {
                    CreateEntity(GenerateEntity(entityType, scope), scope);
                    uow.CommitTransaction();
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }

                sw.Stop();
                Trace.WriteLine($"EF Core - Insert single Entity - Elapsed time: {sw.ElapsedMilliseconds}");
                AttributeDefinitionsDictionary.Clear();
            }
        }

        [Theory, MemberData(nameof(GetRecordCounts))]
        public void InsertMultipleEntitiesReturnsOk(int recordsNumber)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            recordsNumber.Should().BeGreaterThanOrEqualTo(0);

            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
            {
                using var uow = factory.GetOrCreate(NullUserSession.Instance);
                var entities = GenerateEntities(recordsNumber, scope, uow).ToHashSet();
                uow.BeginTransaction();
                try
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    int rowsNumber = 0;
                    foreach (var entity in entities)
                    {
                        rowsNumber += CreateEntity(entity, scope).Id > 0 ? 1: 0;
                    }

                    uow.CommitTransaction();
                    sw.Stop();
                    Trace.WriteLine($"EF Core - Batch Insert {recordsNumber} entities - Elapsed time: {sw.ElapsedMilliseconds}");
                    AttributeDefinitionsDictionary.Clear();
                    rowsNumber.Should().Be(recordsNumber);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }
            }
        }

        [Theory, MemberData(nameof(GetRecordCounts))]
        public void BulkInsertMultipleEntitiesReturnsOk(int recordsNumber)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            recordsNumber.Should().BeGreaterThanOrEqualTo(0);

            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
            {
                var uow = factory.GetOrCreate(NullUserSession.Instance);
                var entities = GenerateEntities(recordsNumber, scope, uow).ToHashSet();
                uow.BeginTransaction();
                try
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    int rowsNumber = 0;
                    foreach (var entity in entities)
                    {
                        rowsNumber += CreateEntity(entity, scope,withBatch: false).Id > 0 ? 1: 0;
                    }

                    uow.CommitTransaction();
                    sw.Stop();
                    Trace.WriteLine($"EF Core - Bulk Insert {recordsNumber} entities - Elapsed time: {sw.ElapsedMilliseconds}");
                    AttributeDefinitionsDictionary.Clear();
                    rowsNumber.Should().Be(recordsNumber);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }
            }

        }

        [Fact]
        public void UpdateEntityReturnsOk()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
            {
                using var uow = factory.GetOrCreate(NullUserSession.Instance);
                uow.BeginTransaction();
                try
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IRepoDbEntityRepository>();
                    var attributeRepository = scope.ServiceProvider.GetRequiredService<IRepoDbAttributeValueRepository>();

                    repository.Attach(uow);
                    attributeRepository.Attach(uow);

                    var latest = repository.GetAll()
                                    .OrderBy(e => e.UpdatedOn)
                                    .LastOrDefault();

                    latest.Should().NotBeNull();

                    var rnd = new Random();
                    var attributes = attributeRepository.FindBy(a => a.EntityId == latest.Id)
                        .Select(a =>
                        {
                            a.Entity = latest;
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
                    latest = repository.Update(latest);
                    uow.CommitTransaction();
                    latest = repository.Get(latest.Id);
                    latest.RowVersion.Should().NotBe(oldRowVersion);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }


            }


        }

        [Fact]
        public void RawUpdateEntityReturnsOk()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
            {
                using var uow = factory.GetOrCreate(NullUserSession.Instance);
                uow.BeginTransaction();
                try
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IRepoDbEntityRepository>();
                    var attributeRepository = scope.ServiceProvider.GetRequiredService<IRepoDbAttributeValueRepository>();

                    repository.Attach(uow);
                    attributeRepository.Attach(uow);

                    var latest = repository.GetAll()
                                    .OrderBy(e => e.UpdatedOn)
                                    .LastOrDefault();

                    latest.Should().NotBeNull();

                    var rnd = new Random();
                    var attributes = attributeRepository.FindBy(a => a.EntityId == latest.Id)
                        .Select(a =>
                        {
                            a.Entity = latest;
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
                    attributeRepository.BulkUpdate(attributes);
                    var count = repository.RawUpdate(latest);
                    uow.CommitTransaction();

                    if (count > 0)
                    {
                        latest = repository.Get(latest.Id);
                    }


                    latest.RowVersion.Should().NotBe(oldRowVersion);
                }
                catch (Exception)
                {
                    uow.RollBackTransaction();
                    throw;
                }


            }
        }

        [Theory, MemberData(nameof(GetRecordCounts))]
        public void CreateEntityWithChildrenReturnsSomething(int childrenCount)
        {
            if (childrenCount == 0)
                Assert.True(true);

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
            {
                using var uow = factory.GetOrCreate(NullUserSession.Instance);
                uow.BeginTransaction();
                try
                {
                    var entities = new List<EntityToCreate>();
                    entities.Add(GenerateEntity(EntityTypeEnum.Order, scope));
                    entities.AddRange(GenerateEntities(childrenCount, scope, uow, EntityTypeEnum.OrderRow));
                    var dbEntities = CreateEntities(entities, scope).ToList();
                    var parentId = dbEntities.SingleOrDefault(entity => entity.EntityTypeId == EntityTypeEnum.Order)?.Id ?? 0;
                    if (parentId > 0)
                    {
                        AddChildrenLinks(parentId
                                            , dbEntities.Where(entity => entity.EntityTypeId == EntityTypeEnum.OrderRow)
                                                .Select(child => child.Id), scope);
                        uow.CommitTransaction();
                    }
                    else
                    {
                        uow.RollBackTransaction();
                    }

                    parentId.Should().BeGreaterThan(0);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }
            }
        }

        [Theory, MemberData(nameof(GetRecordCounts))]
        public void BulkCreateEntityWithChildrenReturnsSomething(int childrenCount)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
            {
                using var uow = factory.GetOrCreate(NullUserSession.Instance);
                uow.BeginTransaction();
                try
                {
                    var entities = new List<EntityToCreate>();
                    entities.Add(GenerateEntity(EntityTypeEnum.Order, scope));
                    entities.AddRange(GenerateEntities(childrenCount, scope, uow, EntityTypeEnum.OrderRow));
                    var dbEntities = CreateEntities(entities, scope,  withBatch: false).ToList();
                    var parentId = dbEntities.SingleOrDefault(entity => entity.EntityTypeId == EntityTypeEnum.Order)?.Id ?? 0;
                    if (parentId > 0)
                    {
                        AddChildrenLinks(parentId
                                            , dbEntities.Where(entity => entity.EntityTypeId == EntityTypeEnum.OrderRow)
                                                .Select(child => child.Id), scope);
                        uow.CommitTransaction();
                    }
                    else
                    {
                        uow.RollBackTransaction();
                    }

                    parentId.Should().BeGreaterThan(0);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }
            }

        }

        [Fact]
        public void GetEntitiesReturnsSomething()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IRepoDbEntityRepository>();
            var entities = service.GetAll();
            entities.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void ConcurrentOperationsDoesntFail()
        {
            var task1 = TimedTaskFactory.Start(() =>
                {
                    InnerCreateEntity(EntityTypeEnum.Product);
                }
                , intervalInMilliseconds: 500
                , synchronous: true
                , cancelToken: CancellationTokenSource.Token);

            // Creazione task di esecuzione 'medio'
            var task2 = TimedTaskFactory.Start(() =>
            {
                var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                //UpdateEntity(scope);
                var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>()
                        .GetOrCreate(NullUserSession.Instance);
                var repository = scope.ServiceProvider.GetRequiredService<IRepoDbEntityRepository>();
                repository.Attach(uow);
                repository.GetAll();
            }
                , intervalInMilliseconds: 100
                , synchronous: true
                , cancelToken: CancellationTokenSource.Token);

            Task.WaitAll(task1, task2);
        }

        [Fact]
        public void ParallelEntityCreationDoesNotFailsForConcurrency()
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                using var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>()
                            .GetOrCreate(NullUserSession.Instance);
                try
                {
                    uow.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                    CreateEntity(GenerateEntity(EntityTypeEnum.Customer, scope), scope);
                    uow.CommitTransaction();
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }
            });

            var task2 = Task.Factory.StartNew(() =>
            {
                using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                using var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>()
                        .GetOrCreate(NullUserSession.Instance);
                try
                {
                    uow.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    var repository = scope.ServiceProvider.GetRequiredService<IRepoDbAttributeValueRepository>();
                    repository.Attach(uow);
                    var latest = repository.GetAll().LastOrDefault();
                    latest.RowVersion = Guid.NewGuid().ToString();
                    uow.CommitTransaction();
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }
            });

            Task.WaitAll(task1, task2);
        }
    }
}
