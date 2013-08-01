using Warlords.Server.Common;

namespace Warlords.Server.Application.Infrastructure
{
    public interface IHandles<T> where T : Message
    {
        void Handle(T message);
    }
}