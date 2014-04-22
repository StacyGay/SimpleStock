/// <reference path="../reference.ts" />
module Services {
    // https://github.com/borisyankov/DefinitelyTyped/tree/master/angularjs resource documentation

    export interface IEntityResourceDescriptor extends ng.resource.IActionDescriptor {
        method: string;
        isArray?: boolean;
        params?: any;
        headers?: any;
        url?: string;
    }

    export interface IResourceParameters {
        id: number;
    }

    export interface IEntityResource<TEntity> {
        // extra api methods here
        $update(): TEntity;
    }

    export interface IEntityResourceClass<TResource extends ng.resource.IResource<TResource>>
    extends ng.resource.IResourceClass<TResource> {
        // overloads and extra api method here
        update(params: IResourceParameters, data: any): TResource;
    }

    export interface IServiceAuth {
        Username: string;
        Password: string;
    }

    export class EntityResourceFactory<TResource extends ng.resource.IResource<TResource>> {
        private $resource: ng.resource.IResourceService;

        static $inject = ["$resource"];
        constructor($resource: ng.resource.IResourceService) {
            this.$resource = $resource;
        }

        public GetEntityResource(url: string, auth: IServiceAuth = null): IEntityResourceClass<TResource> {
            var entityResource
                = this.$resource<TResource, IEntityResourceClass<TResource>>
                    (url, null, this.GetDescriptors(auth));

            return entityResource;
        }

        private GetDescriptors(auth: IServiceAuth = null): any {
            var authHeaders: any = auth == null ?
                null : { 'Authorization': 'Basic ' + btoa(auth.Username + ':' + auth.Password) };
            var queryDescriptor: IEntityResourceDescriptor = {
                method: 'GET',
                headers: authHeaders,
                isArray: true
            };
            var getDescriptor: IEntityResourceDescriptor = {
                method: 'GET',
                headers: authHeaders,
                isArray: false
            };
            var saveDescriptor: IEntityResourceDescriptor = {
                method: 'POST',
                headers: authHeaders,
                isArray: false
            };
            var updateDescriptor: IEntityResourceDescriptor = {
                method: 'PUT',
                headers: authHeaders
            };
            var deleteDescriptor: IEntityResourceDescriptor = {
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
        }
    }

    angularServices.service("EntityResourceFactory", EntityResourceFactory);
}


 