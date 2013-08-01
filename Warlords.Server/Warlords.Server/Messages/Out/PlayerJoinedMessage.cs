using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warlords.Server.Messages.Out
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