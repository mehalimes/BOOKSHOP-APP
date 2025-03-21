using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.webapi.Models.CartFolder
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<CartItem> Items { get; set; }
        public AppUser User { get; set; }
        public float TotalPrice { get; set; }

        public void CalculateTotalPrice()
        {
            TotalPrice = Items.Sum(item => item.Book.Price * item.Quantity);
        }
    }
}
