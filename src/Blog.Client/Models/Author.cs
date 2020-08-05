using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog.Client.Models
{
    public class Author
    {
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        
        [JsonPropertyName("biography")]
        public string Biography { get; set; }
        
        [JsonPropertyName("profileImage")]
        public string ProfileImage { get; set; }

        [JsonPropertyName("socialLinks")]
        public Dictionary<string,string> SocialLinks { get; set; } = new Dictionary<string, string>();
    }
}