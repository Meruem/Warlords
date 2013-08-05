using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TechTalk.SpecFlow;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Hubs;

namespace Warlords.Server.Specs.Infrastructure
{
    public static class ScenarioContextExtensions
    {
        public static MessageHub MessageHub(this ScenarioContext context)
        {
            Contract.Requires(context.ContainsKey("MessageHub"), "Message hub not initialized.");
            var result = context["MessageHub"] as MessageHub;
            Contract.Assert(result != null, "Message hub not initialized.");
            return result;
        }

        public static void SetMessageHub(this ScenarioContext context, MessageHub hub)
        {
            Contract.Requires(hub != null);
            context["MessageHub"] = hub;
        }

        public static IEventStore EventStore(this ScenarioContext context)
        {
            Contract.Requires(context.ContainsKey("InMemoryEventStore"), "InMemoryEventStore not initialized.");
            var result = context["InMemoryEventStore"] as IEventStore;
            Contract.Assert(result != null, "InMemoryEventStore not initialized.");
            return result;
        }

        public static void SetEventStore(this ScenarioContext context, IEventStore eventStore)
        {
            Contract.Requires(eventStore != null);
            context["InMemoryEventStore"] = eventStore;
        }

        public static List<string> CalledMethods(this ScenarioContext context)
        {
            if (!ScenarioContext.Current.ContainsKey("CalledMethods"))
            {
                ScenarioContext.Current["CalledMethods"] = new List<string>();
            }
            var result = context["CalledMethods"] as List<string>;
            Contract.Requires(result != null, "Called methods empty.");
            return result;
        }
    }
}
