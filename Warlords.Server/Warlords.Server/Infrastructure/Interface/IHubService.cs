using System;

namespace Warlords.Server.Infrastructure
{
    public interface IHubService
    {
        void Send<TCommand>(TCommand command) where TCommand : Message;
        void Send(Type commandType, object command);
        void Publish<TEvent>(TEvent @event) where TEvent : Event;
        void Publish(Type eventType, object @event);
    }
}