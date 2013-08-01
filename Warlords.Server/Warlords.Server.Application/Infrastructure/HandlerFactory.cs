using System.Diagnostics.Contracts;
using System.Reflection;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Warlords.Server.Application.Infrastructure
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly Dictionary<Type, IList<Action<object>>> _handlers;
        private readonly IKernel _kernel;
 
        public HandlerFactory(IKernel kernel)
        {
            _kernel = kernel;
            _handlers = new Dictionary<Type, IList<Action<object>>>();

            SearchForHandlers();
        }

        public void AddSubsriber(Type messageType, Action<object> action)
        {
            Contract.Requires(action != null);
            Contract.Requires(messageType != null);

            if (!_handlers.ContainsKey(messageType))
            {
                _handlers[messageType] = new List<Action<object>>();
            }

            _handlers[messageType].Add(action);
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
                        _handlers[messageType] = new List<Action<object>>();
                    }

                    Type tempMessageType = messageType;
                    Type tempHandlerType = handlerType;
                    _handlers[messageType].Add(message =>
                    {
                        var handler = _kernel.Get(tempHandlerType);
                        MethodInfo method = handler.GetType().GetMethod("Handle", new[] { tempMessageType });
                        method.Invoke(handler, new[] { message });
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

        public IEnumerable<Action<object>> GetHandlerMethodsForMessage(Type messageType)
        {
            if (!_handlers.ContainsKey(messageType))
            {
                yield break;
            }

            foreach (var action in _handlers[messageType])
            {
                yield return action;
            }
        }
    }
}