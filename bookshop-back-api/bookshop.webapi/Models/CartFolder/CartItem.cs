using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.webapi.Models.CartFolder
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Book Book { get; set; }
        public Cart Cart { get; set; }
        public int Quantity { get; set; }
    }
}
