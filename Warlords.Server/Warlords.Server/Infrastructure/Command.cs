using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warlords.Server.Infrastructure
{
    public abstract class CommandBase
    {
    }

    public abstract class Command : CommandBase
    {
        public abstract void Execute();
    }

    public abstract class Command<TResult> : CommandBase
    {
        public abstract TResult Execute();
    }
}