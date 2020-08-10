namespace Blog.API.Data.Entities
{
    public class CategoryEntity : Entity<int>
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
    }
}