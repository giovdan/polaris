namespace Mitrol.Framework.Domain.Core.Interfaces
{
    public interface IHasRowVersion
    {
        string RowVersion { get; set; }
    }
}
