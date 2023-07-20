namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Bus.Events;
    using Mitrol.Framework.Domain.Conversions;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Production.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Application.Models.General;
    using Mitrol.Framework.MachineManagement.Application.Models.Production;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public partial class StockService: MachineManagementBaseService, IStockService
    {
        private IMachineConfigurationService MachineConfigurationService 
                    => ServiceFactory.GetService<IMachineConfigurationService>();

        private IQuantityBackLogRepository QuantityBackLogRepository =>
                    ServiceFactory.GetService<IQuantityBackLogRepository>();

        private IEntityValidator<StockItemToAdd, IMachineManagentDatabaseContext> StockToAddValidator
                => ServiceFactory.GetService<IEntityValidator<StockItemToAdd, IMachineManagentDatabaseContext>>();

        #region < Private Methods >
        /// <summary>
        /// Serve a formattare il dato nemrico per presentarlo a video arrotondato con due decimali (nel dettaglio non viene arrotondato ma presentato così come si presenta)
        /// L'arrotondamento viene usato perchè altrimenti la visualizzazione risulterebbe confusa con numeri con un Numero di decimali non determinati.
        /// </summary>
        /// <param name="currentValue"></param>
        /// <returns></returns>
        private string FormatValue(object currentValue)
        {
            string value = currentValue?.ToString() ?? string.Empty;
            //provo a convertire il valore per verificare che effettivamente si tratta di un numero.
            if (decimal.TryParse(value, out var decimalValue))
            {
                //se il numero troncato è uguale al numero convertito significa che non ha decimali e quindi va presentato senza decimali
                if (Math.Truncate(decimalValue) == decimalValue)
                    value = decimalValue.ToString();
                else
                    //altrimenti verrà presentato con due decimali.
                    value = decimalValue.ToString("F2");
            }

            return value;
        }

        /// <summary>
        /// Add Stock Entity master record
        /// </summary>
        /// <param name="stockItem"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        private Entity AddMasterRecord(StockItemToAdd stockItem
                    , IUnitOfWork<IMachineManagentDatabaseContext> uow)
        {
            var entity = Mapper.Map<Entity>(stockItem);
            entity.EntityTypeId = ParentTypeEnum.StockItem.GetEntityType(stockItem.ProfileTypeId);
            // Calcolo HashCode
            entity.HashCode = CalculateHashCode(entity.EntityTypeId
                                    , stockItem.Attributes.ToDictionary(a => a.Key.ToString(), a => a.Value.ToString())
                                    , conversionSystemFrom: UserSession.ConversionSystem
                                    , attributeLinkDefinitionRepository: AttributeDefinitionLinkRepository
                                    , unitOfWork: uow);
            // Recupero la lista degli identificatori
            var identifierDefs =
                    AttributeDefinitionLinkRepository.FindBy(a => a.EntityTypeId == entity.EntityTypeId
                                    && a.GroupId == AttributeDefinitionGroupEnum.Identifiers)
                    .Select(a => Enum.Parse<DatabaseDisplayNameEnum>(a.AttributeDefinition.DisplayName));

            var identifiers = stockItem.Attributes
                        .Where(a => identifierDefs.Contains(a.Key))
                        .ToDictionary(a => a.Key.ToString(), a => a.Value.ToString());
            entity.DisplayName = CalculateDisplayName(entity.EntityTypeId, identifiers
                                                        , conversionSystemFrom: UserSession.ConversionSystem);
            // Inserisco l'entità stock 
            entity = EntityRepository.Add(entity);
            uow.Commit();
            return entity;
        }

        private StockListItem ApplyCustomMapping(StockListItem stockItem
                                        , MeasurementSystemEnum measurementSystemFrom
                                        , MeasurementSystemEnum measurementSystemTo
                                        , HashSet<EntityAttribute> entityAttributes
                                        , QuantityBackLog quantityBackLog)
        {
            var protectionLevels = UserSession.GetProtectionLevels();
            var mappedStockItem = ApplyBaseCustomMapping<StockListItem>(stockItem
                            , measurementSystemTo, protectionLevels, entityAttributes);

            //Recupero il tipo di stock dalla lista degli attributi
            mappedStockItem.StockType = StockTypeEnum.Stock;
            var stockTypeAttribute = entityAttributes
                        .SingleOrDefault(a => a.EnumId
                            == AttributeDefinitionEnum.StockItemType);

            if (stockTypeAttribute != null)
                mappedStockItem.StockType = (StockTypeEnum)stockTypeAttribute.Value;

            if (quantityBackLog != null)
            {
                mappedStockItem.TotalQuantity = quantityBackLog.TotalQuantity;
                mappedStockItem.ExecutedQuantity = quantityBackLog.ExecutedQuantity;
            }

            return mappedStockItem;

        }

        private T ApplyBaseCustomMapping<T>(BaseStockItem baseStockItem, MeasurementSystemEnum conversionSystem
                , IEnumerable<ProtectionLevelEnum> protectionLevels, HashSet<EntityAttribute> attributes)
                where T : BaseStockItem
        {
            var suggestedFormatAttribute =
                DomainExtensions.GetEnumAttribute<ProfileTypeEnum, SuggestedFormatAttribute>
                (baseStockItem.ProfileType);
            baseStockItem.SuggestedFormat = suggestedFormatAttribute?.SuggestedFormat ?? string.Empty;

            baseStockItem.Identifiers = attributes.Where(a => a.AttributeDefinitionGroupId
                                    == AttributeDefinitionGroupEnum.Identifiers)
                .ToDictionary(a => a.DisplayName, a => a.FormattedValue());


            //Recupero il codice profilo se presente
            var profileCode = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.ProfileCode);
            if (profileCode != null)
            {
                baseStockItem.Identifiers.TryAdd(DatabaseDisplayNameEnum.ProfileCode.ToString(), profileCode.TextValue);
            }

            baseStockItem.UMLocalizationKey = $"{DomainExtensions.GENERIC_LABEL}_{DatabaseDisplayNameEnum.Length.ToString().ToUpper()}_{conversionSystem.ToString().ToUpper()}";

            //Recupero il codice materiale 
            var attributeMaterialcode = attributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MaterialCode);
            if (attributeMaterialcode != null)
            {
                baseStockItem.MaterialCode = attributeMaterialcode.TextValue;
            }

            return baseStockItem as T;
        }

        private BaseGroupItem<AttributeDetailItem> ApplyCustomMapping(long stockItemId
                                    , AttributeDefinitionGroupEnum group
                                    , HashSet<AttributeDetailItem> attributes)
        {

            // Applico un filtro per rendere read-only i seguenti campi del dettaglio dello stock.
            var readonlyAttributes = new[]
            {
                DatabaseDisplayNameEnum.ProfileCode.ToString(),
                DatabaseDisplayNameEnum.MaterialCode.ToString(),
                DatabaseDisplayNameEnum.Length.ToString(),
                DatabaseDisplayNameEnum.Width.ToString(),
                DatabaseDisplayNameEnum.Thickness.ToString(),
                DatabaseDisplayNameEnum.StockItemType.ToString(),
                DatabaseDisplayNameEnum.Supplier.ToString(),
                DatabaseDisplayNameEnum.HeatNumber.ToString(),
            };

            var groupItem = new BaseGroupItem<AttributeDetailItem>
            {
                Details = attributes.Select(attribute =>
                {
                    if (readonlyAttributes.Contains(attribute.DisplayName))
                        attribute.IsReadonly = true;

                    return attribute;
                }),
                Priority = (short)group,
            };

            //Aggiungi quantità se il group è StockOthers
            if (group == AttributeDefinitionGroupEnum.Others)
            {
                var backLog = QuantityBackLogRepository.FindBy(x => x.EntityId == stockItemId)
                                                            .SingleOrDefault();

                var quantity = backLog != null ? backLog.TotalQuantity : 0;

                groupItem.Details = groupItem.Details.Append(
                    new AttributeDetailItem
                    {
                        LocalizationKey = $"{MachineManagementExtensions.LABEL_ATTRIBUTE}_{DatabaseDisplayNameEnum.Quantity.ToString().ToUpper()}",
                        UMLocalizationKey = null,
                        DisplayName = DatabaseDisplayNameEnum.Quantity.ToString(),
                        AttributeKind = AttributeKindEnum.Number,
                        ControlType = ClientControlTypeEnum.Edit,
                        ItemDataFormat = AttributeDataFormatEnum.AsIs,
                        ProtectionLevel = ProtectionLevelEnum.Medium,
                        Order = 999,
                        Value = new AttributeValueItem
                        {
                            ValueType = ValueTypeEnum.Flat,
                            Source = null,
                            CurrentValueId = 0,
                            CurrentValue = quantity
                        }
                    });
            }

            return groupItem;
        }
        #endregion

        #region < Boot >
        public Result Boot(IUserSession userSession)
        {
            return CleanUpBeforeBoot(userSession);
        }

        public Result CleanUpBeforeBoot(IUserSession userSession)
        {
            var result = Result.Ok();

            var cleanupLimit = MachineConfigurationService.ConfigurationRoot.CleanUp.StocksLimit;

            if (cleanupLimit > 0)
            {
                EventHubClient.ProgressEvent(new ProgressEvent("Stocks Cleanup started"));

                using var uow = UnitOfWorkFactory.GetOrCreate(userSession);

                EntityRepository.Attach(uow);
                result = EntityRepository.BulkRemove(entity =>
                            entity.IsLinked == 0
                            && entity.TotalQuantity <= entity.ScheduledQuantity
                            && entity.CreatedOn <= DateTime.UtcNow.AddDays(-cleanupLimit));
            }

            return result
                .OnFailure(result =>
                {
                    EventLogHubClient.WriteLogEvent(new WriteLogEvent
                    {
                        EventType = EventTypeEnum.Error,
                        EventContext = EventContextEnum.Boot,
                        MachineName = userSession.MachineName,
                        LoggedUser = userSession.Username,
                        SessionId = userSession.SessionId,
                        Message = $"Stocks Cleanup failed {result.Errors.ToErrorString()}",
                        Method = nameof(CleanUpBeforeBoot)
                    });
                });
        }
        #endregion

        public StockService(IServiceFactory serviceFactory) : base(serviceFactory) { }

        public IEnumerable<AttributeDetailItem> GetAttributeDefinitions(EntityTypeEnum entityType
            , MeasurementSystemEnum measurementSystemFrom
            , MeasurementSystemEnum measurementSystemTo)
        {
            var attributeDetails = base.GetAttributeDefinitions(entityType, measurementSystemFrom, measurementSystemTo);

            attributeDetails = attributeDetails.Append(new AttributeDetailItem
            {
                AttributeDefinitionLinkId = 0,
                GroupId = AttributeDefinitionGroupEnum.Generic,
                AttributeKind = AttributeKindEnum.Number,
                AttributeScopeId = AttributeScopeEnum.Optional,
                ItemDataFormat = AttributeDataFormatEnum.AsIs,
                AttributeType = AttributeTypeEnum.Generic,
                DisplayName = DatabaseDisplayNameEnum.Quantity.ToString(),
                ControlType = ClientControlTypeEnum.Edit,
                ProtectionLevel = ProtectionLevelEnum.Normal,
                EnumId = AttributeDefinitionEnum.TotalQuantity,
                EntityType = entityType
            });

            return attributeDetails;
        }

        public Dictionary<string, string> GetFilteredMaterialCodes()
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            AttributeValueRepository.Attach(uow);
            var entitTypes = ParentTypeEnum.StockItem.GetEntityTypes();

            Expression<Func<EntityAttribute, bool>> predicate 
                            = p => p.EnumId == AttributeDefinitionEnum.MaterialCode
                                    && entitTypes.Contains(p.EntityTypeId);

            return AttributeValueRepository.FindEntityAttributes(predicate)
                                        .GroupBy(a => a.TextValue)
                                        .ToDictionary(a => a.Key, a => a.Key);
        }

        public Dictionary<string, string> GetFilteredProfileCodes(ProfileTypeEnum profileTypeId)
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            AttributeValueRepository.Attach(uow);

            var entityTypeId = ParentTypeEnum.StockItem.GetEntityType((long)profileTypeId);
            return AttributeValueRepository.FindEntityAttributes(p => p.EntityTypeId == entityTypeId
                                        && p.EnumId == AttributeDefinitionEnum.ProfileCode)
                                        .GroupBy(a => a.TextValue)
                                        .ToDictionary(a => a.Key, a => a.Key);
        }

        public Dictionary<string, string> GetFilteredThickness(MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem)
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            AttributeValueRepository.Attach(uow);
            var entitTypes = ParentTypeEnum.StockItem.GetEntityTypes();
            return AttributeValueRepository.FindEntityAttributes(av =>
                        av.EnumId == AttributeDefinitionEnum.Thickness
                        && entitTypes.Contains(av.EntityTypeId))
                        .GroupBy(av => ConvertToHelper.Convert(MeasurementSystemEnum.MetricSystem
                                                            , conversionSystem
                                                            , av.DataFormat
                                                            , av.Value.GetValueOrDefault()
                                                            , true).Value)
                        .ToDictionary(thickness =>
                                    FormatValue(thickness.Key), thickness => FormatValue(thickness.Key));
        }

        /// <summary>
        /// Add attributes for the stock
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="attributes"></param>
        /// <param name="conversionSystemFrom"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        private Result AddAttributes(long entityId
                    , EntityTypeEnum entityType
                    , Dictionary<DatabaseDisplayNameEnum, object> attributes
                    , MeasurementSystemEnum conversionSystemFrom
                    , IUnitOfWork<IMachineManagentDatabaseContext> uow)
        {
            AttributeValueRepository.Attach(uow);

            // Recupero le definizioni degli attributi
            // trovo gli attributi per il profilo selezionato che sono di tipo Stock item
            var attributeValues = InnerGetAttributeDefinitions(entityType, conversionSystemFrom, MeasurementSystemEnum.MetricSystem, uow)
                .OrderBy(a => a.GroupId)
                .Select(attributeValue =>
                       CreateAttributeValue(entityId
                                        , attributeValue
                                        , attributes
                                        , conversionSystemFrom
                                        , MeasurementSystemEnum.MetricSystem)).ToList();

            AttributeValueRepository.BatchInsert(attributeValues);
            uow.Commit();
            return Result.Ok();
        }

        /// <summary>
        /// Create new StockItem
        /// </summary>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        public Result<long> CreateStockItem(StockItemToAdd stockItem)
        {
            long stockId = 0;
            using (var uow = UnitOfWorkFactory.GetOrCreate(UserSession))
            {
                try
                {
                    EntityRepository.Attach(uow);
                    AttributeDefinitionLinkRepository.Attach(uow);
                    uow.BeginTransaction();
                    var relatedEntityTypeId = ParentTypeEnum.StockItem.GetEntityType(stockItem.ProfileTypeId);
                    var relatedEntityTypes = EntityRepository.FindEntityTypes(et => (EntityTypeEnum)et.Id == relatedEntityTypeId
                                    && et.IsManaged);

                    // Controlla se il tipo di entità da inserire è stato configurato
                    if (!relatedEntityTypes.Any())
                    {
                        return Result.Fail<long>(ErrorCodesEnum.ERR_STK001.ToString());
                    }

                    // Validazione stock
                    #region < Stock Validation > 
                    StockToAddValidator.Attach(uow);
                    var validationResult = StockToAddValidator.Validate(stockItem);

                    if (validationResult.Errors?.Any() ?? false)
                    {
                        uow.RollBackTransaction();
                        return Result.Fail<long>(validationResult.Errors);
                    }
                    #endregion

                    ushort quantity = 1;
                    // Recupero la quantità
                    if (stockItem.Attributes.TryGetValue(DatabaseDisplayNameEnum.Quantity
                        , out var quantityAttribute)
                        &&
                        ushort.TryParse(quantityAttribute.ToString(), out quantity))
                    {

                    }

                    // Setto il default del tipo stock a "Stock" nel caso non sia stato specificato
                    if (!stockItem.Attributes.TryGetValue(DatabaseDisplayNameEnum.StockItemType
                            , out var stockType))
                    {
                        stockItem.Attributes.Add(DatabaseDisplayNameEnum.StockItemType
                                    , (int)StockTypeEnum.Stock);
                    }

                    Entity entity = AddMasterRecord(stockItem, uow);
                    if (entity.Id > 0)
                    {
                        return AddAttributes(entity.Id
                                , entity.EntityTypeId
                                , stockItem.Attributes
                                , UserSession.ConversionSystem
                                , uow)
                            .OnSuccess(() =>
                            {
                                // Aggiunta record quantitybacklog
                                return AddQuantityBackLog(new QuantityBackLogItem
                                {
                                    EntityId = entity.Id,
                                    TotalQuantity = quantity
                                }, uow);
                            })
                            .OnSuccess(() =>
                            {
                                uow.CommitTransaction();
                                return Result.Ok(entity.Id);
                            })
                            .OnFailure(errors => {
                                uow.RollBackTransaction();
                            });
                        
                    }
                    else
                    {
                        uow.RollBackTransaction();
                        return Result.Fail<long>(ErrorCodesEnum.ERR_GEN007.ToString());
                    }

                }
                catch (Exception ex)
                {
                    uow.RollBackTransaction();
                    return Result.Fail<long>(ex.InnerException?.Message ?? ex.Message);
                }

                return Result.Ok(stockId);
            }
        }

        public IEnumerable<StockListItem> GetStockItemList(MeasurementSystemEnum conversionSystemTo)
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            EntityRepository.Attach(uow);
            QuantityBackLogRepository.Attach(uow);
            AttributeValueRepository.Attach(uow);

            var entityTypes = ParentTypeEnum.StockItem.GetEntityTypes();
            //Recupero gli identificatori legati agli stock
            var attributeValues = AttributeValueRepository
                .FindEntityAttributes(attribute => attribute.ParentType == ParentTypeEnum.StockItem)
                    .ToLookup(attribute => attribute.EntityId);

            //Recupero le quantità legate agli stock
            var quantities = QuantityBackLogRepository.FindBy(backLog =>
                            entityTypes.Contains(backLog.Entity.EntityTypeId))
                            .ToLookup(qb => qb.EntityId);

            
            return Mapper.Map<IEnumerable<StockListItem>>(EntityRepository.FindBy(e => entityTypes.Contains(e.EntityTypeId)))
                    .ToList()
                    .Select(stock => ApplyCustomMapping(stock
                                    , MeasurementSystemEnum.MetricSystem
                                    , conversionSystemTo
                                    , attributeValues[stock.Id].ToHashSet()
                                    , quantities[stock.Id].SingleOrDefault()));
        }

        public Task<IEnumerable<StockListItem>> GetStockItemListAsync(MeasurementSystemEnum conversionSystem)
        {
            return Task.Factory.StartNew(() => GetStockItemList(conversionSystem));
        }

        public Result<StockDetailItem> GetStockItem(StockItemFilter filter)
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            EntityRepository.Attach(uow);
            AttributeValueRepository.Attach(uow);

            var dbEntity = EntityRepository.Get(filter.Id.GetValueOrDefault());

            if (dbEntity == null)
                return Result.Fail<StockDetailItem>(ErrorCodesEnum.ERR_GEN002.ToString());

            StockDetailItem stockItem = Mapper.Map<StockDetailItem>(dbEntity);

            //Se è una copia allora azzero l'id
            if (filter.IsCopy)
                stockItem.Id = 0;

            var stockAttributes = GetAttributes(stockItem.Id
                                        , dbEntity.EntityTypeId
                                        , onlyQuickAccess: false,
                                        conversionSystemTo: filter.ConversionSystemTo)
                                .ToHashSet();

            //Recupero gli attributi legati allo stockItem
            stockItem.Groups = stockAttributes
                            .ToLookup(a => a.GroupId)
                            .Select(group => ApplyCustomMapping(dbEntity.Id, group.Key, group.ToHashSet()))
                            .OrderBy(group => group.Priority);

            var materialCodeAttribute = stockAttributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MaterialCode);
            if (materialCodeAttribute != null && 
                int.TryParse(materialCodeAttribute.Value.CurrentValueId.ToString(), out var materialCodeId))
            {
                var materialTypeAttribute = AttributeValueRepository.FindEntityAttributes(a => a.EntityId == materialCodeId
                                && a.EnumId == AttributeDefinitionEnum.MaterialTypeAttribute)
                                .SingleOrDefault();

                if (materialTypeAttribute != null)
                {
                    stockItem.MaterialTypeId = (MaterialTypeEnum)materialTypeAttribute.Value;
                }
                
            }

            // Recupero gli attributi del profilo collegato in base al profileCodeId
            var profileCodeAttribute = stockAttributes
                                .SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.ProfileCode);
            
            if (profileCodeAttribute != null)
            {
                var protectionLevels = UserSession.GetProtectionLevels();
                stockItem.ProfileAttributes = AttributeValueRepository.FindEntityAttributes
                                                        (a => a.EntityId == profileCodeAttribute.Value.CurrentValueId)
                                            .Select(a => ApplyCustomMapping(a, protectionLevels, filter.ConversionSystemTo));
            }

            return Result.Ok(stockItem);
        }

        public Result<Entity> GetStockItemByHashCode(string hashCode)
        {
            if (string.IsNullOrEmpty(hashCode))
            {
                return Result.Fail<Entity>(ErrorCodesEnum.ERR_GEN001.ToString());
            }

            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            EntityRepository.Attach(uow);
            var dbEntity = EntityRepository.FindBy(e => e.HashCode == hashCode)
                            .SingleWhenOnlyOrDefault();
            return dbEntity != null ? Result.Ok(dbEntity): Result.Fail<Entity>(ErrorCodesEnum.ERR_GEN002.ToString());
        }

        public Result RemoveStockItem(long id)
        {
            // Controlla se è associato ad un programma
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            try
            {
                // Controllo se l'entità è in relazione con altre entità
                EntityLinkRepository.Attach(uow);
                EntityRepository.Attach(uow);
                QuantityBackLogRepository.Attach(uow);

                var dbEntity = EntityRepository.Get(id);

                var links = EntityLinkRepository.FindBy(e => e.EntityHashCode == dbEntity.HashCode
                            || e.RelatedEntityHashCode == dbEntity.HashCode);

                if (links.Any())
                {
                    return Result.Fail(ErrorCodesEnum.ERR_STK014.ToString());
                }

                uow.BeginTransaction();
                // Rimozione Quantità
                var result = QuantityBackLogRepository.Remove(qb => qb.EntityId == id);
                uow.Commit();
                // Rimozione
                result = Result.Combine(result, RemoveEntity(id, uow));
                uow.CommitTransaction();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                uow.RollBackTransaction();
                return Result.Fail(ex.InnerException?.Message ?? ex.Message);
            }
            
        }

        public Result RemoveStockItems(long[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LastUpdatedStockItem>> GetLastUpdatedStockItemsAsync(MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem)
        {
            throw new NotImplementedException();
        }

        public Result UpdateStockItem(StockItemToUpdate stockItem)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeDetailItem> GetStockItemIdentifiers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseGroupItem<AttributeDetailItem>>> GetStockAttributeDefinitionsAsync(MeasurementSystemEnum conversionSystem, ProfileTypeEnum profileTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<InfoItem<string>>> GetStockProfileTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseInfoItem<int, string>>> GetStockTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubFilterItem>> GetStockSubFiltersAsync(ProfileTypeFilters filters)
        {
            throw new NotImplementedException();
        }
    }
}
