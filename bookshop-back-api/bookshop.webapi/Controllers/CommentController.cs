using bookshop.webapi.Contexts;
using bookshop.webapi.Dtos;
using bookshop.webapi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bookshop.webapi.Controllers
{
    [Route("")]
    [ApiController]
    public class CommentController(UserManager<AppUser> userManager, AppDbContext db) : ControllerBase
    {
        public record BookIdRequest(int BookId);
        public record AddCommentRequest(string Email, int BookId, string Content);
        public record RemoveCommentRequest(int CommentId);

        [HttpPost("getAllCommentsOfABook")]
        public ActionResult<List<CommentDto>> GetAllCommentsOfABook([FromBody] BookIdRequest request)
        {
            List<CommentDto> comments = db.Comments
                .Include(c => c.Book)
                .Include(c => c.User)
                .Where(c => c.Book.Id == request.BookId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    Email = c.User.Email,
                    BookId = c.Book.Id
                }).ToList();

            return Ok(comments);
        }

        [HttpPost("addCommentToABook")]
        public async Task<ActionResult> AddCommentToABook([FromBody] AddCommentRequest request)
        {
            AppUser currentUser = await userManager.FindByEmailAsync(request.Email);
            Book currentBook = await db.Books.FindAsync(request.BookId);

            if(currentUser is null)
            {
                return NotFound("User could not be found.");
            }
            if(request.Content == "")
            {
                return BadRequest("Content cannot be empty.");
            }

            db.Comments.Add(new Comment
            {
                Book = currentBook,
                User = currentUser,
                Content = request.Content
            });

            db.SaveChanges();

            return Ok("Comment has been added successfully.");
        }

        [HttpPost("removeAComment")]
        public ActionResult RemoveAComment([FromBody] RemoveCommentRequest request)
        {
            Comment existingComment = db.Comments.Find(request.CommentId);
            db.Comments.Remove(existingComment);
            db.SaveChanges();
            return Ok("Comment has been removed successfully.");
        }
    }
}
