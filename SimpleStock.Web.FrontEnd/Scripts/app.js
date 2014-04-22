var angularServices = angular.module("Services", ["ngResource"]);
var angularDirectives = angular.module("Directives", []);
var Controllers;
(function (Controllers) {
    var AccountController = (function () {
        function AccountController($scope, resourceFactory) {
            $scope.vm = this;
            this._accountService = resourceFactory.GetEntityResource("/api/account/:id");
            this.Account.Stores = [];
            this.Account.Stores.push({ Id: 0, Name: "", CompanyId: 0 });
        }
        AccountController.prototype.CreateAccount = function () {
            console.log("testing create");
            this.Account = this._accountService.save({}, this.Account, function () {
            }), function () {
            };
        };
        AccountController.$inject = ["$scope", "EntityResourceFactory"];
        return AccountController;
    })();
    Controllers.AccountController = AccountController;
})(Controllers || (Controllers = {}));
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
    

    var EntityResourceFactory = (function () {
        function EntityResourceFactory($resource) {
            this.$resource = $resource;
        }
        EntityResourceFactory.prototype.GetEntityResource = function (url, auth) {
            if (typeof auth === "undefined") { auth = null; }
            var entityResource = this.$resource(url, null, this.GetDescriptors(auth));

            return entityResource;
        };

        EntityResourceFactory.prototype.GetDescriptors = function (auth) {
            if (typeof auth === "undefined") { auth = null; }
            var authHeaders = auth == null ? null : { 'Authorization': 'Basic ' + btoa(auth.Username + ':' + auth.Password) };
            var queryDescriptor = {
                method: 'GET',
                headers: authHeaders,
                isArray: true
            };
            var getDescriptor = {
                method: 'GET',
                headers: authHeaders,
                isArray: false
            };
            var saveDescriptor = {
                method: 'POST',
                headers: authHeaders,
                isArray: false
            };
            var updateDescriptor = {
                method: 'PUT',
                headers: authHeaders
            };
            var deleteDescriptor = {
                method: 'DELETE',
                headers: authHeaders
            };

            return {
                'query': queryDescriptor,
                'get': getDescriptor,
                'save': saveDescriptor,
                'update': updateDescriptor,
                'delete': deleteDescriptor
            };
        };
        EntityResourceFactory.$inject = ["$resource"];
        return EntityResourceFactory;
    })();
    Services.EntityResourceFactory = EntityResourceFactory;

    angularServices.service("EntityResourceFactory", EntityResourceFactory);
})(Services || (Services = {}));
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
angular.module("Controllers", []).controller(Controllers);
angular.module("SimpleStock", ["Controllers", "Services", "Directives"]);
//# sourceMappingURL=app.js.map
