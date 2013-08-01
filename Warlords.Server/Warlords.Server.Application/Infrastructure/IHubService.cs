using System;
using Warlords.Server.Common;
using Warlords.Server.Domain.Infrastructure;

namespace Warlords.Server.Application.Infrastructure
{
    public interface IHubService
    {
        void Send<TCommand>(TCommand command) where TCommand : Message;
        void Send(Type commandType, object command);
        void Publish<TEvent>(TEvent @event) where TEvent : Event;
        void Publish(Type eventType, object @event);
    }
}