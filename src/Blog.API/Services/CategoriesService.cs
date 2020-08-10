using System.Linq;
using System.Threading.Tasks;
using Blog.API.Data;
using Blog.API.Mappers;
using Blog.Contract;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Services
{
    public class CategoriesService : CategoryService.CategoryServiceBase
    {
        private readonly BlogDbContext _blogDbContext;
        private readonly ICategoryMapper _categoryMapper;

        public CategoriesService(BlogDbContext blogDbContext, ICategoryMapper categoryMapper)
        {
            _blogDbContext = blogDbContext;
            _categoryMapper = categoryMapper;
        }

        public override async Task<GetCategoriesResponse> GetCategories(Empty request, ServerCallContext context)
        {
            var categoryEntities = await _blogDbContext.Categories
                .Where(category => category.IsActive && !category.IsDeleted)
                .OrderByDescending(category => category.Order)
                .ToListAsync();

            var response = new GetCategoriesResponse();
            response.Categories.AddRange(categoryEntities.Select(_categoryMapper.Map));

            return response;
        }
    }
}