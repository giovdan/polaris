namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Bus.Events;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Production.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Application.Models.Production;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class StockService: MachineManagementBaseService, IStockService
    {
        private IMachineConfigurationService MachineConfigurationService 
                    => ServiceFactory.GetService<IMachineConfigurationService>();

        private IQuantityBackLogRepository QuantityBackLogRepository =>
                    ServiceFactory.GetService<IQuantityBackLogRepository>();

        #region < Private Methods >
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

        public StockService(IServiceFactory serviceFactory) : base(serviceFactory)  { }

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

        public Dictionary<string, string> GetFilteredMaterialCodes()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetFilteredProfileCodes(ProfileTypeEnum profileTypeId)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetFilteredThickness(MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem)
        {
            throw new NotImplementedException();
        }

        public Result<long> CreateStockItem(StockItemToAdd stockItem)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Result RemoveStockItem(long id)
        {
            throw new NotImplementedException();
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
