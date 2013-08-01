using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace Warlords.Server.Models
{
    public class Battlefield
    {
        public IList<Zone> Zones { get; private set; }

        private string _Player1Name;
        private string _Player2Name;

        public Battlefield(string player1Name, string player2Name)
        {
            _Player1Name = player1Name;
            _Player2Name = player2Name;
            Zones = new List<Zone>();
            Zones.Add(new Zone(player1Name, ZoneTypeEnum.Home));
            Zones.Add(new Zone(player1Name, ZoneTypeEnum.Forward));
            Zones.Add(new Zone(ZoneTypeEnum.Middle));
            Zones.Add(new Zone(player2Name, ZoneTypeEnum.Home));
            Zones.Add(new Zone(player2Name, ZoneTypeEnum.Forward));
        }

        public void AddBuilding(BuildingPrototype prototype, string playerName, ZoneTypeEnum zoneType)
        {
            var zone = Zones.FirstOrDefault(z => (z.OwnerName != null) && (z.OwnerName == playerName && z.Type == zoneType));

            if (zone != null)
            {
                zone.AddBuilding(prototype);
            }
        }

        public void SpawnCreatures()
        {
            foreach (var zone in Zones)
            {
                zone.SpawnCreatures();
            }
        }

        public void Fight()
        {
            foreach (var zone in Zones)
            {
                zone.Fight();
            }
        }

        public void MoveCreatures()
        {
            var orders = new List<MoveOrder>();

            foreach (var zone in Zones)
            {
                // get winner in that zone
                var winnerName = zone.GetWinner();
                if (winnerName != null)
                {
                    var player = _Player1Name == winnerName ? _Player1Name : _Player2Name;
                    var targetZone = GetTargetMoveZone(zone, player);
                    // get zone when the creatures should move to
                    if (targetZone != null)
                    {
                        // check that there are no stronger opposing forces at target area
                        var targetZoneWinnerName = targetZone.GetWinner();
                        if (targetZoneWinnerName == null || targetZoneWinnerName == winnerName || zone.GetWinnerArmyStrength() > targetZone.GetWinnerArmyStrength())
                        {
                            // actual movement, first create orders, then execute them to prevent multiple unit movement
                            orders = orders.Concat(
                                zone.GetCreaturesForPlayer(player)
                                .Select(c => new MoveOrder() { Creature = c, From = zone, OwnerName = player, To = targetZone }))
                                .ToList();

                        }
                    }
                }
            }

            // execute the orders
            foreach (var order in orders)
            {
                order.Execute();
            }

        }

        private Zone GetTargetMoveZone(Zone from, string movingPlayerName)
        {
            Contract.Requires(from != null);

            var opponent = _Player1Name == movingPlayerName ? _Player2Name : _Player1Name;

            switch (from.Type)
            {
                case ZoneTypeEnum.Middle:
                    // from middle go to opponents forward
                    return Zones.FirstOrDefault(z => z.Type == ZoneTypeEnum.Forward && z.OwnerName == opponent);
                case ZoneTypeEnum.Home:
                    if (from.OwnerName == movingPlayerName)
                    {
                        // from own home go to own forward
                        return Zones.FirstOrDefault(z => z.Type == ZoneTypeEnum.Forward && z.OwnerName == movingPlayerName);
                    }
                    else
                    {
                        // from opponents home don't go anywhere
                        return null;
                    }
                case ZoneTypeEnum.Forward:
                    if (from.OwnerName == movingPlayerName)
                    {
                        // from own forward go to middle
                        return Zones.FirstOrDefault(z => z.Type == ZoneTypeEnum.Middle);
                    }
                    else
                    {
                        // from opponent forward go to his home
                        return Zones.FirstOrDefault(z => z.Type == ZoneTypeEnum.Home && z.OwnerName == opponent);
                    }
                default:
                    return null;
            }
        }
    }
}