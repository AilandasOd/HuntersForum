using HuntersForum.Data;
using HuntersForum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace HuntersForum.Controllers
{
    [Route("api/topics/{topicId}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ForumContext _context;

        public PostsController(ForumContext context)
        {
            _context = context;
        }

        // Get all posts for a specific topic
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByTopic(int topicId)
        {
            return await _context.Posts.Where(p => p.TopicId == topicId).ToListAsync();
        }

        // Get a single post by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int topicId, int id)
        {
            var post = await _context.Posts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id && p.TopicId == topicId);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // Create a new post in a specific topic
        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost(int topicId, Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            post.TopicId = topicId;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { topicId = topicId, id = post.Id }, post);
        }

        // Update a post
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int topicId, int id, Post post)
        {
            if (id != post.Id || post.TopicId != topicId)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete a post
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int topicId, int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null || post.TopicId != topicId)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
