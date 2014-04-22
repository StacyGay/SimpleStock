/// <reference path="../reference.ts" />
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
//# sourceMappingURL=AccountController.js.map
