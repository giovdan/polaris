namespace Mitrol.Framework.MachineManagement.Application.Resolvers
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;

    public class ToolStatusResolver:IResolver<IToolStatus, PlantUnitEnum>
    {
        private readonly IServiceFactory _serviceFactory;

        public ToolStatusResolver(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IToolStatus Resolve(PlantUnitEnum plantUnit)
        {
            IToolStatus toolStatus = null;
            switch (plantUnit)
            {
                case (PlantUnitEnum.DrillingMachine):
                    toolStatus = _serviceFactory.GetService<DrillStatus>();
                    break;
                case (PlantUnitEnum.PlasmaTorch):
                    toolStatus = _serviceFactory.GetService<TorchPlaStatus>();
                    break;
                case (PlantUnitEnum.OxyCutTorch):
                    toolStatus = _serviceFactory.GetService<TorchOxyStatus>();
                    break;
                case PlantUnitEnum.SawingMachine:
                    toolStatus = _serviceFactory.GetService<SawStatus>();
                    break;
            }
           
            return toolStatus;
        }
    }
}
