using System;
using System.Collections.Generic;

namespace Warlords.Server.Infrastructure
{
    public interface IHandlerFactory
    {
        IEnumerable<object> GetHandlersForMessage(Type messageType);
    }
}