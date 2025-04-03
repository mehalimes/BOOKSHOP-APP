namespace bookshop.webapi.Dtos.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public List<OrderItemDto> Items { get; set; }
        public float TotalPrice { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PaymentId { get; set; }
    }
}
