using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using Ninject;
using Warlords.Server.Application.Infrastructure;
using Warlords.Server.Domain.Models.Lobby;
using Warlords.Server.Infrastructure;

namespace Warlords.Server.Hubs
{
    public class LobbyHub : WarlordsHub
    {
        [Inject]
        public IRepository<Lobby> Repository { get; set; }

        [Authorize]
        public IEnumerable<string> GetAllPlayers()
        {
            var lobbyId = Repository.GetAllIds().FirstOrDefault();

            // hack :(
            //Contract.Assert(lobbyId != null, "No lobby exists");

            if (lobbyId == default(Guid))
            {
                return null;
            }

            var lobby = Repository.GetById(lobbyId);

            return lobby.Players;
        }

        [Authorize]
        public IEnumerable<LobbyGameInfo> GetAllGames()
        {
            //var lobbyId = Repository.GetAllIds().FirstOrDefault();

            //// hack :(
            ////Contract.Assert(lobbyId != null, "No lobby exists");

            //if (lobbyId == default(Guid))
            //{
            //    var newLobby = new Lobby();
            //    Repository.Save(newLobby, -1);
            //}

            //var lobby = Repository.GetById(lobbyId);

            //return lobby.Games;
            return null;
        }

        //[Authorize]
        //public LobbyGameInfo CreateGame()
        //{
        //    var currentPlayer = GetCurrentPlayer();
        //    var game = Command<CreateGameCommand>().WithParameters(currentPlayer).Execute();
        //    Clients.All.gameCreated(game);
        //    return game;
        //}

        //private Player GetCurrentPlayer()
        //{
        //    var name = Context.User.Identity.Name;
        //    var currentPlayer = Query<GetPlayerInLobbyByNameQuery>().WithParameters(name).GetResult();

        //    Contract.Assert(currentPlayer != null, "Current user is not joined");

        //    return currentPlayer;
        //}

        //[Authorize]
        //public LobbyGameInfo JoinGame(string gameId)
        //{
        //    var currentPlayer = GetCurrentPlayer();
        //    var game = Query<GetGameInLobbyByIdQuery>().WithParameters(gameId).GetResult();
        //    Contract.Assert(game != null, "Game doesn't exist.");
        //    Command<JoinGameCommand>().WithParameters(currentPlayer, game).Execute();
        //    Clients.All.gameChanged(game);
        //    return game;
        //}

        //[Authorize]
        //public void LeaveGame()
        //{
        //    var currentPlayerName = Context.User.Identity.Name;
        //    var game = Query<GetGamePlayedByPlayerQuery>().WithParameters(currentPlayerName).GetResult();
        //    Contract.Assert(game != null, "Game doesn't exist.");

        //    game.LeavePlayer(currentPlayerName); //hmm

        //    if (!game.Players.Any())
        //    {
        //        Command<RemoveGameCommand>().WithParameters(game.Id).Execute();
        //        Clients.All.gameRemoved(game.Id);
        //    }

        //    Clients.All.gameChanged(game);
        //}

        //[Authorize]
        //public void StartGame(string gameId)
        //{
        //    var gameInfo = Query<GetGameInLobbyByIdQuery>().WithParameters(gameId).GetResult();
        //    Contract.Assert(gameInfo != null, "Game doesn't exist.");

        //    gameInfo.StartGame();
        //    //Clients.All.gameChanged(game);

        //    var game = new Game(gameInfo);
        //    game.InitializeGame();

        //    var gameStorage = Query<GetCurrentGameStorageQuery>().GetResult();
        //    Contract.Assert(gameStorage != null, "game storage");
        //    gameStorage.AddGame(game);
            
        //    foreach(var player in gameInfo.Players)
        //    {
        //        Clients.Client(player.GetConnectionId()).gameStarted(game);
        //    }
        //}
    }
}