/// <reference path="../reference.ts" />
module Controllers {
    export class MainController {
        static $inject = ["$scope"];
        constructor($scope) {
            $scope.MainVm = this;
        }
    }
} 