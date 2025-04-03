using bookshop.webapi.Contexts;
using bookshop.webapi.Dtos;
using bookshop.webapi.Dtos.OrderDtos;
using bookshop.webapi.Models;
using bookshop.webapi.Models.OrderFolder;
using bookshop.webapi.Records;
using bookshop.webapi.Services.Iyzico;
using Iyzipay.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace bookshop.webapi.Controllers
{
    [Route("")]
    [ApiController]
    public class OrderController(AppDbContext db, IyzicoService iyzicoService) : ControllerBase
    {
        public record CancelOrderRequest(int OrderId);
        public record GetAllOrdersRequest(string Email);

        [HttpPost("createOrder")]
        public async Task<ActionResult> CreateOrder([FromBody] PaymentBody request) 
        {
            AppUser user = await db.Users
                .Include(user => user.Cart)
                .ThenInclude(cart => cart.Items)
                .ThenInclude(item => item.Book)
                .Include(user => user.Orders)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            Payment payment = await iyzicoService.MakePayment(request);

            if (!(payment.Status == Status.SUCCESS.ToString()))
            {
                return BadRequest("Payment failed.");
            }

            user.Orders.Add(new Order
            {
                Items = user.Cart.Items.Select(item => new Models.OrderFolder.OrderItem
                {
                    Book = item.Book,
                    Quantity = item.Quantity,
                }).ToList(),
                User = user,
                TotalPrice = user.Cart.TotalPrice,
                Address = request.AddressDescription,
                Country = request.Country,
                City = request.City,
                PaymentId = payment.PaymentId
            });

            user.Cart.Items.RemoveAll(item => true);
            user.Cart.CalculateTotalPrice();
            db.Entry(user.Cart).Property(c => c.TotalPrice).IsModified = true;
            await db.SaveChangesAsync();
            return Ok(payment);
        }

        [HttpPost("cancelOrder")]
        public async Task<ActionResult> CancelOrder([FromBody] CancelOrderRequest request) 
        {
            Order order = await db.Orders.FindAsync(request.OrderId);

            Refund refund = await iyzicoService.MakeRefund(order.PaymentId, order.TotalPrice.ToString(CultureInfo.GetCultureInfo("en-US")));

            if (!(refund.Status == Status.SUCCESS.ToString()))
            {
                return BadRequest(refund);
            }

            db.Orders.Remove(db.Orders.Find(request.OrderId));

            await db.SaveChangesAsync();

            return Ok(refund);
        }

        [HttpPost("getAllOrders")]
        public ActionResult<List<OrderDto>> GetAllOrders([FromBody] GetAllOrdersRequest request)
        {
            AppUser user = db.Users
                .Include(u => u.Orders)
                .ThenInclude(o => o.Items)
                .ThenInclude(oi => oi.Book)
                .FirstOrDefault(u => u.Email == request.Email);

            List<OrderDto> orders = user.Orders.Select(o => new OrderDto
            {
                Id = o.Id,
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

            return Ok(orders);
        }
    }
}
