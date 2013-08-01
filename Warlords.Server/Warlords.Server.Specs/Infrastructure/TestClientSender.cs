using System;
using Warlords.Server.Application.Infrastructure;

namespace Warlords.Server.Specs.Infrastructure
{
    public class TestClientSender : IClientSender
    {
        public MethodCallStoreDynamic Mocker { get; set; }

        public TestClientSender()
        {
            Mocker = new MethodCallStoreDynamic();
        }

        public void SendToAllClients(string hubName, Action<dynamic> action)
        {
            action(Mocker);
        }

        public void SendToClient(string connectionId, string hubName, Action<dynamic> action)
        {
            action(Mocker);
        }
    }
}
