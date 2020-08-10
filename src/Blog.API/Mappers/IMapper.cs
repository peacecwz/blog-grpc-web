using Blog.API.Data;

namespace Blog.API.Mappers
{
    public interface IMapper<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        TEntity Map(TModel model);
        TModel Map(TEntity entity);
    }
}