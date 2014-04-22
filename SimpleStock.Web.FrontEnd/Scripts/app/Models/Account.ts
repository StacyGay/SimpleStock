/// <reference path="../reference.ts" />
module Models {
    export interface IAccount
        extends ng.resource.IResource<IAccount>,
        Services.EntityResourceFactory<IAccount> {
        Company: ICompany;
        User: IUser;
        Stores: IStore[];
    }
} 