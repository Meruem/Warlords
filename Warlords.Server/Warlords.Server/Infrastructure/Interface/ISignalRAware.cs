using Microsoft.AspNet.SignalR.Hubs;

namespace Warlords.Server.Infrastructure
{
    public interface ISignalRAwareHandler
    {
        IHubConnectionContext Clients { get; set; }
    }
}
