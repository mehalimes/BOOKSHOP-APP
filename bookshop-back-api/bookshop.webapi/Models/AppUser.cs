using bookshop.webapi.Models.CartFolder;
using bookshop.webapi.Models.OrderFolder;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookshop.webapi.Models
{
    public class AppUser : IdentityUser<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        public List<Comment> Comments { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public List<Order> Orders { get; set; }
        public bool IsAdmin { get; set; }
    }
}
