/// <reference path="../reference.ts" />
var Controllers;
(function (Controllers) {
    var MainController = (function () {
        function MainController($scope) {
            $scope.MainVm = this;
        }
        MainController.$inject = ["$scope"];
        return MainController;
    })();
    Controllers.MainController = MainController;
})(Controllers || (Controllers = {}));
//# sourceMappingURL=MainController.js.map
