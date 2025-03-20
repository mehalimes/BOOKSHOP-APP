using bookshop.webapi.Contexts;
using bookshop.webapi.Interfaces;
using bookshop.webapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace bookshop.webapi.Controllers
{
    [ApiController]
    [Route("")]
    public class GoogleApiController(IHttpClientService httpService, AppDbContext db) : ControllerBase
    {
        [HttpGet("getBooksFromApi")]
        public async Task<ActionResult> GetBooksFromApi()
        {
            if (httpService.GetBooksFromGoogle().Result)
            {
                return Ok("Books has been fetched from Google successfully.");
            }
            else
            {
                return BadRequest("Books has not been fetched successfully.");
            }
        }
    }
}
