namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Models;

    public interface IParameterStrategy
    {
        Result SetParameter(string code, object value);
    }
}
