using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace HuntersForum.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
