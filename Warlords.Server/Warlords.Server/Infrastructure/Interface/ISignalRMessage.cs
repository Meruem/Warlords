namespace Warlords.Server.Infrastructure
{
    public interface ISignalRMessage
    {
        string ConnectionId { get; set; }
    }
}