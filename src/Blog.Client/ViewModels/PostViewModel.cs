using System;

namespace Blog.Client.ViewModels
{
    public class PostViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Slug { get; set; }
        public string Tags { get; set; }
        public string Content { get; set; }
        public string CategorySlug { get; set; }
        public string CategoryName { get; set; }
    }
}