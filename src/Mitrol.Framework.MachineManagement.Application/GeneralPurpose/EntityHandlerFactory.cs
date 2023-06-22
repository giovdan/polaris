namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.RulesHandlers;

    public class EntityHandlerFactory : IEntityHandlerFactory
    {
        private IServiceFactory _serviceFactory;

        private ILookup<ParentTypeEnum, Func<IEntityRulesHandler>> _handlerFactories;
       

        public EntityHandlerFactory(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;

            // Lista dei factory-method per ottenere gli handlers associati ad uno specifico ParentType
            _handlerFactories = new ValueTuple<ParentTypeEnum, Func<IEntityRulesHandler>>[]
             {
                 //(  ParentTypeEnum.ProgramItem,  () => _serviceFactory.GetService<ProgramRulesHandler>()),
                 //(  ParentTypeEnum.StockItem,  () => _serviceFactory.GetService<StockItemRulesHandler>()),
                 //(  ParentTypeEnum.OperationType,  () => _serviceFactory.GetService<OperationTypeRulesHandler>()),
                 //(  ParentTypeEnum.OperationAdditionalItem,  () => _serviceFactory.GetService<AdditionalItemRulesHandler>()),
                 //(  ParentTypeEnum.ToolRange, () => _serviceFactory.GetService<ToolRangeRulesHandler>()),
                 //(  ParentTypeEnum.ProgramPiece,  () => _serviceFactory.GetService<PieceLinkRulesHandler>())
             }
             // Conversione (item1, item2)[] -> ILookup<,>
             .ToLookup(x => x.Item1, x => x.Item2);
           
        }
        

        public IEnumerable<IEntityRulesHandler> ResolveHandlers(ParentTypeEnum parentType)
        {
            // Costruzione degli handler ricavati dalla collezione
            foreach (var handlerFactory in _handlerFactories[parentType])
            {
                yield return handlerFactory.Invoke();
            }
        }
    }
}
