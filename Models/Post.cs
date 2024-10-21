using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace HuntersForum.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        public int TopicId { get; set; }
        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
