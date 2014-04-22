/// <reference path="../reference.ts" />
module Controllers {
    export class AccountController {
        public Account: Models.IAccount;
        private _accountService: Services.IEntityResourceClass<Models.IAccount>;

        static $inject = ["$scope", "EntityResourceFactory"];
        constructor($scope, resourceFactory: Services.EntityResourceFactory<Models.IAccount>) {
            $scope.vm = this;
            this._accountService = resourceFactory.GetEntityResource("/api/account/:id");
            this.Account.Stores = [];
            this.Account.Stores.push({Id: 0, Name: "", CompanyId: 0});
        }

        public CreateAccount(): void {
            console.log("testing create");
            this.Account = this._accountService.save({},this.Account,()=> {
                
            }),
            ()=> {
                
            };
        }
    }
} 