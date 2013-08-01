using System;

namespace Warlords.Server.Infrastructure
{
    public class SignalRMessage : Message, ISignalRMessage
    {
        public string ConnectionId { get; set; }
    }
}