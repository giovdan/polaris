namespace Mitrol.Framework.MachineManagement.Application.Resolvers
{
    using Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Execution;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;

    public sealed class ExecutionHandlerResolver : 
             IResolver<ICodeController>
            // , IResolver<IExecution> TODO
            , IResolver<IPLCApiService>, IResolver<IParameterHandler>
    {
        private readonly IServiceFactory _serviceFactory;

        public ExecutionHandlerResolver(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        // TODO
        public IExecution Resolve()
        {
            //var machineConfigurationService = _serviceFactory.GetService<IMachineConfigurationService>();

            //// Se la macchina è FANUC imposta execution handler come FANUC
            //return (machineConfigurationService.ConfigurationRoot.Machine.Type is MachineTypeEnum.TANG_GEMINI
            //            or MachineTypeEnum.TANG_TIPOG
            //            or MachineTypeEnum.TANG_EX5)
            //        ? this._serviceFactory.GetService<FanucExecution>()

            //        // altrimenti il controller dell'esecuzione è Mitrol
            //        : this._serviceFactory.GetService<MitrolExecution>();
            return null;
        }

        ICodeController IResolver<ICodeController>.Resolve() => (ICodeController)Resolve();

        IPLCApiService IResolver<IPLCApiService>.Resolve() => (IPLCApiService)Resolve();

        IParameterHandler IResolver<IParameterHandler>.Resolve() => (IParameterHandler)Resolve();

        object IResolver.Resolve() => Resolve();
    }
}
