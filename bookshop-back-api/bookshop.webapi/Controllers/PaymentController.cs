using bookshop.webapi.Interfaces;
using bookshop.webapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace bookshop.webapi.Controllers
{
    [Route("")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IHttpClientService _httpClientService;
        private string apiKey = "myapikey";
        private string secretKey = "mysecretkey";
        private string baseUrl = "https://sandbox-api.iyzipay.com/payment/auth";
        private string jsonFilePath;
        private string json;

        public PaymentController(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            this.jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "JSON", "PaymentBody.json");
            this.jsonFilePath = Path.GetFullPath(jsonFilePath);
            this.json = System.IO.File.ReadAllText(jsonFilePath);
        }
        public record PaymentRequest(string Email);

        [HttpPost("makePayment")]
        public async Task<ActionResult<string>> MakePayment([FromBody] PaymentRequest paymentRequest)
        {
            PaymentBody body = JsonSerializer.Deserialize<PaymentBody>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, NumberHandling = JsonNumberHandling.AllowReadingFromString });
            return Ok("hello");
        }
    }
}
