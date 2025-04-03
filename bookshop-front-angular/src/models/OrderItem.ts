import { Book } from "./Book";

export class OrderItem {
    public id: number = 0;
    public book: Book | null = null;
    public quantity: number = 0;

    constructor(object: any) {
        this.id = object.id;
        this.book = object.book;
        this.quantity = object.quantity;
    }
}