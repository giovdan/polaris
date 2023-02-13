namespace UnitTests.RepoDb
{
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using RepoDbVsEF.Data.Interfaces;
    using RepoDbVsEF.Domain;
    using RepoDbVsEF.Domain.Attributes;
    using RepoDbVsEF.Domain.Enums;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.RepoDb.Data.Interfaces;
    using RepoDbVsEF.RepoDb.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using UnitTests.Models;

    public class ParentEntityUnitTest: BaseUnitTest
    {
        private Dictionary<EntityTypeEnum, IEnumerable<AttributeDefinition>> AttributeDefinitionsDictionary { get; set; }
        #region < Private Methods >
        private void AddChildrenLinks(ulong id, IEnumerable<ulong> childrenIds, IServiceScope scope
                                                      , IUnitOfWork<IRepoDbDatabaseContext> unitOfWork
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

        private IEnumerable<Entity> CreateEntities(IEnumerable<EntityToCreate> entities, IServiceScope scope
                                                    , IUnitOfWork<IRepoDbDatabaseContext> unitOfWork
                                                    , bool withBatch = true)
        {
            foreach (var entity in entities)
            {
                yield return CreateEntity(entity, scope, unitOfWork, withBatch);
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
                yield return GenerateEntity(entityTypeEnum, scope, unitOfWork);
            }
        }

        private EntityToCreate GenerateEntity(EntityTypeEnum entityTypeEnum, IServiceScope scope
                                                    , IUnitOfWork<IRepoDbDatabaseContext> unitOfWork)
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
                AttributeDefinitionsDictionary.Add(entityTypeEnum, attributeDefinitions);
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
                                    , IServiceScope scope
                                    , IUnitOfWork<IRepoDbDatabaseContext> unitOfWork)
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
                yield return GenerateEntity(entityTypeEnum, scope, unitOfWork);
            }


        }

        
        /// <summary>
        /// Create single entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="scope"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        private Entity CreateEntity(EntityToCreate entity, IServiceScope scope, IUnitOfWork<IRepoDbDatabaseContext> unitOfWork
                            , bool withBatch = true)
        {
            var repository = scope.ServiceProvider.GetRequiredService<IRepoDbEntityRepository>();
            var attributeRepository = scope.ServiceProvider.GetRequiredService<IRepoDbAttributeValueRepository>();
            var dbEntity = Mapper.Map<Entity>(entity);

            try
            {
                repository.Attach(unitOfWork);
                attributeRepository.Attach(unitOfWork);
                dbEntity.SetAuditableFields(unitOfWork.UserSession?.Username ?? "MITROL");

                dbEntity = repository.Add(dbEntity);

                var attributes = entity.Attributes.Select(a => {
                    var dbAttribute = Mapper.Map<AttributeValue>(a);
                    dbAttribute.EntityId = dbEntity.Id;
                    dbAttribute.Entity = dbEntity;
                    dbAttribute.SetAuditableFields(unitOfWork.UserSession?.Username ?? "MITROL");
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
        #endregion

        [OneTimeSetUp()]
        public void Setup()
        {
            AttributeDefinitionsDictionary = new Dictionary<EntityTypeEnum, IEnumerable<AttributeDefinition>>();
            var services = new ServiceCollection();
            //services.AddScoped<IRepoDbEntityRepository, RepoDbEntityRepository>();
            services.AddScoped<IRepoDbEntityRepository>(service => new RepoDbEntityRepository(ConnectionString));
            services.AddScoped<IRepoDbAttributeDefinitionRepository, RepoDbAttributeDefinitionRepository>(service => new RepoDbAttributeDefinitionRepository(ConnectionString));
            services.AddScoped<IRepoDbAttributeValueRepository, RepoDbAttributeValueRepository>(service => new RepoDbAttributeValueRepository(ConnectionString));
            services.AddScoped<IChildLinkRepository, RepoDbChildLinkRepository>(service => new RepoDbChildLinkRepository(ConnectionString));
            base.Setup(services);
        }

        [OneTimeTearDown()]
        public void EndTest()
        {
            Trace.Flush();
        }

        [Test]
        [Order(1)]
        [TestCase(EntityTypeEnum.Order)]
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
                    CreateEntity(GenerateEntity(entityType, scope, uow), scope, uow);
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

        [Order(2)]
        [Test()]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(5000)]
        [TestCase(10000)]
        public void InsertMultipleEntitiesReturnsOk(int recordsNumber)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            if (recordsNumber == 0)
            {
                Assert.Pass();
            }

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
                        rowsNumber += CreateEntity(entity, scope, uow).Id > 0 ? 1: 0;
                    }

                    uow.CommitTransaction();
                    sw.Stop();
                    Trace.WriteLine($"EF Core - Batch Insert {recordsNumber} entities - Elapsed time: {sw.ElapsedMilliseconds}");
                    AttributeDefinitionsDictionary.Clear();
                    Assert.IsTrue(rowsNumber == recordsNumber);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }
            }



        }

        [Order(3)]
        [Test()]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(5000)]
        [TestCase(10000)]
        [TestCase(20000)]
        public void BulkInsertMultipleEntitiesReturnsOk(int recordsNumber)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            if (recordsNumber == 0)
            {
                Assert.Pass();
            }

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
                        rowsNumber += CreateEntity(entity, scope, uow, withBatch: false).Id > 0 ? 1: 0;
                    }

                    uow.CommitTransaction();
                    sw.Stop();
                    Trace.WriteLine($"EF Core - Bulk Insert {recordsNumber} entities - Elapsed time: {sw.ElapsedMilliseconds}");
                    AttributeDefinitionsDictionary.Clear();
                    Assert.IsTrue(rowsNumber == recordsNumber);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }
            }

        }

        [Test()]
        [Order(4)]
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

                    Assert.IsTrue(latest != null);

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
                    Assert.IsTrue(latest.RowVersion != oldRowVersion);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }


            }


        }

        [Test()]
        [Order(5)]
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

                    Assert.IsTrue(latest != null);

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

                    
                    Assert.IsTrue(latest.RowVersion != oldRowVersion);
                }
                catch (Exception)
                {
                    uow.RollBackTransaction();
                    throw;
                }


            }

           
        }

        [Test()]
        [Order(6)]
        [TestCase(5)]
        [TestCase(100)]
        [TestCase(500)]
        [TestCase(1000)]
        public void CreateEntityWithChildrenReturnsSomething(int childrenCount)
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
                    entities.Add(GenerateEntity(EntityTypeEnum.Order, scope, uow));
                    entities.AddRange(GenerateEntities(childrenCount, scope, uow, EntityTypeEnum.OrderRow));
                    var dbEntities = CreateEntities(entities, scope, uow).ToList();
                    uow.Commit();
                    var parentId = dbEntities.SingleOrDefault(entity => entity.EntityTypeId == EntityTypeEnum.Order)?.Id ?? 0;
                    if (parentId > 0)
                    {
                        AddChildrenLinks(parentId
                                            , dbEntities.Where(entity => entity.EntityTypeId == EntityTypeEnum.OrderRow)
                                                .Select(child => child.Id), scope, uow);
                        uow.Commit();
                        uow.CommitTransaction();
                    }
                    Assert.IsTrue(parentId > 0);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }
            }
        }

        [Test()]
        [Order(7)]
        [TestCase(5)]
        [TestCase(100)]
        [TestCase(500)]
        [TestCase(1000)]
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
                    entities.Add(GenerateEntity(EntityTypeEnum.Order, scope, uow));
                    entities.AddRange(GenerateEntities(childrenCount, scope, uow, EntityTypeEnum.OrderRow));
                    var dbEntities = CreateEntities(entities, scope, uow, withBatch: false).ToList();
                    uow.Commit();
                    var parentId = dbEntities.SingleOrDefault(entity => entity.EntityTypeId == EntityTypeEnum.Order)?.Id ?? 0;
                    if (parentId > 0)
                    {
                        AddChildrenLinks(parentId
                                            , dbEntities.Where(entity => entity.EntityTypeId == EntityTypeEnum.OrderRow)
                                                .Select(child => child.Id), scope, uow);
                        uow.Commit();
                        uow.CommitTransaction();
                    }
                    Assert.IsTrue(parentId > 0);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }
            }

        }

        [Test()]
        [Order(8)]
        public void GetEntitiesReturnsSomething()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IRepoDbEntityRepository>();
            var entities = service.GetAll();

            Assert.IsTrue(entities.Any());

        }
    }
}
