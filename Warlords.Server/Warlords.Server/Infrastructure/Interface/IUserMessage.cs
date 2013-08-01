namespace Warlords.Server.Infrastructure
{
    public interface IUserMessage
    {
        string PlayerName { get; set; }
        string ConnectionId { get; set; }
    }
}