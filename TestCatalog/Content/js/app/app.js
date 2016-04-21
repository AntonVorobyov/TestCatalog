(function() {
    "use strict";

    angular
        .module("app", ["ngRoute", "selectpicker", "phonemask"])
        .config([
            "$routeProvider", "$locationProvider", function($routeProvider, $locationProvider) {

                $routeProvider
                    .when("/", {
                        templateUrl: "/",
                        controller: "usersController"
                    });
                //.otherwise({
                //    redirectTo: "/"
                //});

                $locationProvider.html5Mode({
                    enabled: true,
                    requireBase: false
                }); //.hashPrefix("!");
            }
        ]);
})();