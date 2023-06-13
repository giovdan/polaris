namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMachineParameterService : IApplicationService, IBootableService, IRemoteMachineParameterService
    {
        object ExportToJsonService();

        MachineParameterItem Get(long id);

        Task<IEnumerable<MachineParameterItem>> GetAllItemsAsync(MeasurementSystemEnum conversionSystem);

        Task<MachineParameterItem> GetAsync(string code, MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem);

        Task<MachineParameterItem> GetAsync(long id);

        Task<IEnumerable<MachineParameterNodeConfiguration>> GetNodesAsync();

        IEnumerable<ImportResult<string>> ImportParameterValues(string json);

        Result<MachineParameterItem> Update(MachineParameterToUpdate parameterToUpdate);
    }
}
