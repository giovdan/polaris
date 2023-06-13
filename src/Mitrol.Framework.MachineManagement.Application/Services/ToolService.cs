namespace Mitrol.Framework.MachineManagement.Application.Services
{
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

    public class ToolService : MachineManagementBaseService, IToolService
    {
        public ToolService(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

        private IEntityRepository EntityRepository => ServiceFactory.GetService<IEntityRepository>();
        private IAttributeValueRepository AttributeValueRepository => ServiceFactory.GetService<IAttributeValueRepository>();
        private IDetailIdentifierRepository DetailIdentifierRepository => ServiceFactory.GetService<IDetailIdentifierRepository>();

        private AttributeDetailItem ApplyCustomMapping(DetailIdentifierMaster identifier
                        , long entityId
                        , MeasurementSystemEnum conversionSystem)
        {
            var attributeDetail = Mapper.Map<AttributeDetailItem>(identifier);
            attributeDetail.EntityId = entityId;
            attributeDetail.SetAttributeValue(identifier.Value, conversionSystem);
            return attributeDetail;
        }

 
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



        public Result<ToolDetailItem> Get(long toolId)
        {
            var unitOfWork = UnitOfWorkFactory.GetOrCreate(UserSession);
            EntityRepository.Attach(unitOfWork);
            DetailIdentifierRepository.Attach(unitOfWork);
            AttributeValueRepository.Attach(unitOfWork);

            var entity = EntityRepository.Get(toolId);
            if (entity == null)
            {
                return Result.Fail<ToolDetailItem>(ErrorCodesEnum.ERR_GEN002.ToString());
            }

            var toolDetail = Mapper.Map<ToolDetailItem>(entity);
            var identifiers = DetailIdentifierRepository.GetIdentifiers(identifier =>
                        identifier.HashCode == entity.HashCode)
                        .OrderBy(i => i.Priority);
            var attributes = AttributeValueRepository.FindBy(a => a.EntityId == toolId)
                        .OrderBy(i => i.Priority);

            var protectionLevels = UserSession.GetProtectionLevels();

            toolDetail.CodeGenerators = Mapper.Map<IEnumerable<CodeGeneratorItem>>(identifiers.Where(i => i.IsCodeGenerator));

            toolDetail.Identifiers = identifiers.Select(i => ApplyCustomMapping(i, toolId, UserSession.ConversionSystem));

            toolDetail.Attributes = attributes.Select(a =>
                                    {
                                        var attributeDetail = Mapper.Map<AttributeDetailItem>(a);
                                        attributeDetail.SetAttributeValue(
                                            a.AttributeDefinitionLink.AttributeDefinition
                                                .AttributeKind == AttributeKindEnum.String
                                                ? a.TextValue
                                                : a.Value, UserSession.ConversionSystem);
                                        attributeDetail.EntityId = toolId;
                                        attributeDetail.IsReadonly = !protectionLevels.Contains(attributeDetail.ProtectionLevel);
                                        return attributeDetail;
                                    });

            return Result.Ok(toolDetail);
        }

        private ToolListItem ApplyCustomMapping(Entity entity
                                , IEnumerable<IdentifierDetailItem> identifiers
                                , IEnumerable<CodeGeneratorItem> codeGenerators
                                , MeasurementSystemEnum conversionSystem)
        {
            var tool = Mapper.Map<ToolListItem>(entity);
            tool.Identifiers = identifiers;
            tool.CodeGenerators = codeGenerators;
            tool.InnerId = entity.Id;
            return tool;
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
            var entityTypes = EntityTypeExtensions.GetToolEntityTypes();

            var detailIdentifiers = DetailIdentifierRepository.GetIdentifiers(identifier =>
                        entityTypes.Contains(identifier.EntityTypeId));

            var identifiersLookup = detailIdentifiers
                .Select(i => Mapper.Map<IdentifierDetailItem>(i))
                .ToLookup(di => di.HashCode);


            var codeGeneratorsLookup = detailIdentifiers.Where(ic => ic.IsCodeGenerator)
                .OrderBy(x => x.Priority)
                .Select(item => Mapper.Map<CodeGeneratorItem>(item).ApplyCustomMapping(UserSession.ConversionSystem))
                .ToLookup(cg => cg.HashCode);

            var entities = EntityRepository.FindBy(e => entityTypes.Contains(e.EntityTypeId));
            return
                entities.Select(entity => ApplyCustomMapping(entity
                                        , identifiersLookup[entity.HashCode]
                                        , codeGeneratorsLookup[entity.HashCode]
                                        , UserSession.ConversionSystem));
        }
    }
}
