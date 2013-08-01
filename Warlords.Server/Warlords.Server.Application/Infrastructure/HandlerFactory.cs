using System.Diagnostics.Contracts;
using System.Reflection;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using Warlords.Server.Common;

namespace Warlords.Server.Application.Infrastructure
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly Dictionary<Type, IList<Action<Message>>> _handlers;
        private readonly IKernel _kernel;
        private readonly IList<Action<Message>> _generalHandlers; 
 
        public HandlerFactory(IKernel kernel)
        {
            _kernel = kernel;
            _handlers = new Dictionary<Type, IList<Action<Message>>>();
            _generalHandlers = new List<Action<Message>>();

            SearchForHandlers();
        }

        public void AddSubsriber(Type messageType, Action<Message> action)
        {
            Contract.Assert(action != null);
            Contract.Assert(messageType != null);

            if (!_handlers.ContainsKey(messageType))
            {
                _handlers[messageType] = new List<Action<Message>>();
            }

            _handlers[messageType].Add(action);
        }

        public void AddSubscriberForEveryThing(Action<Message> action)
        {
            Contract.Assert(action != null);
            _generalHandlers.Add(action);
        }

        private void SearchForHandlers()
        {
            var concreteTypes = GetTypesImplementingIHandles();

            foreach (var handlerType in concreteTypes)
            {
                var messageTypes = GetMessageTypes(handlerType);
                foreach (var messageType in messageTypes)
                {
                    if (!_handlers.ContainsKey(messageType))
                    {
                        _handlers[messageType] = new List<Action<Message>>();
                    }

                    Type tempMessageType = messageType;
                    Type tempHandlerType = handlerType;
                    _handlers[messageType].Add(message =>
                    {
                        var handler = _kernel.Get(tempHandlerType);
                        MethodInfo method = handler.GetType().GetMethod("Handle", new[] { tempMessageType });
                        method.Invoke(handler, new object[] { message });
                    });
                }
            }
        }

        private static IEnumerable<Type> GetMessageTypes(Type type)
        {
            Contract.Requires(type != null);
            return type.GetInterface("IHandles`1").GetGenericArguments();
        }

        private IEnumerable<Type> GetTypesImplementingIHandles()
        {
            var assembly = typeof (IHandles<>).Assembly;
            var types = assembly.GetTypes();
            var concreteTypes =
                types.Where(
                    t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof (IHandles<>))
                         && t.IsClass);
            return concreteTypes;
        }

        public IEnumerable<Action<Message>> GetHandlerMethodsForMessage(Type messageType)
        {
            if (!_handlers.ContainsKey(messageType))
            {
                yield break;
            }

            foreach (var handler in _generalHandlers)
            {
                yield return handler;
            }

            foreach (var action in _handlers[messageType])
            {
                yield return action;
            }
        }
    }
}