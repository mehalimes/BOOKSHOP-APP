using bookshop.webapi.Contexts;
using bookshop.webapi.Services.Google;
using Microsoft.AspNetCore.Mvc;

namespace bookshop.webapi.Controllers
{
    [ApiController]
    [Route("")]
    public class GoogleApiController(GoogleApiService googleService, AppDbContext db) : ControllerBase
    {
        [HttpGet("getBooksFromApi")]
        public async Task<ActionResult> GetBooksFromApi()
        {
            if (googleService.GetBooksFromGoogleAndSaveToDatabase().Result)
            {
                return Ok("Books has been fetched from Google and saved to database successfully.");
            }
            else
            {
                return BadRequest("Books has not been fetched successfully.");
            }
        }
    }
}
