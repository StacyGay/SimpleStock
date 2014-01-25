/// <reference path="../reference.ts" />
module Models {
    export interface ILogin {
        Email: string;
        Password: string;    
    }

    export interface IUser {
        Id: number;
        Password: string;
        Email: string;
        FirstName: string;
        LastName: string;
        CompanyId: number;
    }
} 