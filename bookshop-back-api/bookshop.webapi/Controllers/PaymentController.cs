using bookshop.webapi.Contexts;
using bookshop.webapi.Models;
using bookshop.webapi.Records;
using bookshop.webapi.Services.Iyzico;
using Iyzipay.Model;
using Iyzipay.Request.V2.Subscription;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookshop.webapi.Controllers
{
    [Route("")]
    [ApiController]
    public class PaymentController(AppDbContext db, IyzicoService iyzicoService) : ControllerBase
    {
        [HttpPost("makePayment")]
        public async Task<ActionResult> MakePayment([FromBody] PaymentBody requestBody)
        {
            Task<Payment> payment = iyzicoService.Pay(requestBody);
            if (!payment.IsCompletedSuccessfully)
            {
                return BadRequest("Payment unsuccessfull.");
            }

            AppUser user = await db.Users
                .Include(user => user.Cart)
                .ThenInclude(cart => cart.Items)
                .FirstOrDefaultAsync(user => user.Email == requestBody.Email);



        }
    }
}
