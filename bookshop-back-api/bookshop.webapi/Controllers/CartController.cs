using bookshop.webapi.Contexts;
using bookshop.webapi.Dtos;
using bookshop.webapi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookshop.webapi.Controllers
{
    [Route("")]
    [ApiController]
    public class CartController(UserManager<AppUser> userManager, AppDbContext db) : ControllerBase
    {
        public record AddToCartRequest(int BookId, string Email);
        public record GetCartRequest(string Email);
        public record RemoveCartItemRequest(int CartItemId, string Email);

        [HttpPost("addToCart")]
        public async Task<ActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            AppUser user = await db.Users
                .Include(user => user.Cart)
                .ThenInclude(cart => cart.Items)
                .ThenInclude(items => items.Book)
                .FirstOrDefaultAsync(user => user.Email == request.Email);

            if (user is null)
                return NotFound("User could not be found.");

            Book book = db.Books.Find(request.BookId);

            if (book is null)
                return NotFound("Book could not be found.");

            CartItem currentCartItem = user.Cart?.Items?.Find(item => item.Book.Id == book.Id);

            if (currentCartItem is null)
            {
                user.Cart.Items.Add(new CartItem { Book = book, Quantity = 1, Cart = user.Cart });
            }
            else
            {
                currentCartItem.Quantity += 1;
            }

            user.Cart.UpdateTotalPrice();
            await db.SaveChangesAsync();

            return Ok("Book has been added successfully.");
        }

        [HttpPost("getCartOfAUser")]
        public async Task<ActionResult<CartDto>> GetCartOfAUser([FromBody] GetCartRequest request)
        {
            AppUser user = await db.Users
                .Include(user => user.Cart)
                .ThenInclude(cart => cart.Items)
                .ThenInclude(ci => ci.Book)
                .FirstOrDefaultAsync(user => user.Email == request.Email);

            user.Cart.UpdateTotalPrice();
            await db.SaveChangesAsync();

            CartDto cartResponse = new CartDto
            {
                Books = user.Cart.Items.Select(item => new BookDto { Id = item.Book.Id, Quantity = item.Quantity }).ToList(),
                Price = user.Cart.TotalPrice
            };

            return Ok(cartResponse);
        }

        [HttpPost("removeCartItem")]
        public async Task<ActionResult<bool>> RemoveCartItem([FromBody] RemoveCartItemRequest request)
        {
            AppUser user = await db.Users
                .Include(user => user.Cart)
                .ThenInclude(c => c.Items)
                .ThenInclude(ci => ci.Book)
                .FirstOrDefaultAsync(user => user.Email == request.Email);

            CartItem cartItem = db.CartItems.FirstOrDefault(item => item.Id == request.CartItemId);

            db.CartItems.Remove(cartItem);
            user.Cart.UpdateTotalPrice();

            await db.SaveChangesAsync();

            return Ok("Cart item has been removed successfully.");
        }
    }
}
