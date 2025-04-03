namespace bookshop.webapi.Dtos.OrderDtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public BookDto Book { get; set; }
        public int Quantity { get; set; }
    }
}
