using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Client.ViewModels;
using Blog.Contract;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace Blog.Client.Services
{
    public class CategoriesService
    {
        private readonly GrpcChannel _grpcChannel;

        public CategoriesService(GrpcChannel grpcChannel)
        {
            _grpcChannel = grpcChannel;
        }

        public async Task<List<CategoryViewModel>> GetCategories()
        {
            var client = new CategoryService.CategoryServiceClient(_grpcChannel);
            var response = await client.GetCategoriesAsync(new Empty());

            return response.Categories.Select(category => new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Slug = category.Slug
            }).ToList();
        }
    }
}