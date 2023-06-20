namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IMachineParameterRepository: IRepository<MachineParameter, IMachineManagentDatabaseContext>
    {
        MachineParameterLink AddOrUpdateLink(MachineParameterLink machineParameterLink);

        void CleanUp(HashSet<long> parametersToKeep, HashSet<long> parameterLinksToKeep);

        IEnumerable<MachineParameterLink> FindLinkBy(Expression<Func<MachineParameterLink, bool>> predicate);

        MachineParameter Get(string code);

        MachineParameter Get(string code, ParameterCategoryEnum category);

        Task<MachineParameter> GetAsync(string code);

        Task<MachineParameter> GetAsync(string code, ParameterCategoryEnum category);

        MachineParameterLink GetMachineParameterLink(long id);

        Task<MachineParameterLink> GetMachineParameterLinkAsync(long id);

        IEnumerable<MachineParameterLink> GetMachineParameterLinks(long parameterId);

        Task<IEnumerable<MachineParameterLink>> GetMachineParameterLinksAsync(long parameterId);

        IEnumerable<MachineParameter> GetMachineParameterWithLinks();

        void RemoveLinks(Expression<Func<MachineParameterLink, bool>> predicate);

        void BulkUpdate(IEnumerable<MachineParameter> parameters);
    }
}
