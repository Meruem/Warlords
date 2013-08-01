using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Warlords.Server.Queries
{
    public abstract class Query<TResult> : Query
    {
        public abstract TResult GetResult();
    }

    public abstract class Query<TParam, TResult> : Query
    {
        TParam _Params;

        public abstract TResult GetResult(TParam param);

        public TResult GetResult()
        {
            return GetResult(_Params);
        }

        public Query<TParam, TResult> WithParams(TParam param)
        {
            _Params = param;
            return this;
        }
    }

    public abstract class Query
    {
    }
}