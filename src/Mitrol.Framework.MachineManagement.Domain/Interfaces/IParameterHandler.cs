namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models;

    public interface IParameterHandler
    {
        Result SetParameter(MachineParameterLink parameter, float value);
    }
}
