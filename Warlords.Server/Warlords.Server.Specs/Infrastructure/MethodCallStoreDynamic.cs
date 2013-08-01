using System.Dynamic;
using TechTalk.SpecFlow;

namespace Warlords.Server.Specs.Infrastructure
{
    public class MethodCallStoreDynamic : DynamicObject
    {
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var methods = ScenarioContext.Current.CalledMethods();

            methods.Add(binder.Name);

            result = null;
            return true;
        }
    }
}
