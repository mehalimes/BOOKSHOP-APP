export class BookComment {
    public id: number = 0;
    public email: string = "";
    public bookId: number = 0;
    public content: string = "";

    constructor(object: any) {
        this.id = object.id;
        this.email = object.email;
        this.bookId = object.bookId;
        this.content = object.content;
    }
}