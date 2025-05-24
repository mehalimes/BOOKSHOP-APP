import { OrderItem } from "./OrderItem";

export class Order {
    public id: number = 0;
    public items: OrderItem[] = [];
    public totalPrice: number = 0;
    public address: string = "";
    public country: string = "";
    public city: string = "";
    public paymentId: string = "";
    public userName?: string = "";
    public email?: string = "";

    constructor(object: any) {
        this.id = object.id;
        this.items = object.items;
        this.totalPrice = object.totalPrice;
        this.address = object.address;
        this.country = object.country;
        this.city = object.city;
        this.paymentId = object.paymentId;
        this.email = object.user?.email;
        this.userName = object.user?.userName;
    }
}