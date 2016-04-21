(function () {
    "use strict";

    angular
        .module("app")
        .controller("usersController", [
            "$scope", "userService", function ($scope, userService) {

                var toPaging = function(pages) {
                    var array = [];
                    for (var i = 1; i <= pages; i++) {
                        array.push(i);
                    }
                    return array;
                };

                $scope.getUsers = function (page) {
                    userService
                        .users({ page: page || 1 })
                        .success(function(response) {
                            $scope.users = response.items;
                            $scope.total = response.total;
                            $scope.pages = toPaging(response.pages);
                            $scope.page = response.page;
                        });
                }

                $scope.getUsers();
            }
        ]);
})();