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

                        $timeout(function () {
                            console.log(scope, model);

                            var $element = $(element);
                            $element
                                .removeClass("selectpicker")
                                .selectize({
                                    create: false,
                                    sortField: "text",
                                    valueField: "id",
                                    labelField: "title",
                                    searchField: "title",
                                    preload: "focus",
                                    options: [
                                        scope.$eval(attrs.ngSelectpicker)
                                    ],
                                    items: [model.$modelValue],
                                    load: function(query, callback) {
                                        countryService
                                            .countries({ query: query })
                                            .success(function(response) {
                                                scope.options = response.items;
                                                callback(response.items);
                                            });
                                    },
                                    onChange: function(value) {
                                        console.log("selectpicker", value);
                                        console.log(scope);

                                        if (value) {
                                            model.$setViewValue(value);
                                            scope.$apply();
                                        }
                                    }
                                });
                        });
                    }
                };
            }
        ]);
})();