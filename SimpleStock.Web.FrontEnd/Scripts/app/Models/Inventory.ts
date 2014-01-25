module Models {
    export interface IInventory {
        Id: number;
        Date: Date;
        Amount: number;
        Sold: number;
        Lost: number;
        ProductId: number;
    }
} 