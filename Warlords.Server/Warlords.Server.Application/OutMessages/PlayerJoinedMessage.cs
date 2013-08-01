namespace Warlords.Server.Application.OutMessages
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