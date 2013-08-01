using System;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Threading.Tasks;

namespace Warlords.Server.Infrastructure
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

        public void Send(Type commandType, object command)
        {
            Contract.Assert(command as Message != null);
            var handlers = _handlerFactory.GetHandlersForMessage(commandType);
            foreach (var handler in handlers)
            {
                InvokeHandleMethod(commandType, handler, command);
            }
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            Publish(@event.GetType(), @event);
        }

        public void Publish(Type eventType, object @event)
        {
            Contract.Assert(@event as Message != null);
            var handlers = _handlerFactory.GetHandlersForMessage(eventType);
            Parallel.ForEach(handlers, handler => InvokeHandleMethod(eventType, handler, @event));
        }

        private static void InvokeHandleMethod(Type type, object handler, object messageObject)
        {
            Contract.Requires(handler != null);
            MethodInfo method = handler.GetType().GetMethod("Handle", new[] { type });
            method.Invoke(handler, new[] { messageObject });
        }
    }
}