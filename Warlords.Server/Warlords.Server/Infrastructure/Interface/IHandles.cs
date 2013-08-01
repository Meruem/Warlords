using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warlords.Server.Infrastructure
{
    public interface IHandles<T> where T : Message
    {
        void Handle(T message);
    }
}