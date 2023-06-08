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

    public class ToolService : BaseService, IToolService
    {
        private IAttributeValueRepository AttributeValueRepository => ServiceFactory.GetService<IAttributeValueRepository>();
        private IDetailIdentifierRepository DetailIdentifierRepository => ServiceFactory.GetService<IDetailIdentifierRepository>();
        
        public ToolService(IServiceFactory serviceFactory) : base(serviceFactory)
        {

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

            return Result.Ok(toolDetail);
        }

        private ToolListItem ApplyCustomMapping(ToolListItem tool
                                , IEnumerable<IdentifierDetailItem> identifiers
                                , IEnumerable<CodeGeneratorItem> codeGenerators
                                , MeasurementSystemEnum conversionSystem)
        {
            tool.Identifiers = identifiers;
            tool.CodeGenerators = codeGenerators;
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
            return
                Mapper.Map<IEnumerable<ToolListItem>>
                    (EntityRepository.FindBy(e => entityTypes.Contains(e.EntityTypeId)))
                    .Select(tool => ApplyCustomMapping(tool
                                        , identifiersLookup[tool.HashCode]
                                        , codeGeneratorsLookup[tool.HashCode]
                                        , UserSession.ConversionSystem));
        }


    }
}
