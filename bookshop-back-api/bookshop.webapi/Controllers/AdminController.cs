using bookshop.webapi.Contexts;
using bookshop.webapi.Dtos;
using bookshop.webapi.Dtos.OrderDtos;
using bookshop.webapi.Models;
using bookshop.webapi.Models.OrderFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookshop.webapi.Controllers
{
    [Route("")]
    [ApiController]
    public class AdminController(
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager,
        AppDbContext db
    ) : ControllerBase
    {
        public record AdminLoginRequest(string Email, string Password);

        [HttpPost("adminLogin")]
        public async Task<ActionResult<AppUser>> AdminLogin([FromBody] AdminLoginRequest request)
        {
            AppUser user = await userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return NotFound("Admin user could not be found.");

            var result = await signInManager.PasswordSignInAsync(user, request.Password, true, false);

            if (user.IsAdmin && result.Succeeded)
                return Ok(user);

            return BadRequest("Admin login unsuccessfull.");
        }

        [HttpGet("getOrdersOfAllUsers")]
        public async Task<ActionResult<List<Order>>> GetOrdersOfAllUsers()
        {
            List<Order> orders = db.Orders
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .ToList();

            List<OrderDto> orderDtos = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                User = new AppUserDto
                {
                    Id  = o.User.Id,
                    Email = o.User.Email,
                    UserName = o.User.UserName
                },
                Items = o.Items.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    Book = new BookDto
                    {
                        Id = oi.Book.Id,
                        ISBN_13 = oi.Book.ISBN_13,
                        PublicId = oi.Book.PublicId,
                        Author = oi.Book.Author,
                        Price = oi.Book.Price,
                        Title = oi.Book.Title,
                        SubTitle = oi.Book.SubTitle
                    },
                    Quantity = oi.Quantity
                }).ToList(),
                TotalPrice = o.TotalPrice,
                Address = o.Address,
                Country = o.Country,
                City = o.City,
                PaymentId = o.PaymentId
            }).ToList();

            return Ok(orderDtos);
        }
    }
}