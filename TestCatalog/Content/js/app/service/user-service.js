(function() {
    "use strict";

    angular
        .module("app")
        .factory("userService", [
            "$http", function($http) {
                return {
                    users: function(params) {
                        return $http.get("/api/users", { params: params });
                    },

                    user: function(id) {
                        return $http.get("/api/users/" + id);
                    }
                };
            }
        ]);
})();