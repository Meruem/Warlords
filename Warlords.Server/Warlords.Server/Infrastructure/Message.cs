namespace Warlords.Server.Infrastructure
{
    public class Message
    {
        public Message(int version)
        {
            Version = version;
        }

        public Message()
            : this(-1)
        {
        }

        public int Version { get; set; }
        public string UserName { get; set; }
        public string ConnectionId { get; set; }
    }
}