var angularServices = angular.module("Services", ["ngResource"]);
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
angular.module("Controllers", []).controller(Controllers);
angular.module("SimpleStock", ["Controllers", "Services", "Directives"]);
//# sourceMappingURL=app.js.map
