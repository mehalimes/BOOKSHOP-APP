import { Book } from "./Book";

export class CartItem {
    public id: number = 0;
    public quantity: number = 0;
    public book: Book | null = null;

    constructor(id: number, quantity: number, book: any) {
        this.id = id;
        this.quantity = quantity;
        this.book = new Book(book);
    }
}