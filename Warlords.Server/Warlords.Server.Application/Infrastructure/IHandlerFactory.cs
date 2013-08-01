using System;
using System.Collections.Generic;

namespace Warlords.Server.Application.Infrastructure
{
    public interface IHandlerFactory
    {
        IEnumerable<Action<object>> GetHandlerMethodsForMessage(Type messageType);
        void AddSubsriber(Type messageType, Action<object> action);
    }
}