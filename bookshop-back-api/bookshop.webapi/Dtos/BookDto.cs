namespace bookshop.webapi.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string ISBN_13 { get; set; }
        public string PublicId { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }
}
