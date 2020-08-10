using System.Linq;
using System.Threading.Tasks;
using Blog.API.Data;
using Blog.API.Data.Entities;
using Blog.API.Mappers;
using Blog.Contract;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Services
{
    public class PostsService : PostServices.PostServicesBase
    {
        private readonly BlogDbContext _blogDbContext;
        private readonly IPostMapper _postMapper;

        public PostsService(BlogDbContext blogDbContext, IPostMapper postMapper)
        {
            _blogDbContext = blogDbContext;
            _postMapper = postMapper;
        }

        public override async Task<GetPostsResponse> GetPublishedPostsByCategory(GetPostsByCategoryRequest request,
            ServerCallContext context)
        {
            var response = new GetPostsResponse();

            var categoryEntity = await _blogDbContext.Categories
                .SingleOrDefaultAsync(category =>
                    category.Slug == request.CategorySlug && category.IsActive && !category.IsDeleted);

            if (categoryEntity == null)
            {
                return response;
            }

            var postEntities = await _blogDbContext.Posts
                .Where(post => post.CategoryId == categoryEntity.Id)
                .Where(post => post.IsActive && !post.IsDeleted)
                .Where(post => post.Status == PostStatus.Published)
                .OrderByDescending(post => post.PublishedDate)
                .Skip((int) request.Offset)
                .Take((int) request.Limit)
                .ToListAsync();

            response.Posts.AddRange(postEntities.Select(postEntity => _postMapper.Map(postEntity, categoryEntity)));

            return response;
        }

        public override async Task<GetPostsResponse> GetPublishedPosts(GetPostsRequest request,
            ServerCallContext context)
        {
            var posts = await _blogDbContext.Posts
                .Where(post => post.IsActive && !post.IsDeleted)
                .Where(post => post.Status == PostStatus.Published)
                .OrderByDescending(post => post.PublishedDate)
                .Skip((int) request.Offset)
                .Take((int) request.Limit)
                .ToListAsync();

            var response = new GetPostsResponse();
            response.Posts.AddRange(posts.Select(_postMapper.Map));

            return response;
        }

        public override async Task<Post> GetPublishedPost(GetPostRequest request, ServerCallContext context)
        {
            var postEntity = await _blogDbContext.Posts
                .FirstOrDefaultAsync(post => post.Slug == request.Slug
                                             && post.IsActive
                                             && !post.IsDeleted
                                             && post.Status ==
                                             PostStatus.Published);
            var categoryEntity =
                await _blogDbContext.Categories.FirstOrDefaultAsync(category => category.Id == postEntity.CategoryId);
            return _postMapper.Map(postEntity, categoryEntity);
        }
    }
}