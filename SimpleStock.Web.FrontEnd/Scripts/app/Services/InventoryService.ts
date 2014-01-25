/// <reference path="../reference.ts" />
module Services {
    export interface IInventoryParameters {
        id: number;
    }

    export interface IInventoryResource extends Models.IInventory, ng.resource.IResource<IInventoryResource> {
        // extra api methods here
        $update(): void;
    }

    export interface IInventoryResourceClass extends ng.resource.IResourceClass<IInventoryResource> {
        // overload with custom parameters here
        // add extra api methods here
        update(params: IInventoryParameters, data: Models.IInventory);
    }

    export class InventoryService
    {
        public Resource: IInventoryResourceClass;
        static $inject = ["$resource"];
        constructor($resource: ng.resource.IResourceService) {
            var updateDescriptor: ng.resource.IActionDescriptor = {
                method: "PUT",
                headers: { 'auth-token': 'C3PO R2D2' }
            };

            this.Resource = $resource<IInventoryResource, IInventoryResourceClass>('/api/Inventory/:id', null,
                {
                    'update': updateDescriptor
                });
        }      
    }

    angularServices.service("InventoryService", InventoryService);
} 