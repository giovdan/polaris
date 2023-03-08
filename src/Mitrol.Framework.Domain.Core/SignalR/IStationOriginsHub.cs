namespace Mitrol.Framework.Domain.Core.SignalR
{
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using System.Threading.Tasks;

    public interface IStationOriginsHub : IHubWithConnectionCounter
    {
        Task Stations(StationItem[] stationItems);
    }
}
