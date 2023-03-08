namespace Mitrol.Framework.Domain.Core.Interfaces
{
    public interface IStorable
    {
        bool ShouldSave();
        bool Save();
    }
}
