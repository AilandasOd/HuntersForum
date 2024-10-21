using System.Text.Json.Serialization;

namespace HuntersForum.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }
    }
}
