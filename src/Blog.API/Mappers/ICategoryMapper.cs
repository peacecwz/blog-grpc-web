using Blog.API.Data.Entities;
using Blog.Contract;

namespace Blog.API.Mappers
{
    public interface ICategoryMapper : IMapper<CategoryEntity, Category>
    {
    }

    public class CategoryMapper : ICategoryMapper
    {
        public CategoryEntity Map(Category model)
        {
            throw new System.NotImplementedException();
        }

        public Category Map(CategoryEntity entity)
        {
            return new Category
            {
                Id = entity.Id,
                Name = entity.Name,
                Slug = entity.Slug,
                ImageUrl = entity.ImageUrl
            };
        }
    }
}