/// <reference path="../reference.ts" />
var Services;
(function (Services) {
    var ResourceConfig = (function () {
        function ResourceConfig() {
            this.GenerateDescriptors();
            this.ResourceDescriptors = {
                'query': this.queryDescriptor,
                'post': this.postDescriptor,
                'update': this.updateDescriptor,
                'delete': this.deleteDescriptor
            };
        }
        ResourceConfig.prototype.GenerateDescriptors = function () {
            this.queryDescriptor = {
                method: "GET",
                isArray: true,
                header: undefined,
                url: ""
            };

            this.postDescriptor = {
                method: "POST",
                isArray: true,
                header: undefined,
                url: ""
            };

            this.updateDescriptor = {
                method: "PUT",
                isArray: true,
                header: undefined,
                url: ""
            };

            this.deleteDescriptor = {
                method: "DELETE",
                isArray: true,
                header: undefined,
                url: ""
            };
        };
        return ResourceConfig;
    })();
    Services.ResourceConfig = ResourceConfig;

    angularServices.service("ResourceConfig", ResourceConfig);
})(Services || (Services = {}));
//# sourceMappingURL=ResourceConfig.js.map
