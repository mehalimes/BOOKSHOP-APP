using bookshop.webapi.Contexts;
using bookshop.webapi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace bookshop.webapi.Controllers
{
    [Route("")]
    [ApiController]
    public class CartController(UserManager<AppUser> userManager, AppDbContext db) : ControllerBase
    {
        public record AddToCartRequest();

        [HttpPost("addToCart")]
        public async Task<ActionResult> AddToCart()
        {
            return Ok();
        }
    }
}
