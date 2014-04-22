/// <reference path="reference.ts" />
angular.module("SimpleStock", ["Controllers", "Services", "Directives"])
   /* .config(["$routeProvider",
        ($routeProvider: ng.route.IRouteProvider) => {
            $routeProvider
                .when("/", {
                    templateUrl: "views/main.html",
                    controller: "MainCtrl",
                    resolve: {
                        entries: [
                            "Restangular", (Restangular: Restangular)=> {
                                return Restangular.all("entries").getList();
                            }
                        ]
                    }
                })
                .otherwise("/");
        }]);*/