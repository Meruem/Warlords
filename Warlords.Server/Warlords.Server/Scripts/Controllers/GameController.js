/// <reference path="../3rdParty/linq.js" />
/// <reference path="../3rdParty/angular.js" />
/// <reference path="../app.js" />
function GameController($scope, $location, currentGameService) {
    $scope.currentGame = currentGameService.game;

    $.connection.hub.logging = true;
    var gameHub = $.connection.gameHub;

    $.connection.hub.start().done(function () {
        gameHub.server.getGameStatus(currentGameService.game.Id).done(function (game) {
            $scope.currentGameData = game;
            $scope.$apply();
        });
    });

};