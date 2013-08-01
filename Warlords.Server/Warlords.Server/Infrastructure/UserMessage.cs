namespace Warlords.Server.Infrastructure
{
    public class UserMessage : SignalRMessage, IUserMessage
    {
        public string PlayerName { get; set; }
        public string ConnectionId { get; set; }
    }
}