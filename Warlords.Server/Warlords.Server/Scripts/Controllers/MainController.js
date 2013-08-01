/// <reference path="../3rdParty/linq.js" />
/// <reference path="../3rdParty/angular.js" />
/// <reference path="../app.js" />
function MainController($scope, $location, currentGameService) {
    $.connection.hub.logging = true;
    var lobby = $.connection.lobbyHub;
    var messageHub = $.connection.messageHub;
    $scope.players = [];
    $scope.games = [];
    $scope.currentUserName = currentUserName;
    $scope.currentGame = currentGameService.game;

    function setCurrentGame(game) {
        currentGameService.game = game;
        $scope.currentGame = currentGameService.game;
        $scope.$apply();
    }

    $scope.canJoinGame = function (gameId) {
        if (currentGameService.game != null && currentGameService.game.Id != null) return false;

        for (var i = 0; i < $scope.games.length; i++) {
            if ($scope.games[i].Id == gameId) {
                if ($scope.games[i].IsStarted) return false;

                if ($scope.games[i].Players.length >= 2) return false;

                if (Enumerable.from($scope.games[i].Players).any('$.Name == currentUserName')) return false;

                return true;
            }
        }

        return false;
    };

    $scope.createGame = function () {
        messageHub.server.handleMessage('CreateGameMessage', '{}')
            .done(function (message) {
            //setCurrentGame(message.Game);
        });
    };

    $scope.joinGame = function (gameId) {
        lobby.server.joinGame(gameId).done(function(game){
            setCurrentGame(game);
        });
    };

    $scope.leaveGame = function () {
        lobby.server.leaveGame().done(function () {
            setCurrentGame(null);
        });
    };

    $scope.startGame = function () {
        if (currentGameService.game != null) {
            lobby.server.startGame(currentGameService.game.Id);
        }
    };

    lobby.client.gameStarted = function (game) {
        $location.path('/Game');
        $scope.$apply();
    };

    messageHub.client.gameCreated = function (message) {
        $scope.$apply($scope.games.push(message.Game));
    };

    messageHub.client.playerJoined = function (player) {
        $scope.$apply($scope.players.push(player));
    };

    lobby.client.gameRemoved = function (gameId) {
        for (var i = 0; i < $scope.games.length; i++) {
            if ($scope.games[i].Id == gameId) {
                $scope.games.splice(i, 1);
                break;
            }
        }

        $scope.$apply();
    };

    lobby.client.gameChanged = function (game) {
        // find game to replace with new information
        for (var i = 0; i < $scope.games.length; i++) {
            if ($scope.games[i].Id == game.Id) {
                $scope.games[i] = game;
                break;
            }
        }

        // change current game information in necessary
        if (currentGameService.game) {
            if (currentGameService.game.Id == game.Id) {
                setCurrentGame(game);
            }
        }

        $scope.$apply();
    };

    $.connection.hub.start().done(function () {
        //lobby.server.joinLobby()
        messageHub.server.handleMessage('JoinPlayerInLobbyMessage', '{}')
            .done(function () {
                // After joining lobby

                // load list of all Owners in lobby
                lobby.server.getAllPlayers()
                    .done(function (players) {
                        $scope.$apply($scope.players = players);
                    });

                // load list of all active games
                lobby.server.getAllGames()
                    .done(function (games) {

                        // check that the current Owner already is in any of these games
                        for (var i = 0; i < games.length; i++) {
                            if (Enumerable.from(games[i].Players).any("$.Name == currentUserName")) {
                                setCurrentGame(games[i]);
                                break;
                            }
                        }

                        $scope.$apply($scope.games = games);
                    });
            });
    });
};

