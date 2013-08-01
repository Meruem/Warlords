/// <reference path="3rdParty/angular.js" />
var warlordsModule = angular.module('warlordsApp', []).
    config(['$routeProvider', function ($routeProvider) {
        $routeProvider
            .when('/', { templateUrl: 'partials/Main.cshtml', controller: 'MainController' })
            .when('/Game', { templateUrl: 'partials/Game.cshtml', controller: 'GameController' })
            .otherwise({ redirectTo: '/' });
    }]);