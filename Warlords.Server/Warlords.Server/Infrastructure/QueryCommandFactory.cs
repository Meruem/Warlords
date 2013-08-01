using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warlords.Server.Queries
{
    public class QueryCommandFactory
    {
        IKernel _Kernel;

        public QueryCommandFactory(IKernel kernel)
        {
            _Kernel = kernel;
        }

        public TQueryType Get<TQueryType>()
        {
            return _Kernel.Get<TQueryType>();
        }
    }
}