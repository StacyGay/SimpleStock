/// <reference path="../reference.ts" />
module Services {
    interface IApiActionDescriptor {
        method: string;
        isArray: boolean;
        params: any;
        header: any;
        url: string;    
    }

    export class ResourceConfig {
        private queryDescriptor: IApiActionDescriptor;
        private postDescriptor: IApiActionDescriptor;
        private updateDescriptor: IApiActionDescriptor;
        private deleteDescriptor: IApiActionDescriptor;
        
        public ResourceDescriptors: any;

        constructor() {
            this.GenerateDescriptors();
            this.ResourceDescriptors =
            {
                'query': this.queryDescriptor, 
                'post': this.postDescriptor,
                'update': this.updateDescriptor,
                'delete': this.deleteDescriptor
            };
        }

        private GenerateDescriptors(): void {
            this.queryDescriptor = {
                method: "GET",
                isArray: true,
                params: undefined,
                header: undefined,
                url: ""    
            };

            this.postDescriptor = {
                method: "POST",
                isArray: true,
                params: undefined,
                header: undefined,
                url: ""
            };

            this.updateDescriptor = {
                method: "PUT",
                isArray: true,
                params: undefined,
                header: undefined,
                url: ""
            };

            this.deleteDescriptor = {
                method: "DELETE",
                isArray: true,
                params: undefined,
                header: undefined,
                url: ""
            };
        }
    }

    angularServices.service("ResourceConfig", ResourceConfig);
} 