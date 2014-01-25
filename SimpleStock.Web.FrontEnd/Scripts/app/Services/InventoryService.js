/// <reference path="../reference.ts" />
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
//# sourceMappingURL=InventoryService.js.map
