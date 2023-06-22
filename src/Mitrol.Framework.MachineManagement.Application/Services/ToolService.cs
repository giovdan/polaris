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
    using Mitrol.Framework.Domain.Configuration.Extensions;
    using Mitrol.Framework.Domain.Core.Interfaces;

    public class ToolService : MachineManagementBaseService, IToolService
    {
        public ToolService(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

        private IEntityRepository EntityRepository => ServiceFactory.GetService<IEntityRepository>();
        private IDetailIdentifierRepository DetailIdentifierRepository => ServiceFactory.GetService<IDetailIdentifierRepository>();
        private IMachineConfigurationService MachineConfigurationService => ServiceFactory.GetService<IMachineConfigurationService>();

        private IEntityValidator<ToolDetailItem> ToolValidator
                => ServiceFactory.GetService<IEntityValidator<ToolDetailItem>>();


        #region < Private Methods >
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
                                , MeasurementSystemEnum conversionSystem)
        {
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

            //ToolValidator.ValidateAttributes(attributes)
            //        .OnSuccess(() =>
            //        {
                        var toolstatusHandler = ServiceFactory.Resolve<IToolStatus, PlantUnitEnum>(toolListItem.PlantUnit);
                        if (toolstatusHandler != null)
                        {
                            (toolListItem.Percentage, _) = toolstatusHandler.GetBatteryStatus(toolStatusAttributes);

                            (toolListItem.Status, toolListItem.StatusLocalizationKey) = toolstatusHandler
                                        .GetToolStatus(attributes
                                                .Where(a => a.IsStatusAttribute)
                                                .Select(a => ApplyCustomMapping(a, MeasurementSystemEnum.MetricSystem, conversionSystem)));
                        }
                    //})
                    //.OnFailure(() =>
                    //{
                    //    toolListItem.Status = EntityStatusEnum.Alarm;
                    //    toolListItem.StatusLocalizationKey = $"{MachineManagementExtensions.LABEL_TOOLSTATUS}_{ EntityStatusEnum.Alarm.ToString().ToUpper()}";
                    //})
                    ;

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
        #endregion < Private Methods >

        public Result Boot(IUserSession userSession)
        {
            return Result.Ok();
        }

        public Result CleanUpBeforeBoot(IUserSession userSession)
        {
            return Result.Ok();
        }

        public Result<ToolDetailItem> CreateTool(ToolDetailItem toolDetail)
        {
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
    }
}
