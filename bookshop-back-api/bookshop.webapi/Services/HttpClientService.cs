using bookshop.webapi.Contexts;
using bookshop.webapi.Interfaces;
using bookshop.webapi.Models;
using System.Text.Json;

namespace bookshop.webapi.Services
{
    public class HttpClientService(IHttpClientFactory httpClientFactory, AppDbContext db) : IHttpClientService
    {
        public readonly HttpClient _httpClient = httpClientFactory.CreateClient();
        public async Task<bool> GetBooksFromGoogle()
        {
            string apiKey = "myapikey";
            HttpResponseMessage response = await _httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=design+patterns&key={apiKey}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                Console.WriteLine(content);

                GoogleApiResponse bookApiResponse = JsonSerializer.Deserialize<GoogleApiResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                bookApiResponse.Items.ForEach(item =>
                {
                    if (item.SaleInfo.Saleability != "NOT_FOR_SALE")
                    {
                        string ISBN = item.VolumeInfo.IndustryIdentifiers?[0].Identifier ?? "";
                        string publicId = item.VolumeInfo.ImageLinks.Thumbnail;
                        string author = item.VolumeInfo.Authors[0];
                        float price = (float)Convert.ToDouble(item.SaleInfo.RetailPrice.Amount);
                        string title = item.VolumeInfo.Title;
                        string subTitle = item.VolumeInfo.Subtitle ?? "";

                        Book newBook = new Book
                        {
                            ISBN_13 = ISBN,
                            PublicId = publicId,
                            Author = author,
                            Price = price,
                            Title = title,
                            SubTitle = subTitle,
                        };

                        db.Books.Add(newBook);
                    }
                });
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MakePayment()
        {
            return;
        }

        public void VerifyPayment()
        {
            return;
        }
    }
}
