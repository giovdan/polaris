namespace Mitrol.Framework.Domain.SignalR
{
    using System.Threading.Tasks;

    public interface INotesHub
    {
        Task UserRemoved(long userId);
    }
}