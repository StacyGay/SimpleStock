/// <reference path="../reference.ts" />
module Models {
    export interface IAccount {
        Company: ICompany;
        User: IUser;
        Stores: IStore[];
    }
} 