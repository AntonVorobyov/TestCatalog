(function () {
    "use strict";

    angular
        .module("app")
        .factory("countryService", [
            "$http", function($http) {
                return {
                    countries: function (params) {
                        return $http.get("/api/countries", { params: params });
                    }
                };
            }
        ]);
})();