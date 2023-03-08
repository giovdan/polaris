namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Core.Interfaces;

    public class NullDataCollector : IDataCollector
    {
        public bool Save()
        {
            return true;
        }

        public bool Subscribe(IStorable storable)
        {
            return true;
        }

        public bool UnSubscribe(IStorable storable)
        {
            return true;
        }

        public static NullDataCollector Instance { get; } = new NullDataCollector();
    }
}
