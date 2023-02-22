namespace XUnitTests.EFCore
{
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using RepoDbVsEF.Application.Interfaces;
    using RepoDbVsEF.Application.Models;
    using RepoDbVsEF.Application.Services;
    using RepoDbVsEF.Domain;
    using RepoDbVsEF.Domain.Attributes;
    using RepoDbVsEF.Domain.Enums;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.Domain.Threading;
    using RepoDbVsEF.EF.Data.Interfaces;
    using RepoDbVsEF.EF.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("TestType", "Entity")]
    public class EntityTest: BaseUnitTest
    {
        private Dictionary<EntityTypeEnum, IEnumerable<AttributeDefinition>> AttributeDefinitionsDictionary { get; set; }
        protected CancellationTokenSource CancellationTokenSource { get; private set; }

        #region < Private Methods >
        private Entity GenerateEntity(EntityTypeEnum entityTypeEnum, IServiceScope scope)
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

            return new Entity
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
        private IEnumerable<Entity> GenerateEntities(int recordsNumber
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
        

        #endregion

        public EntityTest(): base()
        {
            AttributeDefinitionsDictionary = new Dictionary<EntityTypeEnum, IEnumerable<AttributeDefinition>>();
            CancellationTokenSource = new CancellationTokenSource();
            var services = new ServiceCollection();
            services.AddScoped<IEntityService, EntityService>();
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
            var service = ServiceProvider.GetRequiredService<IEntityService>();
            service.SetSession(NullUserSession.InternalSessionInstance);
            var entities = service.GetAll();
            entities.Should().NotBeNullOrEmpty();
        }

        [Theory()]
        [InlineData(EntityTypeEnum.Customer)]
        public void CreateEntityReturnsOk(EntityTypeEnum entityType)
        {
            var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IEntityService>();
            service.SetSession(NullUserSession.InternalSessionInstance);
            var result = service.Create(GenerateEntity(entityType, scope));

            result.Success.Should().Be(true);
            result.Value.EntityType.Should().Be(EntityTypeEnum.Customer);
        }



        [Theory, MemberData(nameof(GetRecordCounts))]
        public void BulkCreateEntityWithChildrenReturnsSomething(int childrenCount)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var entities = new List<Entity>();
            entities.Add(GenerateEntity(EntityTypeEnum.Order, scope));
            entities.AddRange(GenerateEntities(childrenCount, scope, EntityTypeEnum.OrderRow));
            var service = scope.ServiceProvider.GetRequiredService<IEntityService>();
            var result = service.BulkCreate(entities);

            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNullOrEmpty();
        }

        [Theory, MemberData(nameof(GetRecordCounts))]
        public void BulkInsertMultipleEntitiesReturnsOk(int recordsNumber)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            if (recordsNumber == 0)
            {
                Assert.True(true);
                return;
            }
                

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var entities = GenerateEntities(recordsNumber, scope).ToHashSet();
            var service = scope.ServiceProvider.GetRequiredService<IEntityService>();
            var result = service.BulkCreate(entities);
            result.Success.Should().BeTrue();
            result.Value.Should().NotBeNullOrEmpty();
        }

        [Theory, MemberData(nameof(GetRecordCounts))]
        public void CreateEntityWithChildrenReturnsSomething(int childrenCount)
        {
            if (childrenCount == 0)
            {
                Assert.True(true);
                return;
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var entityWithChildren = new EntityWithChildren(GenerateEntity(EntityTypeEnum.Order, scope)
                            ,GenerateEntities(childrenCount, scope, EntityTypeEnum.OrderRow));

            var service = scope.ServiceProvider.GetRequiredService<IEntityService>();
            var result = service.CreateEntityWithChildren(entityWithChildren);
            result.Success.Should().BeTrue();
            result.Value.Id.Should().BeGreaterThan(0);
            result.Value.Children.Should().NotBeNullOrEmpty();
        }


        [Fact()]
        public void UpdateEntityReturnsOk()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using (var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IEFDatabaseContext>>())
            {
                using var uow = factory.GetOrCreate(NullUserSession.Instance);
                uow.BeginTransaction();
                try
                {
                    var repository = scope.ServiceProvider.GetRequiredService<IEFEntityRepository>();
                    var attributeRepository = scope.ServiceProvider.GetRequiredService<IEFAttributeValueRepository>();

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
                    latest = repository.Get(latest.Id);
                    uow.CommitTransaction();
                    latest.RowVersion.Should().NotBe(oldRowVersion);
                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    Assert.Fail(ex.InnerException?.Message ?? ex.Message);
                }
            }
        }

        [Fact()]
        public void ParallelEntityCreationDoesNotFailsForConcurrency()
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                using var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IEFDatabaseContext>>()
                            .GetOrCreate(NullUserSession.Instance);
                var service = scope.ServiceProvider.GetRequiredService<IEntityService>();
                service.Create(GenerateEntity(EntityTypeEnum.Customer, scope));
                uow.Commit();
            });

            var task2 = Task.Factory.StartNew(() =>
            {
                using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                using var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IEFDatabaseContext>>()
                                                    .GetOrCreate(NullUserSession.Instance);
                var service = scope.ServiceProvider.GetRequiredService<IEntityService>();
                service.Create(GenerateEntity(EntityTypeEnum.Customer, scope));
                uow.Commit();
            });

            Task.WaitAll(task1, task2);
        }

        [Fact()]
        public void ConcurrentOperationsDoesntFail()
        {
            var task1 = TimedTaskFactory.Start(() =>
                {
                    var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<IEntityService>();
                    service.Create(GenerateEntity(EntityTypeEnum.Customer, scope));
                }
                , intervalInMilliseconds: 500
                , synchronous: true
                , cancelToken: CancellationTokenSource.Token);

            // Creazione task di esecuzione 'medio'
            var task2 = TimedTaskFactory.Start(() =>
            {
                var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
                //UpdateEntity(scope);
                var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IEFDatabaseContext>>()
                        .GetOrCreate(NullUserSession.Instance);
                var repository = scope.ServiceProvider.GetRequiredService<IEFEntityRepository>();
                repository.Attach(uow);
                repository.GetAll();
            }
            , intervalInMilliseconds: 100
            , synchronous: true
            , cancelToken: CancellationTokenSource.Token);

            Task.WaitAll(task1, task2);
        }
        [Fact()]
        public void RawUpdateEntityReturnsOk()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            using var scope = ServiceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var factory = scope.ServiceProvider.GetRequiredService<IUnitOfWorkFactory<IEFDatabaseContext>>();
            using var uow = factory.GetOrCreate(NullUserSession.Instance);
            uow.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
            try
            {
                var repository = scope.ServiceProvider.GetRequiredService<IEFEntityRepository>();
                var attributeRepository = scope.ServiceProvider.GetRequiredService<IEFAttributeValueRepository>();

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
                    })
                    .ToList();

                var affectedRows = attributeRepository.BulkUpdate(attributes);
                uow.CommitTransaction();

                attributes.Should().NotBeNullOrEmpty();
                attributes.Should().HaveCount(affectedRows);
            }
            catch (Exception ex)
            {
                uow.RollBackTransaction();
                Assert.Fail(ex.InnerException?.Message ?? ex.Message);
            }
        }


    }
}
