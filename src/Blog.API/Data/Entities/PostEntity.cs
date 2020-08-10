using System;

namespace Blog.API.Data.Entities
{
    public class PostEntity : Entity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public string CoverImageUrl { get; set; }
        public string Tags { get; set; }
        public PostStatus Status { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int CategoryId { get; set; }
    }

    public enum PostStatus
    {
        Published = 1,
        Draft = 2,
        Scheduled = 3,
    }
}