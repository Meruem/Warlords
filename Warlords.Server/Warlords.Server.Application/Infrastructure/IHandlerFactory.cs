using System;
using System.Collections.Generic;
using System.Reflection;
using Warlords.Server.Common;

namespace Warlords.Server.Application.Infrastructure
{
    public interface IHandlerFactory
    {
        IEnumerable<Action<Message>> GetHandlerMethodsForMessage(Type messageType);
        void AddSubsriber(Type messageType, Action<Message> action);
        void AddSubscriberForEveryThing(Action<Message> action);
        void AddHandlersLocatedInAssembly(Assembly assembly);
    }
}