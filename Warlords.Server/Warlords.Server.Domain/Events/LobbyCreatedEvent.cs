﻿using System;
using Warlords.Server.Domain.Infrastructure;

namespace Warlords.Server.Domain.Events
{
    public class LobbyCreatedEvent : Event
    {
        public Guid Guid { get; set; }
    }
}