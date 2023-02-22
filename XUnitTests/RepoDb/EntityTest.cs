namespace XUnitTests.RepoDb
{
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using RepoDbVsEF.Application.Models;
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

    public class EntityTest: BaseUnitTest
    {
        private CancellationTokenSource CancellationTokenSource;
        private Dictionary<EntityTypeEnum, IEnumerable<AttributeDefinition>> AttributeDefinitionsDictionary { get; set; }

        #region < Private Methods >
        private void AddChildrenLinks(long id, IEnumerable<long> childrenIds, IServiceScope scope
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


        /// <summary>
        /// Creazione entità
        /// </summary>
        /// <param name="recordsNumber"></param>
        /// <param name="entityType"></param>
        /// <param name="attributeDefinitionRepository"></param>
        /// <returns></returns>
        private IEnumerable<Entity> GenerateEntities(int recordsNumber
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

        private Entity GenerateEntity(EntityTypeEnum entityTypeEnum, IServiceScope scope)
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

            return new Entity
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
        private IEnumerable<Entity> GenerateEntities(int recordsNumber
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
                var sw = new Stopwatch();
                sw.Start();

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
              
            }

        }

        [Fact]
        public void UpdateEntityReturnsOk()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
            {
              
              
            }


        }

        [Fact]
        public void RawUpdateEntityReturnsOk()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
            {
              

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
            }
        }

        [Theory, MemberData(nameof(GetRecordCounts))]
        public void BulkCreateEntityWithChildrenReturnsSomething(int childrenCount)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>())
            {
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
                    // InnerCreateEntity(EntityTypeEnum.Product);
                }
                , intervalInMilliseconds: 500
                , synchronous: true
                , cancelToken: CancellationTokenSource.Token);

            // Creazione task di esecuzione 'medio'
            var task2 = TimedTaskFactory.Start(() =>
            {
                var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                ////UpdateEntity(scope);
                //var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IRepoDbDatabaseContext>>()
                //        .GetOrCreate(NullUserSession.Instance);
                //var repository = scope.ServiceProvider.GetRequiredService<IRepoDbEntityRepository>();
                //repository.Attach(uow);
                //repository.GetAll();
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
                
            });

            var task2 = Task.Factory.StartNew(() =>
            {
                using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                
            });

            Task.WaitAll(task1, task2);
        }
    }
}
