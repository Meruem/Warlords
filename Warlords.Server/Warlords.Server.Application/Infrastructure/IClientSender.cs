using System;

namespace Warlords.Server.Application.Infrastructure
{
    public interface IClientSender
    {
        void SendToAllClients(string hubName, Action<dynamic> action);
        void SendToClient(string connectionId, string hubName, Action<dynamic> action);
    }
}
