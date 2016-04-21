(function () {
    "use strict";

    angular
        .module("app")
        .controller("usersController", [
            "$scope", "userService", function ($scope, userService) {

                userService
                    .users()
                    .success(function (response) {
                        $scope.users = response.items;
                        $scope.total = response.total;
                    });

                $scope.title = "yooo";
            }
        ]);
})();