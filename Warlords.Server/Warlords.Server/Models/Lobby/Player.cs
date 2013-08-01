using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace Warlords.Server.Models
{
    public class Player
    {
        private string _ConnectionId;

        public string Name { get; private set; }

        public bool IsInGame { get; set; }

        [Pure]
        public string GetConnectionId() { return _ConnectionId; }

        public void RefreshConnection(string id) { _ConnectionId = id; }

        public Player(string id, string name)
        {
            _ConnectionId = id;
            Name = name;
        }
    }
}