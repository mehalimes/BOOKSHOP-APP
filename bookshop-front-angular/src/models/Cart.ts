import { CartItem } from "./CartItem";

export class Cart {
    public id: number = 0;
    public totalPrice: number = 0;
    public cartItems: CartItem[] = [];

    constructor(id: number, totalPrice: number, cartItems: CartItem[]) {
        this.id = id;
        this.totalPrice = totalPrice;
        this.cartItems = cartItems;
    }
}