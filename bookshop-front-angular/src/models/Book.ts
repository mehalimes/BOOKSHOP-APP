export class Book {
    public id: number = 0;
    public isbn_13: string = "";
    public publicId: string = "";
    public author: string = "";
    public price: number = 0;
    public title: string = "";
    public subTitle?: string = "";

    constructor(object: any) {
        this.id = object.id;
        this.isbn_13 = object.isbN_13;
        this.publicId = object.publicId;
        this.author = object.author;
        this.price = object.price;
        this.title = object.title;
        this.subTitle = object.subTitle;
    }
}