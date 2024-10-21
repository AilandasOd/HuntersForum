using HuntersForum.Data;
using HuntersForum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace HuntersForum.Controllers
{
    [Route("api/topics/{topicId}/posts/{postId}/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ForumContext _context;

        public CommentsController(ForumContext context)
        {
            _context = context;
        }

        // Get all comments for a specific post
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByPost(int postId)
        {
            return await _context.Comments.Where(c => c.PostId == postId).AsNoTracking().ToListAsync();
        }

        // Get a single comment by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int topicId, int postId, int id)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id && c.PostId == postId);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // Create a new comment on a post
        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment(int topicId, int postId, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            comment.PostId = postId;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { topicId = topicId, postId = postId, id = comment.Id }, comment);
        }

        // Update a comment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int postId, int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            // Prijungti egzistuojantį Comment prie DbContext
            _context.Attach(comment);
            _context.Entry(comment).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete a comment
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int topicId, int postId, int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null || comment.PostId != postId)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
