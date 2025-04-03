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

        public async Task<Payment> MakePayment(PaymentBody param)
        {
            AppUser user = _db.Users
                .Include(user => user.Cart)
                .ThenInclude(cart => cart.Items)
                .FirstOrDefault(user => user.Email == param.Email)!;

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
            paymentCard.CardHolderName = param.CardHolderName;
            paymentCard.CardNumber = param.CardNumber;
            paymentCard.ExpireMonth = param.ExpireMonth;
            paymentCard.ExpireYear = param.ExpireYear;
            paymentCard.Cvc = param.CVC;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = param.Name;
            buyer.Surname = param.Surname;
            buyer.GsmNumber = "+905350000000";
            buyer.Email = param.Email;
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
            shippingAddress.ContactName = param.Name + " " + param.Surname;
            shippingAddress.City = param.City;
            shippingAddress.Country = param.Country;
            shippingAddress.Description = param.AddressDescription;
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = param.Name + " " + param.Surname;
            billingAddress.City = param.City;
            billingAddress.Country = param.Country;
            billingAddress.Description = param.AddressDescription;
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

        public async Task<Refund> MakeRefund(string PaymentId, string Price)
        {
            Options options = new Options();
            options.ApiKey = apiKey;
            options.SecretKey = secretKey;
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreateAmountBasedRefundRequest request = new CreateAmountBasedRefundRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "--";
            request.Ip = "85.34.78.112";
            request.Price = Price;
            request.PaymentId = PaymentId;

            Refund amountBasedRefund = await Refund.CreateAmountBasedRefundRequest(request, options);
            return amountBasedRefund;
        }
    }
}
