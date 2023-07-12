namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Bus.Events;
    using Mitrol.Framework.Domain.Bus;
    using Mitrol.Framework.Domain.Configuration.Extensions;
    using System.Linq.Expressions;
    using Mitrol.Framework.Domain.Core.Extensions;

    public class ToolService : MachineManagementBaseService, IToolService
    {
        public ToolService(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

        private IDetailIdentifierRepository DetailIdentifierRepository => ServiceFactory.GetService<IDetailIdentifierRepository>();
        private IMachineConfigurationService MachineConfigurationService => ServiceFactory.GetService<IMachineConfigurationService>();

        private IEntityValidator<ToolDetailItem> ToolValidator
                => ServiceFactory.GetService<IEntityValidator<ToolDetailItem>>();


        #region < Private Methods >
        private Result UpdateAttributes(IEnumerable<AttributeDetailItem> attributeDetailItems, 
                            IUnitOfWork<IMachineManagentDatabaseContext> unitOfWork = null)
        {

            var uow = unitOfWork ?? UnitOfWorkFactory.GetOrCreate(UserSession);
            try
            {
                AttributeValueRepository.Attach(uow);

                // Non considero gli attributi Fake
                attributeDetailItems = attributeDetailItems.Where(a => !a.IsFake);

                // Creo il dizionario per accedere più rapidamente ai singoli AttributeDetailItem
                var attributeDetailItemsDictionary = attributeDetailItems
                            .ToDictionary(attributeDetailItem => attributeDetailItem.Id);

                // Preparo la lista degli AttributeValueId
                var attributeValueIds = attributeDetailItems.Select(attributeDetailItem => attributeDetailItem.Id).ToHashSet();

                // Filtro gli AttributeDetailItem con override
                var attributeDatailItemsWithOverride = attributeDetailItems
                    .Where(adi => adi.Value.CurrentOverrideValue.OverrideType != OverrideTypeEnum.None);

                // Recupero gli ID degli attributi con override
                var attributeDatailItemsWithOverrideIds = attributeDatailItemsWithOverride
                    .Select(a => a.Id).ToHashSet();

                // Recupero gli attributi già presenti nel database
                var existingAttributeValues = AttributeValueRepository
                    .FindBy(attributeValue => attributeValueIds.Contains(attributeValue.Id))
                    .Select(attributeValue => UpdateAttributeValueFromAttributeDetailItem(attributeDetailItemsDictionary[attributeValue.Id],
                                                                                            UserSession.ConversionSystem,
                                                                                            attributeValue));

                // Recupero gli override, associati agli attributi, già presenti nel database
                var existingAttributeOverrideValues = AttributeValueRepository
                    .FindAttributeOverridesBy(aov => attributeDatailItemsWithOverrideIds.Contains(aov.AttributeValueId))
                    .Select(attributeOverrideValue => UpdateAttributeOverrideValueFromAttributeDetailItem(attributeDetailItemsDictionary[attributeOverrideValue.AttributeValueId],
                                                                                                            UserSession.ConversionSystem,
                                                                                                            attributeOverrideValue));
                // Preparo la lista degli AttributeValueId per gli overrideValues esistenti
                var existingAttributeOverrideValueAttributeIds = existingAttributeOverrideValues.Select(attributeOverrideValue => attributeOverrideValue.AttributeValueId);

                // Creo gli override non ancora presenti nel database
                var attributeOverrideValuesToCreate = attributeDatailItemsWithOverride
                    .Where(attr => !existingAttributeOverrideValueAttributeIds.Contains(attr.Id))
                    .Select(attributeDetailItem => 
                                    UpdateAttributeOverrideValueFromAttributeDetailItem(attributeDetailItem,
                                                                            UserSession.ConversionSystem,
                                                                            new AttributeOverrideValue
                                                                            {
                                                                                AttributeValueId = attributeDetailItem.Id,
                                                                            }));

                // Aggiornamento degli attributi esistenti
                AttributeValueRepository.BatchUpdate(existingAttributeValues);
                // Aggiornamento degli override esistenti
                AttributeValueRepository.BatchUpdateOverrides(existingAttributeOverrideValues);
                // Creazione degli override non ancora creati
                AttributeValueRepository.BatchInsertOverrides(attributeOverrideValuesToCreate);
                uow.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result.Ok();
        }

        private Result InnerToolUpdate(Entity dbEntity, ToolDetailItem tool)
        {
            try
            {
                using (var uow = UnitOfWorkFactory.GetOrCreate(UserSession))
                {
                    EntityRepository.Attach(uow);

                    uow.BeginTransaction();
                    UpdateAttributes(tool.Attributes, uow);
                    dbEntity.SecondaryKey = tool.Id;
                    EntityRepository.Update(dbEntity);
                    uow.Commit();
                    uow.CommitTransaction();
                }

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.InnerException?.Message ?? ex.Message);
            }
            
        }

        private IEnumerable<AttributeDefinitionEnum> GetEnabledUnits(ToolTypeEnum toolType)
        {
            var plantUnit = toolType.GetEnumAttribute<PlantUnitAttribute>()?.PlantUnit ?? PlantUnitEnum.None;
            IEnumerable<AttributeDefinitionEnum> enabledUnits = new List<AttributeDefinitionEnum>();

            switch (plantUnit)
            {
                case PlantUnitEnum.DrillingMachine:
                    {
                        enabledUnits = new List<AttributeDefinitionEnum>
                           { AttributeDefinitionEnum.ToolEnableA, AttributeDefinitionEnum.ToolEnableB,
                            AttributeDefinitionEnum.ToolEnableC, AttributeDefinitionEnum.ToolEnableD };
                    }
                    break;
                case PlantUnitEnum.PlasmaTorch:
                case PlantUnitEnum.OxyCutTorch:
                    {
                        enabledUnits = new List<AttributeDefinitionEnum>
                        { AttributeDefinitionEnum.ToolEnableC, AttributeDefinitionEnum.ToolEnableD };
                    }

                    break;
            }

            return enabledUnits;
        }

        /// <summary>
        /// Recupero Prima posizione libera
        /// </summary>
        /// <returns></returns>
        private int GetFirstAvailablePosition(IUnitOfWork<IMachineManagentDatabaseContext> unitOfWork)
        {
            EntityRepository.Attach(unitOfWork);
            var toolTypes = EntityTypeExtensions.GetToolEntityTypes();

            //Recupera la lista dei toolManagementId
            var toolManagementIds = EntityRepository.FindBy(e => toolTypes.Contains(e.EntityTypeId))
                        .OrderBy(e => e.SecondaryKey)
                        .Select(e => e.SecondaryKey);

            //Se non c'è nessun Tool ritorna 1
            if (!toolManagementIds.Any())
                return 1;

            //Ciclo su tutte le posizioni disponibili fra 1 e maxToolManagementId 
            //per recuperare la prima posizione libera
            var maxToolManagementId = toolManagementIds.Last();
            int position = 0;
            for (position = 1; position <= maxToolManagementId; position++)
            {

                if (!toolManagementIds.Contains(position))
                    break;
            }
            return position == 0 ? (int)maxToolManagementId + 1 : position;
        }

        /// <summary>
        /// Get Real Processing Technology based on toolType
        /// </summary>
        /// <param name="processingTechnology"></param>
        /// <param name="toolType"></param>
        /// <returns></returns>
        private ProcessingTechnologyEnum GetRealProcessTechnology(ProcessingTechnologyEnum processingTechnology
                        , ToolTypeEnum toolType)
        {
            var plantUnit = toolType.GetEnumAttribute<PlantUnitAttribute>().PlantUnit;
            if (processingTechnology != ProcessingTechnologyEnum.Default)
            {
                var plantUnitToApply = processingTechnology.GetEnumAttribute<TechnologyRelatedToAttribute>();
                //Se l'attributo RelatedToAttribute è All oppure il tipo di plant unit non è collegato al ToolType
                //la tecnologia di elaborazione non può essere associata al tool
                if (plantUnitToApply.PlantUnit == PlantUnitEnum.All ||
                    (plantUnitToApply.PlantUnit != PlantUnitEnum.All &&
                        plantUnit != plantUnitToApply.PlantUnit))
                    processingTechnology = ProcessingTechnologyEnum.Default;
            }

            return processingTechnology;
        }

        private ToolListItem ApplyCustomMapping(Entity entity
                                , IEnumerable<IdentifierDetailItem> identifiers
                                , IEnumerable<CodeGeneratorItem> codeGenerators
                                , IEnumerable<AttributeDetailItem> attributes
                                , MeasurementSystemEnum conversionSystem
                                , IUnitOfWork<IMachineManagentDatabaseContext> unitOfWork = null)
        {
            var uow = unitOfWork ?? UnitOfWorkFactory.GetOrCreate(UserSession);

            var toolListItem = Mapper.Map<ToolListItem>(entity);
            toolListItem.Identifiers = identifiers;
            toolListItem.CodeGenerators = codeGenerators;
            toolListItem.InnerId = entity.Id;

            // Validazione tool
            ToolValidator.Init(ServiceFactory, new Dictionary<DatabaseDisplayNameEnum, object>()
                                { { DatabaseDisplayNameEnum.TS, entity.EntityTypeId.ToToolType() } });

            // Recupero gli attributi collegati allo stato
            IEnumerable<EntityStatusAttribute> toolStatusAttributes =
                    Mapper.ProjectTo<EntityStatusAttribute>(
                            attributes.Where(a => a.IsStatusAttribute).AsQueryable())
                    .ToHashSet();

            ToolValidator.ValidateAttributes(attributes)
                    .OnSuccess(() =>
                    {
                        var toolstatusHandler = ServiceFactory.Resolve<IToolStatus, PlantUnitEnum>(toolListItem.PlantUnit);
                        if (toolstatusHandler != null)
                        {
                            (toolListItem.Percentage, _) = toolstatusHandler.GetBatteryStatus(toolStatusAttributes);

                            (toolListItem.Status, toolListItem.StatusLocalizationKey) = toolstatusHandler
                                        .GetToolStatus(attributes
                                                .Where(a => a.IsStatusAttribute)
                                                .Select(a => ApplyCustomMapping(a, MeasurementSystemEnum.MetricSystem, conversionSystem)));
                        }
                    })
                    .OnFailure(() =>
                    {
                        toolListItem.Status = EntityStatusEnum.Alarm;
                        toolListItem.StatusLocalizationKey = $"{MachineManagementExtensions.LABEL_TOOLSTATUS}_{ EntityStatusEnum.Alarm.ToString().ToUpper()}";
                    });

            return toolListItem;
        }

        private AttributeDetailItem ApplyCustomMapping(DetailIdentifierMaster identifier
                        , long entityId
                        , MeasurementSystemEnum conversionSystem)
        {
            var attributeDetail = Mapper.Map<AttributeDetailItem>(identifier);
            attributeDetail.EntityId = entityId;
            attributeDetail.SetAttributeValue(identifier.Value, conversionSystem);
            return attributeDetail;
        }

        /// <summary>
        /// Recupera le unità configurate in base al tipo di unità
        /// </summary>
        /// <param name="plantUnit"></param>
        private IEnumerable<UnitEnum> GetConfiguredUnits(PlantUnitEnum plantUnit)
        {
            //Gestisce gli attributi di abilitazione unità in base alla configurazione delle stesse sulla postazione CNC
            IEnumerable<UnitEnum> configuredUnits = null;
            switch (plantUnit)
            {
                case PlantUnitEnum.DrillingMachine:
                    {
                        if (MachineConfigurationService.ConfigurationRoot.Setup.Drill.IsConfigured())
                        {
                            configuredUnits = MachineConfigurationService.ConfigurationRoot.Setup.Drill.Units
                            .Where(unit => unit.SlotCount > 0).Select(unit => unit.Id);
                        }
                    }
                    break;
                case PlantUnitEnum.OxyCutTorch:
                    {
                        if (MachineConfigurationService.ConfigurationRoot.Setup.Oxy.IsConfigured())
                        {
                            configuredUnits = MachineConfigurationService.ConfigurationRoot.Setup.Oxy.Torches.Select(unit => unit.Id);
                        }
                    }
                    break;
                case PlantUnitEnum.PlasmaTorch:
                    {
                        if (MachineConfigurationService.ConfigurationRoot.Setup.Pla.IsConfigured())
                        {
                            configuredUnits = MachineConfigurationService.ConfigurationRoot.Setup.Pla.Torches
                            .Where(unit => unit.IsPresent).Select(unit => unit.Id);
                        }
                    }
                    break;
            }

            return configuredUnits;
        }

        /// <summary>
        /// Filtra sulla lista di attributi definiti per un determinato plantUnit eliminando 
        /// gli attributi di tipo ToolEnable associati ad unità non configurate
        /// </summary>
        /// <param name="attributes"></param>
        /// <param name="toolType"></param>
        /// <returns></returns>
        private List<AttributeDetailItem> FilterOnUnitEnabledAttributes
                                    (List<AttributeDetailItem> attributes
                                        , PlantUnitEnum plantUnit)
        {
            //Recupero degli attributi che gestiscono l'abilitazione all'unità
            var unitEnabledAttributes = new List<AttributeDetailItem>();
            unitEnabledAttributes.AddRange(attributes.Where(x =>
                                x.EnumId == AttributeDefinitionEnum.ToolEnableA
                                || x.EnumId == AttributeDefinitionEnum.ToolEnableB
                                || x.EnumId == AttributeDefinitionEnum.ToolEnableC
                                || x.EnumId == AttributeDefinitionEnum.ToolEnableD));
            //Gestisce gli attributi di abilitazione unità in base alla configurazione delle stesse sulla postazione CNC

            IEnumerable<UnitEnum> configuredUnits = GetConfiguredUnits(plantUnit);
            if (configuredUnits != null)
            {
                foreach (var unitEnabledAttribute in unitEnabledAttributes)
                {
                    //Recupera l'unità legata all'attributo di abilitazione
                    var relatedUnit = unitEnabledAttribute.EnumId.GetEnumAttribute<BitEnableFor>();

                    //Se l'unità non è presente nella lista delle unità configurate 
                    //Allora viene rimosso l'attributo
                    if (!configuredUnits.Contains(relatedUnit.Unit))
                    {
                        attributes.Remove(unitEnabledAttribute);
                    }

                }
            }

            return attributes;
        }

        /// <summary> 
        /// Get Attribute DefinitionItem List for Tool based on filters
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private IEnumerable<AttributeDetailItem> GetToolAttributeDefinitionItems(
            AttributeDefinitionFilter filter)
        {

            if (!Enum.IsDefined(filter.ToolType))
                return null;

            //Recupero la tecnologia 
            var plantUnitAttribute = filter.ToolType.GetEnumAttribute<PlantUnitAttribute>();

            //Definisco la processingTechnology
            var processingTechnology = plantUnitAttribute.PlantUnit == PlantUnitEnum.PlasmaTorch ?
                                                    MachineConfigurationService.ConfigurationRoot.Setup.Pla
                                                        .GetProcessingTechnology() :
                                                    ProcessingTechnologyEnum.Default;
            Expression<Func<AttributeDefinitionLink, bool>> predicate = adl => adl.EntityTypeId == filter.ToolType.ToEntityType();

            if (filter.AttributeType != AttributeTypeEnum.All)
            {
                predicate.AndAlso(adl => adl.AttributeDefinition.AttributeType == filter.AttributeType);
            }

            var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            AttributeDefinitionLinkRepository.Attach(uow);
            //Recupero le definizioni degli attributi
            var attributeDefinitions = AttributeDefinitionLinkRepository
                    .FindBy(predicate);

            var protectionLevels = UserSession.GetProtectionLevels();

            var list = Mapper.Map<IEnumerable<AttributeDetailItem>>(attributeDefinitions)
                    .Select(a => ApplyCustomMapping(a, MeasurementSystemEnum.MetricSystem
                                        , filter.ConversionSystem, null, filter.Values));

            if (!filter.Values?.Any() ?? false)
            {
                list = list.Select(a =>
                {
                    switch (a.AttributeKind)
                    {
                        case AttributeKindEnum.Enum:
                            if (a.Value.CurrentValueId == default)
                            {
                                a.Value.CurrentValueId = 0;
                            }
                            break;
                        case AttributeKindEnum.Number:
                        case AttributeKindEnum.Bool:
                            if (a.Value.CurrentValue == null)
                            {
                                a.Value.CurrentValue = 0;
                            }
                            break;
                        case AttributeKindEnum.String:
                            if (a.Value.CurrentValue == null)
                            {
                                a.Value.CurrentValue = string.Empty;
                            }
                            break;
                    }

                    return a;
                });
            }

            list = FilterOnUnitEnabledAttributes(list.ToList(), plantUnitAttribute.PlantUnit)
                        .OrderBy(a => a.Order);

            // Add ToolRulesHandler call => Gitea #425
            var rulesHandler = ServiceFactory.Resolve<IEntityRulesHandler<ToolDetailItem>>();

            var toolDetail = rulesHandler.Handle(new ToolDetailItem
            {
                ToolType = filter.ToolType,
                PlantUnit = filter.ToolType.GetEnumAttribute<PlantUnitAttribute>()?.PlantUnit ?? PlantUnitEnum.None,
                Attributes = list.ToList()
            });


            return toolDetail.Attributes;
        }

        /// <summary>
        /// Inner Add Master Tool Table entity
        /// </summary>
        /// <param name="newTool"></param>
        /// <param name="toolType"></param>
        /// <returns></returns>
        private Entity AddToolMasterRecord<T>(ToolImportItem<T> newTool
                        , ToolTypeEnum toolType
                        , MeasurementSystemEnum conversionSystemFrom = MeasurementSystemEnum.MetricSystem
                        , IUnitOfWork<IMachineManagentDatabaseContext> unitOfWork = null)
        {
            var uow = unitOfWork ?? UnitOfWorkFactory.GetOrCreate(UserSession);

            try
            {
                //Recupera la processing Technology
                var processingTechnology = GetRealProcessTechnology(
                            MachineConfigurationService.ConfigurationRoot.Setup.Pla.GetProcessingTechnology()
                            , toolType);

                #region < Creazione Tool >
                var entity = Mapper.Map<Entity>(newTool);
                entity.EntityTypeId = toolType.ToEntityType();
                entity.HashCode = CalculateHashCode(entity.EntityTypeId, newTool.Identifiers
                                                    , conversionSystemFrom
                                                    , unitOfWork: uow);
                entity.DisplayName = CalculateDisplayName(entity.EntityTypeId, newTool.Identifiers
                                                    , conversionSystemFrom);
                #endregion < Creazione Tool >
                EntityRepository.Attach(uow);
                entity = EntityRepository.Add(entity);
                uow.Commit();
                return entity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

  
        private IEnumerable<AttributeValue>
            GetAttributeValues<TAttribute>(ToolImportItem<TAttribute> newTool
            , long entityId, EntityTypeEnum entityType)
        {
            var attributeValuesToAdd = Enumerable.Empty<AttributeValue>();

            //Recupera tutti gli attributi non identificativi
            var attributesDefinitionLinkList = AttributeDefinitionLinkRepository
                .FindBy(a => a.EntityTypeId == entityType);

            //ToolImportItem specifica in maniera obbligatoria solo gli identificatori
            foreach (var attributeDefinitionLink in attributesDefinitionLinkList)
            {
                //Recuperate le definizioni degli attributi controllo se è stato specificato un attributo
                //collegato alla definizione nella collezione Attributes del tool da importare
                //altrimenti lo importo con valore 0
                AttributeValue attributeValue = Mapper.Map<AttributeValue>(attributeDefinitionLink);
                attributeValue.EntityId = entityId;
                attributeValue.Value = 0;

                if (newTool.Attributes.TryGetValue(attributeDefinitionLink.AttributeDefinition.DisplayName
                            , out var attribute))
                {
                    if (attribute is AttributeValueItem attributeValueToAdd)
                    {

                        switch (attributeDefinitionLink.AttributeDefinition.AttributeKind)
                        {
                            case AttributeKindEnum.String:
                                attributeValue.TextValue = attributeValueToAdd.CurrentValue.ToString();
                                break;
                            case AttributeKindEnum.Number:
                            case AttributeKindEnum.Bool:
                                if (decimal.TryParse(attributeValueToAdd.CurrentValue.ToString(), out var decimalValue))
                                {
                                    attributeValue.Value = decimalValue;
                                }
                                break;
                            case AttributeKindEnum.Enum:
                                attributeValue.Value = attributeValueToAdd.CurrentValueId;
                                break;
                        }
                    }
                    else
                    {
                        switch (attributeDefinitionLink.AttributeDefinition.AttributeKind)
                        {
                            case AttributeKindEnum.String:
                                attributeValue.TextValue = attribute.ToString();
                                break;
                            case AttributeKindEnum.Number:
                            case AttributeKindEnum.Bool:
                            case AttributeKindEnum.Enum:
                                if (decimal.TryParse(attribute.ToString(), out var decimalValue))
                                {
                                    attributeValue.Value = decimalValue;
                                }
                                break;
                        }
                    }
                }

                #region < Set Value for Unit Enabling >
                if (attributeDefinitionLink.AttributeDefinition.EnumId == AttributeDefinitionEnum.ToolEnableA &&
                            newTool.ToolUnitMask.HasFlag(ToolUnitMaskEnum.UnitA))
                    attributeValue.Value = 1;

                if (attributeDefinitionLink.AttributeDefinition.EnumId == AttributeDefinitionEnum.ToolEnableB &&
                        newTool.ToolUnitMask.HasFlag(ToolUnitMaskEnum.UnitB))
                    attributeValue.Value = 1;

                if (attributeDefinitionLink.AttributeDefinition.EnumId == AttributeDefinitionEnum.ToolEnableC &&
                    newTool.ToolUnitMask.HasFlag(ToolUnitMaskEnum.UnitC))
                    attributeValue.Value = 1;

                if (attributeDefinitionLink.AttributeDefinition.EnumId == AttributeDefinitionEnum.ToolEnableD &&
                                newTool.ToolUnitMask.HasFlag(ToolUnitMaskEnum.UnitD))
                    attributeValue.Value = 1;
                #endregion < Set Value for Unit Enabling >

                attributeValuesToAdd = attributeValuesToAdd.Append(attributeValue);
            }

            return attributeValuesToAdd;
        }

        private IEnumerable<AttributeOverrideValue> GetAttributeOverrideValues<TAttribute>(
                    IEnumerable<AttributeValue> attributeValuesToAdd
                    , Dictionary<string, TAttribute> newToolAttributes)
        {
            // Recupero gli Id e displayName degli attributeDefinition che sono Override
            var overridesDefinitionIds = AttributeDefinitionLinkRepository
                    .FindBy(adl => adl.ControlType == ClientControlTypeEnum.Override)
                    .ToDictionary(adl => adl.Id, adl => adl.AttributeDefinition.DisplayName);

            var attributeOverrideValuesToAdd = Enumerable.Empty<AttributeOverrideValue>();
            // Recupero solo gli AttributeValue che ho appena inserito nel DB, che sono override (mi serve l'ID per inserirlo nella tabella collegata) 
            foreach (var attributeValue in
                    attributeValuesToAdd
                        .Where(av => overridesDefinitionIds.Keys.Contains(av.AttributeDefinitionLinkId)))
            {
                if (overridesDefinitionIds.TryGetValue(attributeValue.AttributeDefinitionLinkId, out var displayName)
                    && newToolAttributes.TryGetValue(displayName, out var attribute))
                {
                    if (attribute is AttributeValueItem attributeValueToAdd)
                    {
                        attributeOverrideValuesToAdd =
                                attributeOverrideValuesToAdd.Append(new AttributeOverrideValue
                                {
                                    AttributeValueId = attributeValue.Id,
                                    Value = attributeValueToAdd.CurrentOverrideValue.Value,
                                    OverrideType = attributeValueToAdd.CurrentOverrideValue.OverrideType
                                });
                    }
                }
            }

            return attributeOverrideValuesToAdd;
        }

        /// <summary>
        /// Inner New Tool Creation
        /// </summary>
        /// <param name="newTool"></param>
        /// <returns></returns>
        private Result InnerAddToolAttributes<TAttribute>(ToolImportItem<TAttribute> newTool
                                                , long toolId
                                                , EntityTypeEnum entityType
                                                , IUnitOfWork<IMachineManagentDatabaseContext> unitOfWork = null)
        {

            var uowAttributes = unitOfWork ?? UnitOfWorkFactory.GetOrCreate(UserSession);

            try
            {
                EventHubClient.StatusEvent(new StatusEvent(percentualProgress: "50"
                     , status: GenericEventStatusEnum.InProgress
                     , localizationKey: "LBL_CLONE_TOOL_ATTRIBUTES"));
                AttributeValueRepository.Attach(uowAttributes);
                AttributeDefinitionLinkRepository.Attach(uowAttributes);

                var attributesToAdd= GetAttributeValues(newTool, toolId , entityType);
                 AttributeValueRepository.BatchInsert(attributesToAdd);
                uowAttributes.Commit();

                // Aggiunta degli Overrides
                EventHubClient.StatusEvent(new StatusEvent(percentualProgress: "65"
                     , status: GenericEventStatusEnum.InProgress
                     , localizationKey: "LBL_CLONE_TOOL_ATTRIBUTES_OVERRIDES"));

                var attributeOverrideValuesToAdd = GetAttributeOverrideValues(attributesToAdd, newTool.Attributes);
                if (attributeOverrideValuesToAdd.Any())
                {
                    AttributeValueRepository.BatchInsertOverrides(attributeOverrideValuesToAdd);
                    uowAttributes.Commit();
                }

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.InnerException?.Message ?? ex.Message);
            }
            finally
            {
                if (unitOfWork == null)
                    uowAttributes.Dispose();
            }
        }

        /// <summary>
        /// Crea un nuovo tool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newTool"></param>
        /// <param name="processingTechnology"></param>
        /// <returns></returns>
        private Result<Entity> InnerCreateTool<T>(ToolImportItem<T> newTool
                        , ProcessingTechnologyEnum processingTechnology = ProcessingTechnologyEnum.Default)
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);

            EntityRepository.Attach(uow);
            uow.BeginTransaction();
            Entity created = null;
            try
            {
                // Controllo se il tooltype specificato esiste
                if (Enum.TryParse<ToolTypeEnum>(newTool.Type, out var toolType))
                {
                    var plantUnit = toolType.GetEnumAttribute<PlantUnitAttribute>()
                                ?.PlantUnit ?? PlantUnitEnum.None;

                    // Abilito l'utensile per ogni unità
                    if (newTool.ToolUnitMask is ToolUnitMaskEnum.None)
                    {
                        newTool.ToolUnitMask = plantUnit == PlantUnitEnum.None ? ToolUnitMaskEnum.None
                                        : plantUnit is PlantUnitEnum.DrillingMachine or PlantUnitEnum.SawingMachine ? ToolUnitMaskEnum.All
                                            : ToolUnitMaskEnum.UnitC | ToolUnitMaskEnum.UnitD;
                    }

                    EventHubClient.StatusEvent(new StatusEvent(percentualProgress: "20"
                                    , status: GenericEventStatusEnum.InProgress
                                    , localizationKey: "LBL_CLONE_TOOL_MASTER"));

                    // Aggiungo il master
                    created = AddToolMasterRecord(newTool, toolType,conversionSystemFrom: UserSession.ConversionSystem,
                                    unitOfWork: uow);
                    if (created.Id > 0)
                    {
                        // Aggiungi gli attributi perchè nella tabella AttributeValue non c'è una FK diretta sulla tabella dei tool
                        InnerAddToolAttributes(newTool, created.Id,
                                entityType: created.EntityTypeId
                                , unitOfWork: uow)
                            .OnSuccess(() =>
                            {
                                uow.CommitTransaction();
                                EventHubClient.StatusEvent(new StatusEvent(percentualProgress: "100"
                                        , status: GenericEventStatusEnum.Completed
                                        , localizationKey: "LBL_CLONE_TOOL_COMPLETED"));
                            })
                            .OnFailure(errors =>
                            {
                                throw new Exception(errors.ToString());
                            });
                    }
                }
                return Result.Ok(created);
            }
            catch (Exception e)
            {
                EventHubClient.StatusEvent(new StatusEvent(percentualProgress: "100"
                        , status: GenericEventStatusEnum.Failed
                        , localizationKey: "LBL_CLONE_TOOL_FAILED"));
                uow.RollBackTransaction();
                return Result.Fail<Entity>(e.InnerException?.Message ?? e.Message);
            }
        }

        #endregion < Private Methods >

        public Result Boot(IUserSession userSession)
        {
            return Result.Ok();
        }

        public Result CleanUpBeforeBoot(IUserSession userSession)
        {
            return Result.Ok();
        }

        #region < Tool Management >
        public ToolDetailItem GetToolTemplateForCreation(AttributeDefinitionFilter filters)
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            var toolToManage = new ToolDetailItem();

            toolToManage.ConversionSystem = UserSession.ConversionSystem;
            // Recupero la definizione degli attributi in base ai filtri in input
            var attributes = GetToolAttributeDefinitionItems(filters);

            if (attributes == null || (attributes != null && !attributes.Any()))
                return null;

            // Assegno gli identificatori ed aggiungo l'identificatore "Fake" ToolManagementId
            toolToManage.Identifiers = Mapper.Map<IEnumerable<AttributeDetailItem>>(attributes.Where(a => a.AttributeType == AttributeTypeEnum.Identifier
                    || a.GroupId == AttributeDefinitionGroupEnum.Identifiers)).ToList()
                    .AddToolManagementIdAsIdentifier(GetFirstAvailablePosition(uow));
            
            toolToManage.NumberOfCopies = 1;
            

            var identifiersEnumIds = toolToManage.Identifiers.Select(i => i.EnumId);
            // Assegno gli altri attributi
            toolToManage.Attributes = attributes.Where(a => !identifiersEnumIds.Contains(a.EnumId)).ToList();
            toolToManage.ToolType = filters.ToolType;

            return toolToManage;
        }

        /// <summary>
        /// Create a new Tool based on toolDetail input
        /// </summary>
        /// <param name="toolDetail"></param>
        /// <returns></returns>
        public Result<ToolDetailItem> CreateTool(ToolDetailItem toolDetail)
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            EntityRepository.Attach(uow);

            toolDetail.Id = toolDetail.GetAttributeValue<int>(AttributeDefinitionEnum.ToolManagementId);
            toolDetail.WarehouseId = toolDetail.GetAttributeValue<int>(AttributeDefinitionEnum.WarehouseId);


            EventHubClient.StatusEvent(new StatusEvent(percentualProgress: "0"
                            , status: GenericEventStatusEnum.Started
                            , localizationKey: "LBL_CLONE_TOOL_VALIDATE"));
            // Gitea #278 - Validazione tool
            ToolValidator.Init(new Dictionary<DatabaseDisplayNameEnum, object>
            { { DatabaseDisplayNameEnum.TS, toolDetail.ToolType } });
            var validationResult = ToolValidator.Validate(toolDetail);

            if (validationResult.Failure)
            {
                EventHubClient.StatusEvent(new StatusEvent(percentualProgress: "10"
                       , status: GenericEventStatusEnum.Failed
                       , localizationKey: ErrorCodesEnum.ERR_GEN007.ToString()));
                return Result.Fail<ToolDetailItem>(validationResult.Errors);
            }

            var entityType = toolDetail.ToolType.ToEntityType();
            var entities = EntityRepository.FindBy(e => e.EntityTypeId == entityType && e.SecondaryKey == toolDetail.Id);
            if (entities.Any())
            {
                EventHubClient.StatusEvent(new StatusEvent(percentualProgress: "10"
                , status: GenericEventStatusEnum.Failed
                , localizationKey: ErrorCodesEnum.ERR_GEN002.ToString()));
                        return Result.Fail<ToolDetailItem>(
                            new ErrorDetail(DatabaseDisplayNameEnum.ToolManagementId.ToString(), ErrorCodesEnum.ERR_GEN003.ToString())
                        );
            }

            var itemToCreate = Mapper.Map<ToolImportItem<AttributeValueItem>>(toolDetail);

            itemToCreate.Identifiers = toolDetail.Identifiers
                                      .Where(i => !i.IsFake)
                                      .ToDictionary(id => id.DisplayName
                                          , id => id.GetAttributeValue(
                                                  conversionSystemFrom: toolDetail.ConversionSystem).ToString());

            EventHubClient.StatusEvent(new StatusEvent(percentualProgress: "17"
                       , status: GenericEventStatusEnum.Failed
                       , localizationKey: "LBL_CLONE_TOOL_GETATTRIBUTES"));
            // Recupero le definizioni degli attributi e gli assegno i valori di default
            if (!toolDetail.Attributes.Any())
            {
                var enabledUnits = GetEnabledUnits(toolDetail.ToolType);
                toolDetail.Attributes = 
                        AttributeDefinitionLinkRepository.FindBy(adl => adl.EntityTypeId == toolDetail.ToolType.ToEntityType())
                        .Select(a =>
                        {
                            // Trasformo in attributeValueItem e gli assegno il valore di default
                            var attributeDetail = Mapper.Map<AttributeDetailItem>(a);
                            attributeDetail.SetDefaultAttributeValue(ServiceFactory);

                            // Valori di default per attributi speciali del tool
                            // Attributi di abilitazione unità e Diametro reale
                            if (enabledUnits.Contains(a.AttributeDefinition.EnumId))
                            {
                                attributeDetail.Value.CurrentValue = 1;
                            }
                            else if (a.AttributeDefinition.EnumId == AttributeDefinitionEnum.RealDiameter)
                            {
                                var nominalDiameterAttribute = toolDetail.Identifiers
                                        .SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.NominalDiameter);
                                if (nominalDiameterAttribute != null &&
                                    decimal.TryParse(nominalDiameterAttribute.Value
                                        .CurrentValue.ToString(), out var nominalDiameter))
                                {
                                    attributeDetail.Value.CurrentValue = nominalDiameter;
                                }
                            }
                            return attributeDetail;
                        });
            }

            itemToCreate.Attributes = toolDetail.Attributes
                            .ToDictionary(a => a.DisplayName, a =>
                            {
                                if (a.AttributeKind == AttributeKindEnum.Number)
                                {
                                    var convertedValue = a.GetAttributeValue(conversionSystemFrom: toolDetail.ConversionSystem);
                                    a.Value.CurrentValue = convertedValue;
                                }
                                return a.Value;
                            });

            return InnerCreateTool(itemToCreate)
                    .OnSuccess(created =>
                    {
                        toolDetail.InnerId = created.Id;
                        return Result.Ok(toolDetail);
                    });
        }


        private Result<ToolDetailItem> Get(Entity entity
                    , IUnitOfWork<IMachineManagentDatabaseContext> unitOfWork
                    , bool onlyQuickAccess = false
                    , MeasurementSystemEnum conversionSystemFrom = MeasurementSystemEnum.MetricSystem
                    , MeasurementSystemEnum conversionSystemTo = MeasurementSystemEnum.MetricSystem)
        {
            DetailIdentifierRepository.Attach(unitOfWork);
            AttributeValueRepository.Attach(unitOfWork);

            if (entity == null)
            {
                return Result.Fail<ToolDetailItem>(ErrorCodesEnum.ERR_GEN002.ToString());
            }

            var toolDetail = Mapper.Map<ToolDetailItem>(entity);
            var identifiers = DetailIdentifierRepository.GetIdentifiers(identifier =>
                        identifier.HashCode == entity.HashCode)
                        .OrderBy(i => i.Priority);
            var attributes = AttributeValueRepository.FindBy(a => a.EntityId == entity.Id)
                        .OrderBy(i => i.Priority);

            var protectionLevels = UserSession.GetProtectionLevels();

            toolDetail.CodeGenerators = Mapper.Map<IEnumerable<CodeGeneratorItem>>(identifiers.Where(i => i.IsCodeGenerator));

            toolDetail.Identifiers = identifiers.Select(i => ApplyCustomMapping(i, entity.Id, UserSession.ConversionSystem));

            toolDetail.Attributes = attributes.Select(a =>
            {
                var attributeDetail = Mapper.Map<AttributeDetailItem>(a);
                attributeDetail.SetAttributeValue(
                    a.AttributeDefinitionLink.AttributeDefinition
                        .AttributeKind == AttributeKindEnum.String
                        ? a.TextValue
                        : a.Value, UserSession.ConversionSystem);
                attributeDetail.EntityId = entity.Id;
                attributeDetail.IsReadonly = !protectionLevels.Contains(attributeDetail.ProtectionLevel);
                // Setta la visibilità dell'attributo in base al suo scope ed alla richiesta in input di avere
                // solo attributi fondamentali
                attributeDetail.Hidden = onlyQuickAccess && attributeDetail.AttributeScopeId != AttributeScopeEnum.Fundamental;
                return attributeDetail;
            });

            // Gitea #521 => Effettuo la validazione del tool in base al suo tooltype
            // In caso di fallimento assegno lo stato di Allarme
            ToolValidator.Init(ServiceFactory, new Dictionary<DatabaseDisplayNameEnum, object>()
                                { { DatabaseDisplayNameEnum.TS, toolDetail.ToolType } });
            var result = ToolValidator.ValidateAttributes(toolDetail.Attributes)
                    .OnSuccess(() =>
                    {
                        var toolStatusHandler = ServiceFactory.Resolve<IToolStatus, PlantUnitEnum>(toolDetail.PlantUnit);
                        toolDetail.SetToolStatus(toolStatusHandler);
                    })
                    .OnFailure(() =>
                    {
                        toolDetail.Status = EntityStatusEnum.Alarm;
                        toolDetail.StatusLocalizationKey = $"{MachineManagementExtensions.LABEL_TOOLSTATUS}_{ EntityStatusEnum.Alarm.ToString().ToUpper()}";
                    });

            //var processingTechnology = GetRealProcessTechnology(
            //                MachineConfigurationService.ConfigurationRoot.Setup.Pla.GetProcessingTechnology()
            //                , entity.EntityTypeId.ToToolType());
            toolDetail.SetUnitsEnabledMask();
            //var plantUnit = entity.EntityTypeId.ToToolType().GetEnumAttribute<PlantUnitAttribute>()?.PlantUnit ?? PlantUnitEnum.None;
            //toolDetail.SetSetupInfo(Execution, MachineConfigurationService.ConfigurationRoot, plantUnit);

            var rulesHandler = ServiceFactory.Resolve<IEntityRulesHandler<ToolDetailItem>>();

            toolDetail = rulesHandler.Handle(toolDetail);

            return Result.Ok(toolDetail);
        }

        public Result<ToolDetailItem> Get(long toolId
                    , bool onlyQuickAccess = false
                    , MeasurementSystemEnum conversionSystemFrom = MeasurementSystemEnum.MetricSystem
                    , MeasurementSystemEnum conversionSystemTo = MeasurementSystemEnum.MetricSystem)
        {
            var unitOfWork = UnitOfWorkFactory.GetOrCreate(UserSession);
            EntityRepository.Attach(unitOfWork);

            return Get(EntityRepository.Get(toolId), unitOfWork, onlyQuickAccess, conversionSystemFrom, conversionSystemTo);
        }

        public Result<ToolDetailItem> GetByToolManagementId(int toolManagementId
                    , bool onlyQuickAccess = false
                    , MeasurementSystemEnum conversionSystemFrom = MeasurementSystemEnum.MetricSystem
                    , MeasurementSystemEnum conversionSystemTo = MeasurementSystemEnum.MetricSystem)
        {
            var unitOfWork = UnitOfWorkFactory.GetOrCreate(UserSession);
            EntityRepository.Attach(unitOfWork);

            var entityToolTypes = EntityTypeExtensions.GetToolEntityTypes();

            var tool = EntityRepository.FindBy(e => e.SecondaryKey == toolManagementId
                        && entityToolTypes.Contains(e.EntityTypeId))
                        .SingleOrDefault();
            return Get(tool, unitOfWork, onlyQuickAccess, conversionSystemFrom, conversionSystemTo);
        }

        
        /// <summary>
        /// Get All Tools
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ToolListItem> GetAll()
        {
            var unitOfWork = UnitOfWorkFactory.GetOrCreate(UserSession);
            EntityRepository.Attach(unitOfWork);
            DetailIdentifierRepository.Attach(unitOfWork);
            AttributeValueRepository.Attach(unitOfWork);

            var entityTypes = EntityTypeExtensions.GetToolEntityTypes();

            var detailIdentifiers = DetailIdentifierRepository.GetIdentifiers(identifier =>
                        entityTypes.Contains(identifier.EntityTypeId))
                        .ToList();

            var identifiersLookup = detailIdentifiers
                .Select(i => Mapper.Map<IdentifierDetailItem>(i))
                .ToLookup(di => di.HashCode);


            var codeGeneratorsLookup = detailIdentifiers.Where(ic => ic.IsCodeGenerator)
                .OrderBy(x => x.Priority)
                .Select(item => Mapper.Map<CodeGeneratorItem>(item).ApplyCustomMapping(UserSession.ConversionSystem))
                .ToLookup(cg => cg.HashCode);

            // Tool Entity Types
            var toolEntityTypes = EntityTypeExtensions.GetToolEntityTypes();

            // Recupero gli attributi per calcolare lo stato collegati a tutti i tool definiti
            var attributes = AttributeValueRepository.FindBy(av =>
                                toolEntityTypes.Contains(av.Entity.EntityTypeId))
              .GroupBy(e => e.EntityId)
              .ToDictionary(a => a.Key, a =>
                        a.AsEnumerable()
                        .Select(a =>
                        {
                            var attribute = Mapper.Map<AttributeDetailItem>(a);
                            attribute.SetAttributeValue(a.AttributeDefinitionLink.AttributeDefinition.AttributeKind == AttributeKindEnum.String
                                            ? a.TextValue
                                            : a.Value, UserSession.ConversionSystem);
                            return attribute;
                        }));
                                                
              

            var entities = EntityRepository.FindBy(e => entityTypes.Contains(e.EntityTypeId))
                    .OrderBy(e => e.SecondaryKey).ToList();
            return
                entities.Select(entity => ApplyCustomMapping(entity
                                        , identifiersLookup[entity.HashCode]
                                        , codeGeneratorsLookup[entity.HashCode]
                                        , attributes[entity.Id]
                                        , UserSession.ConversionSystem));
        }


        public Result Remove(long toolId)
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            uow.BeginTransaction();
            try
            {
                EntityRepository.Attach(uow);
                AttributeValueRepository.Attach(uow);

                var entity = EntityRepository.Get(toolId);
                if (entity == null)
                {
                    return Result.Fail(ErrorCodesEnum.ERR_GEN002.ToString());
                }

                AttributeValueRepository.Remove(a => a.EntityId == toolId);
                EntityRepository.Remove(entity);
                uow.Commit();
                uow.CommitTransaction();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                uow.RollBackTransaction();
                return Result.Fail(ex.InnerException?.Message ?? ex.Message);
            }
        }

        /// <summary>
        /// Update Tool and related Attributes
        /// </summary>
        /// <param name="tool"></param>
        /// <returns></returns>
        public Result<ToolDetailItem> UpdateTool(ToolDetailItem tool)
        {
            try
            {
                var dbEntity = EntityRepository.GetBySecondaryKey(tool.Id, tool.ToolType.ToEntityType());

                if (dbEntity == null)
                    return Result.Fail<ToolDetailItem>(ErrorCodesEnum.ERR_GEN002.ToString());

                ToolValidator.Init(new Dictionary<DatabaseDisplayNameEnum, object>
                {
                    { DatabaseDisplayNameEnum.TS, tool.ToolType }
                });

                return ToolValidator.Validate(tool)
                            .OnSuccess(() =>
                            {
                                var plantUnit = dbEntity.EntityTypeId.GetEnumAttribute<PlantUnitAttribute>()?.PlantUnit ?? PlantUnitEnum.None;
                                var toolStatusHandler = ServiceFactory.Resolve<IToolStatus, PlantUnitEnum>(plantUnit);
                                tool.SetToolStatus(toolStatusHandler);
                                return InnerToolUpdate(dbEntity, tool);
                            })
                            .OnSuccess(() =>
                            {
                                var result = Result.Ok();

                                tool.CodeGenerators = tool.Identifiers
                                        .Select(item => Mapper.Map<CodeGeneratorItem>(item).ApplyCustomMapping(UserSession.ConversionSystem))
                                        .ToList();
                                // TO DO:
                                //if (tool.Source == Enums.UpdateSourceEnum.Application
                                //        && IsInSetup(tool.Id, dbEntity.ToolType.PlantUnitEnumId))
                                //{
                                //    result = PLCApiHandler.UpdateToolOnCnc(tool);
                                //}
                                return result.Success ? Result.Ok(tool) : Result.Fail<ToolDetailItem>(result.Errors);
                            });
            }
            catch (Exception ex)
            {
                return Result.Fail<ToolDetailItem>(ex.InnerException?.Message ?? ex.Message);
            }
        }


        #endregion < Tool Management >

        #region < IRemoteToolService>
        public IEnumerable<ToolItem> GetToolIdentifiers() => GetToolIdentifiers(new());

        public IEnumerable<ToolItem> GetToolIdentifiers(ToolItemIdentifiersFilter filter)
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            EntityRepository.Attach(uow);

            // Per qualsiasi combinazione di filtro specificata,
            // recupera solo i tool il cui type è managed
            Expression<Func<Tool, bool>> predicate = x => x.IsManaged;

            // Se è specificata l'unità di abilitazione allora filtra per l'unità di abilitazione
            // recuperando solo i tool non presenti in altri slot
            if (filter.ToolUnitMask != ToolUnitMaskEnum.All &&
                filter.ToolUnitMask != ToolUnitMaskEnum.None)
            {
                predicate = predicate.AndAlso(x => x.ToolMask.HasFlag(filter.ToolUnitMask));
            }

            // Se è specificato il filtro unitType
            if (filter.UnitType != PlantUnitEnum.All)
            {
                predicate = predicate.AndAlso(x => (x.PlantUnitId & filter.UnitType) == filter.UnitType);
            }

            // Recupero i tool senza attributi
            var tools = EntityRepository.FindTools(predicate).OrderBy(x => x.Id);
            if (tools.Any() is false)
                return Array.Empty<ToolItem>();

            // Recupero i ToolId dei tool richiesti per ottenere gli attributi
            var toolIds = tools.ToDictionary(r => r.Id, r => r.ToolManagementId);

            var hashCodes = tools.Select(r => r.HashCode).ToHashSet();

            // Recupero gli identificatori
            var attributeValuesLookup = AttributeValueRepository.FindBy(
                a => a.AttributeDefinitionLink.GroupId == AttributeDefinitionGroupEnum.ProcessingToolFilter)
                .Where(a => toolIds.ContainsKey(a.EntityId))
                    .ToLookup(a => toolIds[a.EntityId]);// la chiave diventa il ToolManagementId

            var identifiers = DetailIdentifierRepository.FindBy(di => hashCodes.Contains(di.HashCode));

            var identifiersLookup = identifiers
                .OrderBy(x => x.Priority)
                .Select(identifier => Mapper.Map<IdentifierDetailItem>(identifier.Convert(filter.ConversionSystem)))
                .ToLookup(identifier => identifier.HashCode);



            var toolStatusAttributesLookup = EntityRepository.GetStatusAttributes(a => a.EntityTypeId.ToParentType() 
                                                                                        == ParentTypeEnum.Tool 
                                                                            && (PlantUnitEnum)a.SecondaryKey == filter.UnitType)
               .GroupBy(x => x.EntityId)
               .ToDictionary(x => x.Key, x => x.AsEnumerable());

            var toolstatusHandler = ServiceFactory.Resolve<IToolStatus, PlantUnitEnum>(filter.UnitType);
            if (toolstatusHandler == null)
                throw new NotImplementedException();

            var toolStatuses = toolStatusAttributesLookup.Select(item =>
            {
                var toolStatusAttributes = item.Value;

                var attributes = Mapper.Map<IEnumerable<AttributeDetailItem>>(toolStatusAttributes)
                    .Select(a => ApplyCustomMapping(a, conversionSystemFrom: MeasurementSystemEnum.MetricSystem
                            , conversionSystemTo: MeasurementSystemEnum.MetricSystem));

                (var status, _) = toolstatusHandler.GetToolStatus(attributes);

                return new { ToolManagementId = toolIds[item.Key], Status = status };
            }).ToDictionary(status => status.ToolManagementId);


            return Mapper.Map<IEnumerable<ToolItem>>(tools).Select(tool =>
            {
                tool.Identifiers = identifiersLookup[tool.HashCode]
                    .ToDictionary(x => Enum.Parse<DatabaseDisplayNameEnum>(x.DisplayName), x => (object)x.Value);

                tool.Attributes = attributeValuesLookup[tool.ToolManagementId]
                    .ToDictionary(x => Enum.Parse<DatabaseDisplayNameEnum>(x.AttributeDefinitionLink.AttributeDefinition.DisplayName), x => (object)x.Value);

                tool.Status = toolStatuses[tool.ToolManagementId].Status;

                return tool;
            });
        }

        public ToolItem GetTool(ToolItemFilter filter)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
