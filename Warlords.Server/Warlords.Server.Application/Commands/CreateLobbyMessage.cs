using Warlords.Server.Common;

namespace Warlords.Server.Application.Commands
{
    public class CreateLobbyMessage : Message
    {
        public CreateLobbyMessage()
        {
            UserName = "system";
            ConnectionId = "system";
        }
    }
}
