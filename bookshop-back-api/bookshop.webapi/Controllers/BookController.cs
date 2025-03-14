using bookshop.webapi.Contexts;
using bookshop.webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bookshop.webapi.Controllers
{
    [Route("")]
    [ApiController]
    public class BookController(AppDbContext db) : ControllerBase
    {
        public record GetBookByIdRequest(int Id);

        [HttpGet("getAllBooks")]
        public ActionResult<List<Book>> getAllBooks()
        {
            return db.Books.ToList();
        }

        [HttpPost("getBookById")]
        public ActionResult<Book> getBookById([FromBody] GetBookByIdRequest request)
        {
            Book currentBook = db.Books.Find(request.Id);

            if(currentBook is null)
                return BadRequest("Book could not be found.");
            else
                return Ok(currentBook);
        }
    }
}
