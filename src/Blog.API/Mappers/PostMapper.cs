using Blog.API.Data.Entities;
using Blog.Contract;
using Google.Protobuf.WellKnownTypes;

namespace Blog.API.Mappers
{
    public class PostMapper : IPostMapper
    {
        public PostEntity Map(Post model)
        {
            throw new System.NotImplementedException();
        }

        public Post Map(PostEntity entity)
        {
            return new Post()
            {
                Id = entity.Id.ToString(),
                Content = entity.Content,
                Description = entity.Description,
                Title = entity.Title,
                Slug = entity.Slug,
                PublishedDate = entity.PublishedDate.HasValue
                    ? Timestamp.FromDateTime(entity.PublishedDate.Value.ToUniversalTime())
                    : null,
                CoverImageUrl = entity.CoverImageUrl,
                Tags = entity.Tags
            };
        }

        public Post Map(PostEntity postEntity, CategoryEntity categoryEntity)
        {
            var post = Map(postEntity);
            post.CategorySlug = categoryEntity.Slug;
            post.CategoryName = categoryEntity.Name;
            
            return post;
        }
    }
}