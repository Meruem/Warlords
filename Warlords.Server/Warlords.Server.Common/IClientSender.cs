using System;

namespace Warlords.Server.Common
{
    public interface IClientSender
    {
        void SendToAllClients(object message);
        void SendToClient(string connectionId, object message);
    }
}
