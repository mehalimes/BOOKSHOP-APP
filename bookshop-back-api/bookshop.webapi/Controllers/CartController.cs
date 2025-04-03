using bookshop.webapi.Contexts;
using bookshop.webapi.Dtos;
using bookshop.webapi.Dtos.CartDtos;
using bookshop.webapi.Models;
using bookshop.webapi.Models.CartFolder;
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

            user.Cart.CalculateTotalPrice();
            db.Entry(user.Cart).Property(c => c.TotalPrice).IsModified = true;
            await db.SaveChangesAsync();

            return Ok("Book has been added successfully.");
        }

        [HttpPost("getCartOfAUser")]
        public async Task<ActionResult<List<CartDto>>> GetCartOfAUser([FromBody] GetCartRequest request)
        {
            AppUser user = await db.Users
                .Include(user => user.Cart)
                .ThenInclude(cart => cart.Items)
                .ThenInclude(ci => ci.Book)
                .FirstOrDefaultAsync(user => user.Email == request.Email);


            user.Cart.CalculateTotalPrice();
            db.Entry(user.Cart).Property(c => c.TotalPrice).IsModified = true;
            await db.SaveChangesAsync();

            List<CartItemDto> cartItems = user.Cart.Items.Select(x => new CartItemDto
            {
                Id = x.Id,
                Book = new BookDto
                {
                    Id = x.Book.Id,
                    ISBN_13 = x.Book.ISBN_13,
                    PublicId = x.Book.PublicId,
                    Author = x.Book.Author,
                    Price = x.Book.Price,
                    Title = x.Book.Title,
                    SubTitle = x.Book.SubTitle
                },
                Quantity = x.Quantity,
            }).ToList();

            float totalPrice = user.Cart.TotalPrice;

            CartDto cart = new CartDto
            {
                Id = user.Cart.Id,
                CartItems = cartItems,
                TotalPrice = totalPrice
            };

            return Ok(cart);
        }

        [HttpPost("removeCartItem")]
        public async Task<ActionResult<string>> RemoveCartItem([FromBody] RemoveCartItemRequest request)
        {
            AppUser user = await db.Users
                .Include(user => user.Cart)
                .ThenInclude(c => c.Items)
                .ThenInclude(ci => ci.Book)
                .FirstOrDefaultAsync(user => user.Email == request.Email);

            CartItem cartItem = db.CartItems.FirstOrDefault(item => item.Id == request.CartItemId);

            if(cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
            }
            else if(cartItem.Quantity == 1)
            {
                db.CartItems.Remove(cartItem);
            }

            user.Cart.CalculateTotalPrice();
            db.Entry(user.Cart).Property(c => c.TotalPrice).IsModified = true;

            await db.SaveChangesAsync();

            return Ok("Cart item has been removed successfully.");
        }
    }
}
