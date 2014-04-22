/// <reference path="../reference.ts" />
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
//# sourceMappingURL=EntityResourceFactory.js.map
