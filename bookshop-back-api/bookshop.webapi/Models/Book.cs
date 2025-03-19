using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.webapi.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ISBN_13 { get; set; }
        public string PublicId { get; set; }
        public string Author { get; set; }
        public float Price { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public List<Comment> Comments { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
