using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Client.ViewModels;
using Blog.Contract;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;

namespace Blog.Client.Services
{
    public class PostsService
    {
        private readonly GrpcChannel _grpcChannel;

        public PostsService(GrpcChannel grpcChannel)
        {
            _grpcChannel = grpcChannel;
        }

        public async Task<List<PostViewModel>> GetPostsByCategory(string categorySlug)
        {
            var client = new PostServices.PostServicesClient(_grpcChannel);
            var response = await client.GetPublishedPostsByCategoryAsync(new GetPostsByCategoryRequest()
            {
                CategorySlug = categorySlug,
                Offset = 0,
                Limit = 100
            });

            return MapToViewModel(response);
        }

        public async Task<PostViewModel> GetPost(string slug)
        {
            var client = new PostServices.PostServicesClient(_grpcChannel);
            var response = await client.GetPublishedPostAsync(new GetPostRequest()
            {
                Slug = slug
            });

            if (response == null)
            {
                return null;
            }

            return new PostViewModel()
            {
                Id = response.Id,
                Description = response.Description,
                Title = response.Title,
                Slug = response.Slug,
                PublishedDate = response.PublishedDate.ToDateTime(),
                CoverImageUrl = response.CoverImageUrl,
                Tags = response.Tags,
                Content = response.Content,
                CategoryName = response.CategoryName,
                CategorySlug = response.CategorySlug
            };
        }

        public async Task<List<PostViewModel>> GetPosts()
        {
            var client = new PostServices.PostServicesClient(_grpcChannel);
            var response = await client.GetPublishedPostsAsync(new GetPostsRequest()
            {
                Offset = 0,
                Limit = 100
            });

            return MapToViewModel(response);
        }

        private static List<PostViewModel> MapToViewModel(GetPostsResponse response)
        {
            return response.Posts.Select(post => new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Slug = post.Slug,
                Description = post.Description,
                PublishedDate = post.PublishedDate.ToDateTime(),
                CoverImageUrl = post.CoverImageUrl,
                CategoryName = post.CategoryName,
                CategorySlug = post.CategorySlug
            }).ToList();
        }
    }
}