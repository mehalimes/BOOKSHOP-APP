namespace bookshop.webapi.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }

    public class CartDto
    {
        public List<BookDto> Books { get; set; }
        public float Price { get; set; }
    }
}
