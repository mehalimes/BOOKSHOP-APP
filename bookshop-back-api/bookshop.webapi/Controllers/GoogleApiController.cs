using bookshop.webapi.Contexts;
using bookshop.webapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace bookshop.webapi.Controllers
{
    [ApiController]
    [Route("")]
    public class GoogleApiController(AppDbContext db) : ControllerBase
    {
        [HttpGet("getBooksFromApi")]
        public async Task<ActionResult> GetBooksFromApi()
        {
            string apiKey = "myapikey";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://www.googleapis.com/books/v1/volumes?q=design+patterns&key={apiKey}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                Console.WriteLine(content);

                GoogleApiResponse bookApiResponse = JsonSerializer.Deserialize<GoogleApiResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                bookApiResponse.Items.ForEach(item =>
                {
                    if(item.SaleInfo.Saleability != "NOT_FOR_SALE")
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
                        db.SaveChanges();
                    }
                });
                return Ok();
            }
            else
            {
                return BadRequest("Google Books API'den veri alınamadı.");
            }
        }
    }
}
