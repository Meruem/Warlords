using System.Diagnostics.Contracts;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Warlords.Server.Infrastructure
{
    public class HandlerFactory : IHandlerFactory
    {
        private readonly Dictionary<Type, IList<Type>> _handlers;
        private readonly IKernel _kernel;
 
        public HandlerFactory(IKernel kernel)
        {
            _kernel = kernel;
            _handlers = new Dictionary<Type, IList<Type>>();

            SearchForHandlers();
        }

        private void SearchForHandlers()
        {
            var concreteTypes = GetTypesImplementingIHandles();

            foreach (var type in concreteTypes)
            {
                var messageType = GetMessageType(type);

                if (!_handlers.ContainsKey(messageType))
                {
                    _handlers[messageType] = new List<Type>();
                }

                _handlers[messageType].Add(type);
            }
        }

        private static Type GetMessageType(Type type)
        {
            Contract.Requires(type != null);
            return type.GetInterface("IHandles`1").GetGenericArguments().First();
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

        public IEnumerable<object> GetHandlersForMessage(Type messageType)
        {
            if (!_handlers.ContainsKey(messageType))
            {
                yield break; 
            }

            foreach (var type in _handlers[messageType])
            {
                yield return _kernel.Get(type);
            }
        }
    }
}