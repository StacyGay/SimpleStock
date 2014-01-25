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
var Services;
(function (Services) {
    var InventoryService = (function () {
        function InventoryService($resource) {
            var updateDescriptor = {
                method: "PUT",
                headers: { 'auth-token': 'C3PO R2D2' }
            };

            this.Resource = $resource('/api/Inventory/:id', null, {
                'update': updateDescriptor
            });
        }
        InventoryService.$inject = ["$resource"];
        return InventoryService;
    })();
    Services.InventoryService = InventoryService;

    angularServices.service("InventoryService", InventoryService);
})(Services || (Services = {}));
var Services;
(function (Services) {
    var ResourceConfig = (function () {
        function ResourceConfig() {
        }
        return ResourceConfig;
    })();
    Services.ResourceConfig = ResourceConfig;
})(Services || (Services = {}));
angular.module("Controllers", []).controller(Controllers);
angular.module("SimpleStock", ["Controllers", "Services", "Directives"]);
//# sourceMappingURL=app.js.map
