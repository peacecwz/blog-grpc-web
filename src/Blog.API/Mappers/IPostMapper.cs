using Blog.API.Data.Entities;
using Blog.Contract;

namespace Blog.API.Mappers
{
    public interface IPostMapper : IMapper<PostEntity, Post>
    {
        Post Map(PostEntity postEntity, CategoryEntity categoryEntity);
    }
}