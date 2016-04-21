(function () {
    "use strict";

    angular
        .module("phonemask", [])
        .directive("ngPhone", [
            "$timeout", function($timeout) {
                return {
                    restrict: "A",
                    //require: "?ngModel",
                    link: function(scope, element, attrs, model) {

                        $timeout(function() {
                            var $element = $(element);

                            $element.inputmask({
                                mask: "+9-(999)-999-99-99",
                                placeholder: "_",
                                onUnMask: function(maskedValue, unmaskedValue, opts) {
                                    return maskedValue;
                                }
                            });
                        });
                    }
                };
            }
        ]);
})();