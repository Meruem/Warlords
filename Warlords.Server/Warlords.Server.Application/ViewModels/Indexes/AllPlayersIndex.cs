using System.Linq;
using Raven.Client.Indexes;

namespace Warlords.Server.Application.ViewModels.Indexes
{
    public class AllPlayersIndex : AbstractIndexCreationTask<Player>
    {
        public AllPlayersIndex()
        {
            Map = players => from player in players
                            select new { player.Name };
            //Index(x => x.Name, FieldIndexing.Analyzed);                
        }
    }
}