using System.Diagnostics.Contracts;

namespace Warlords.Server.Domain.Models.Lobby
{
    public class Player
    {
        private string _connectionId;

        public string Name { get; private set; }

        public bool IsInGame { get; set; }

        [Pure]
        public string GetConnectionId() { return _connectionId; }

        public void RefreshConnection(string id) { _connectionId = id; }

        public Player(string id, string name)
        {
            _connectionId = id;
            Name = name;
        }
    }
}