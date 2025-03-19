namespace bookshop.webapi.Dtos
{
    public class CartDto
    {
        public int Id { get; set; }
        public List<CartItemDto> CartItems { get; set; }
        public float TotalPrice { get; set; }
    }
}
