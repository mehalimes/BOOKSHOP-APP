using bookshop.webapi.Contexts;
using bookshop.webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookshop.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(AppDbContext db) : ControllerBase
    {
        public record BookId(int bookId);
        [HttpGet]
        public ActionResult<List<Comment>> getAllCommentsOfAProduct([FromBody] BookId request)
        {
            List<Comment> comments = db.Comments
                .Include(c => c.Book)
                .Where(c => c.Book.Id == request.bookId).ToList();
            return comments;
        }
    }
}
