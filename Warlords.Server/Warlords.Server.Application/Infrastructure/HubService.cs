using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Warlords.Server.Common;
using Warlords.Server.Common.Aspects;
using Warlords.Server.Domain.Infrastructure;

namespace Warlords.Server.Application.Infrastructure
{
    public class HubService : IHubService
    {
        private readonly IHandlerFactory _handlerFactory;

        public HubService(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        public void Send<TCommand>(TCommand command) where TCommand : Message
        {
            Send(typeof(TCommand), command);
        }

        [Log]
        public void Send(Type commandType, object command)
        {
            Contract.Assert(command as Message != null);
            var handlers = _handlerFactory.GetHandlerMethodsForMessage(commandType);
            foreach (var handler in handlers)
            {
                handler.Invoke(command);
            }
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            Publish(@event.GetType(), @event);
        }

        [Log]
        public void Publish(Type eventType, object @event)
        {
            Contract.Assert(@event as Message != null);
            var handlers = _handlerFactory.GetHandlerMethodsForMessage(eventType);
            Parallel.ForEach(handlers, handler => handler.Invoke(@event));
        }
    }
}