using System;
using Warlords.Server.Common;
using Warlords.Server.DomainF.Events;

namespace Warlords.Server.Application.Infrastructure
{
    public interface IHubService
    {
        void Send<TCommand>(TCommand command) where TCommand : Message;
        void Send(Type commandType, Message command);
        void Publish<TEvent>(TEvent @event) where TEvent : Event;
        void Publish(Type eventType, Message @event);
    }
}