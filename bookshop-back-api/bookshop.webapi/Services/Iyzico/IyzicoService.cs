using bookshop.webapi.Contexts;
using bookshop.webapi.Models;
using bookshop.webapi.Records;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace bookshop.webapi.Services.Iyzico
{
    public class IyzicoService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;
        private readonly string apiKey;
        private readonly string secretKey;
        public IyzicoService(AppDbContext db, IConfiguration config) 
        {
            _db = db;
            _config = config;
            apiKey = _config["Iyzipay:ApiKey"];
            secretKey = _config["Iyzipay:SecretKey"];
        }

        public async Task<Payment> Pay(PaymentBody requestBody)
        {
            AppUser user = _db.Users
                .Include(user => user.Cart)
                .ThenInclude(cart => cart.Items)
                .FirstOrDefault(user => user.Email == requestBody.Email)!;

            Options options = new Options();
            options.ApiKey = apiKey;
            options.SecretKey = secretKey;
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = "1";
            request.PaidPrice = user.Cart.TotalPrice.ToString(CultureInfo.InvariantCulture);
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = requestBody.CardHolderName;
            paymentCard.CardNumber = requestBody.CardNumber;
            paymentCard.ExpireMonth = requestBody.ExpireMonth;
            paymentCard.ExpireYear = requestBody.ExpireYear;
            paymentCard.Cvc = requestBody.CVC;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = requestBody.Name;
            buyer.Surname = requestBody.Surname;
            buyer.GsmNumber = "+905350000000";
            buyer.Email = requestBody.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = requestBody.Name + " " + requestBody.Surname;
            shippingAddress.City = requestBody.City;
            shippingAddress.Country = requestBody.Country;
            shippingAddress.Description = requestBody.AddressDescription;
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = requestBody.Name + " " + requestBody.Surname;
            billingAddress.City = requestBody.City;
            billingAddress.Country = requestBody.Country;
            billingAddress.Description = requestBody.AddressDescription;
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "0.3";
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "BI102";
            secondBasketItem.Name = "Game code";
            secondBasketItem.Category1 = "Game";
            secondBasketItem.Category2 = "Online Game Items";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = "0.5";
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new BasketItem();
            thirdBasketItem.Id = "BI103";
            thirdBasketItem.Name = "Usb";
            thirdBasketItem.Category1 = "Electronics";
            thirdBasketItem.Category2 = "Usb / Cable";
            thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            thirdBasketItem.Price = "0.2";
            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            Payment payment = await Payment.Create(request, options);

            return payment;
        }

        public async Task<bool> Refund()
        {
            return true;
        }
    }
}
