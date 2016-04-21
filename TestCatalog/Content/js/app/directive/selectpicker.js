(function () {
    "use strict";

    angular
        .module("selectpicker", [])
        .directive("ngSelectpicker", [
            "$timeout", "countryService", function ($timeout, countryService) {
                return {
                    restrict: "A",
                    require: "?ngModel",
                    link: function(scope, element, attrs, model) {

                        $timeout(function() {
                            var $element = $(element);

                            $element.removeClass("selectpicker");
                            $element.selectize({
                                create: false,
                                sortField: "text",
                                valueField: "id",
                                labelField: "title",
                                searchField: "title",
                                preload: "focus",
                                load: function(query, callback) {
                                    countryService
                                        .countries({ query: query })
                                        .success(function(response) {
                                            callback(response.items);
                                        });
                                },
                                onChange: function(value) {
                                    console.log("selectpicker", value);
                                    scope.$apply(function() {
                                        model.$setViewValue(value);
                                    });
                                }
                            });
                        });
                    }
                };
            }
        ]);
})();