namespace Warlords.Server.Application.Messages.Out
{
    public class PlayerJoinedMessage
    {
        public string Name { get; set; }

        public PlayerJoinedMessage(string name)
        {
            Name = name;
        }
    }
}