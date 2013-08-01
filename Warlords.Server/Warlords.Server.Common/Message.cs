using System.Text;

namespace Warlords.Server.Common
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

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(GetType().Name);
            builder.Append("(Version: ");
            builder.Append(Version);
            builder.Append(", UserName: ");
            builder.Append(UserName);
            builder.Append(", ConnectionId: ");
            builder.Append(ConnectionId);
            builder.Append(")");

            return builder.ToString();
        }
    }
}