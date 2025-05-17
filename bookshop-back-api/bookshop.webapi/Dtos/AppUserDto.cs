using bookshop.webapi.Models.CartFolder;
using bookshop.webapi.Models.OrderFolder;
using bookshop.webapi.Models;

namespace bookshop.webapi.Dtos
{
    public class AppUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
