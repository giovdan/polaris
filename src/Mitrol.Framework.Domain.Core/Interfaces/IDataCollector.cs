namespace Mitrol.Framework.Domain.Core.Interfaces
{
    public interface IDataCollector
    {
        bool Subscribe(IStorable storable);
        bool UnSubscribe(IStorable storable);
        bool Save();
    }
}
