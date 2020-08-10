using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blog.Client.ViewModels
{
    public class AuthorViewModel
    {
        public string FullName { get; set; }

        public string Biography { get; set; }

        public string ProfileImage { get; set; }

        public Dictionary<string, string> SocialLinks { get; set; } = new Dictionary<string, string>();
    }
}