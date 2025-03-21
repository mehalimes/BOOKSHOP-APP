using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.webapi.Models.OrderFolder
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<OrderItem> Items { get; set; }
        public AppUser User { get; set; }
        public float TotalPrice { get; set; }
    }
}
