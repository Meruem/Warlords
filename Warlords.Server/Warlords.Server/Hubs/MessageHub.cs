using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Warlords.Server.ApplicationF;
using Warlords.Server.Common;
using Warlords.Server.Common.Aspects;

namespace Warlords.Server.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IHubService _hubService;

        public MessageHub(IHubService hubService)
        {
            _hubService = hubService;
        }

        [Log]
        public void HandleMessage(string messageType, string message)
        {
            var type = GetDotNetMessageType(messageType);
            if (type != null)
            {
                var messageObject = CreateMessageObject(message, type);
                InjectUserSpecificInformationToMessage(messageObject);
                _hubService.Send(messageObject);
            }
        }

        private static Type GetDotNetMessageType(string messageType)
        {
            var type = typeof(CreateLobbyCommand).Assembly.GetTypes().FirstOrDefault(t => t.Name == messageType);
            return type;
        }

        private Message CreateMessageObject(string message, Type type)
        {
            var messageObject = JsonConvert.DeserializeObject(message, type) as Message;
            Contract.Assert(messageObject != null, "Error in serialization");
            return messageObject;
        }

        private void InjectUserSpecificInformationToMessage(object messageObject)
        {
            var userMessageObject = messageObject as Message;
            if (userMessageObject != null)
            {
                userMessageObject.ConnectionId = Context.ConnectionId;
                userMessageObject.UserName = Context.User.Identity.Name;
            }
        }
    }
}