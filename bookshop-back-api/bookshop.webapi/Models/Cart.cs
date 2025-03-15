using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.webapi.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<CartItem> Items { get; set; }
        public AppUser User { get; set; }
        public float TotalPrice => Items.Sum(ci => ci.Quantity * ci.Book.Price);
    }
}
