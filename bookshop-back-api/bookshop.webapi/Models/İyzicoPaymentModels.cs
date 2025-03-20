using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Client;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace bookshop.webapi.Models
{
    public class PaymentBody
    {
        [JsonPropertyName("installment")]
        public byte Installment { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("paidPrice")]
        public string PaidPrice { get; set; }

        [JsonPropertyName("paymentCard")]
        public PaymentCard PaymentCard { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("billingAddress")]
        public BillingAddress BillingAddress { get; set; }

        [JsonPropertyName("shippingAddress")]
        public ShippingAddress ShippingAddress { get; set; }

        [JsonPropertyName("buyer")]
        public Buyer Buyer { get; set; }

        [JsonPropertyName("basketItems")]
        public List<BasketItem> BasketItems { get; set; }

    }

    public class ShippingAddress
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("city")]
        public string City  { get; set; }

        [JsonPropertyName("contactName")]
        public string ContactName { get; set; }

    }

    public class BasketItem
    {
        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("category1")]
        public string Category1 { get; set; }

        [JsonPropertyName("category2")]
        public string Category2 { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("itemType")]
        public string ItemType { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

    }

    public class Buyer
    {
        [JsonPropertyName("registrationAddress")]
        public string RegistrationAddress { get; set; }

        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("identityNumber")]
        public string IdentityNumber { get; set; }

        [JsonPropertyName("surname")]
        public string Surname { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

    }

    public class BillingAddress
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("contactName")]
        public string ContactName { get; set; }

    }

    public class PaymentCard
    {
        [JsonPropertyName("cardHolderName")]
        public string CardHolderName { get; set; }

        [JsonPropertyName("cardNumber")]
        public string CardNumber { get; set; }

        [JsonPropertyName("expireYear")]
        public string ExpireYear { get; set; }

        [JsonPropertyName("expireMonth")]
        public string ExpireMonth { get; set; }

        [JsonPropertyName("cvc")]
        public string CVC { get; set; }

    }
}
