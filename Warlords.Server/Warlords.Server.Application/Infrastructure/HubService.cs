using System;
using System.Diagnostics.Contracts;
using Warlords.Server.Common;
using Warlords.Server.Common.Aspects;
using Warlords.Server.Domain.Infrastructure;

namespace Warlords.Server.Application.Infrastructure
{
    public class HubService : IHubService
    {
        private readonly IHandlerFactory _handlerFactory;
        private readonly IEventScheduler _eventScheduler;

        public HubService(IHandlerFactory handlerFactory, IEventScheduler eventScheduler)
        {
            _handlerFactory = handlerFactory;
            _eventScheduler = eventScheduler;
        }

        public void Send<TCommand>(TCommand command) where TCommand : Message
        {
            Send(typeof(TCommand), command);
        }

        [Log]
        public void Send(Type commandType, Message command)
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
        public void Publish(Type eventType, Message @event)
        {
            Contract.Assert(@event as Message != null);
            var handlers = _handlerFactory.GetHandlerMethodsForMessage(eventType);
            foreach (var handler in handlers)
            {
                Action<Message> tempHandler = handler;
                _eventScheduler.ScheduleJob(() => tempHandler.Invoke(@event));
            }
        }
    }
}