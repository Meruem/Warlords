using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using TechTalk.SpecFlow;

namespace Warlords.Server.Specs.Infrastructure
{
    public static class ClientSenderHelper
    {
        public static bool CheckMethodWasCalled(string methodName)
        {
            var methods = ScenarioContext.Current.CalledMethods();
            Contract.Assert(methods != null);
            return methods.Any(m => m == methodName);
        }
    }
}
