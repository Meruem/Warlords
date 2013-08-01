using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Warlords.Server.Models;

namespace Warlords.Server.Queries
{
    // default implementation using in-memory object
    public class GetCurrentGameStorageQuery: Query<GameRepository>
    {
        private GameRepository _GameStorage;

        public GetCurrentGameStorageQuery(GameRepository gameStorage)
        {
            _GameStorage = gameStorage;
        }

        public override GameRepository GetResult()
        {
            return _GameStorage;
        }
    }
}