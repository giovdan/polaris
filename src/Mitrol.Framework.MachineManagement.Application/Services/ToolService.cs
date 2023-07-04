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
            IEnumerable<ToolStatusAttribute> toolStatusAttributes =
                    Mapper.ProjectTo<ToolStatusAttribute>(
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

            var enabledUnits = GetEnabledUnits(toolDetail.ToolType);

            itemToCreate.Identifiers = toolDetail.Identifiers
                                      .ToDictionary(id => id.DisplayName
                                          , id => id.GetAttributeValue(
                                                  conversionSystemFrom: toolDetail.ConversionSystem).ToString());

            // Recupero le definizioni degli attributi e gli assegno i valori di default
            if (!toolDetail.Attributes.Any())
            {
                toolDetail.Attributes = 
                        AttributeDefinitionLinkRepository.FindBy(adl => adl.EntityTypeId == toolDetail.ToolType.ToEntityType())
                        .Select(a =>
                        {
                            // Trasformo in attributeValueItem e gli assegno il valore di default
                            var attributeDetail = Mapper.Map<AttributeDetailItem>(a);
                            attributeDetail.SetDefaultAttributeValue(ServiceFactory);
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

            return Result.Ok(new ToolDetailItem());
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

        
        #endregion < Tool Management >
    }
}
