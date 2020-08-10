using System;
using System.Collections.Generic;
using System.Linq;
using Blog.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(BlogDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            dbContext.Database.Migrate();

            if (dbContext.Posts.Any())
            {
                return;
            }

            var categories = new List<CategoryEntity>()
            {
                new CategoryEntity()
                {
                    Order = 1,
                    Name = ".NET Core",
                    Slug = "net-core",
                    ImageUrl = "https://miro.medium.com/max/2000/1*owH3LC7VnzpM8JV4RtY8oA.png"
                },
                new CategoryEntity()
                {
                    Order = 2,
                    Name = "Spring Boot",
                    Slug = "spring-boot",
                    ImageUrl =
                        "https://res.cloudinary.com/practicaldev/image/fetch/s--NNcb7stF--/c_imagga_scale,f_auto,fl_progressive,h_900,q_auto,w_1600/https://thepracticaldev.s3.amazonaws.com/i/kqcla6ngdh5sela8ze3b.jpg"
                },
                new CategoryEntity()
                {
                    Order = 5,
                    Name = "React Native",
                    Slug = "react-native",
                    ImageUrl = ""
                },
                new CategoryEntity()
                {
                    Order = 3,
                    Name = "Software Architecture",
                    Slug = "software-architecture",
                    ImageUrl = ""
                },
                new CategoryEntity()
                {
                    Order = 4,
                    Name = "Software Craftsmanship",
                    Slug = "software-craftsmanship",
                    ImageUrl = ""
                }
            };

            dbContext.Categories.AddRange(categories);

            dbContext.SaveChanges();

            var posts = new List<PostEntity>()
            {
                new PostEntity()
                {
                    Id = Guid.NewGuid(),
                    Title = ".NET Core Configuration",
                    Description = ".NET Core Configuration article description",
                    Content =
                        "<h3>.NET Core Configuration</h3><p>.NET Core Configuration article content Part I</p><p>.NET Core Configuration article content Part II</p><p>.NET Core Configuration article content Part III</p><h4>Conclusion</h4>",
                    Slug = "net-core-configuration",
                    Status = PostStatus.Published,
                    Tags = "net-core,csharp,development",
                    CategoryId = categories[0].Id,
                    PublishedDate = DateTime.UtcNow.AddDays(-3),
                    CoverImageUrl = "https://miro.medium.com/max/2000/1*owH3LC7VnzpM8JV4RtY8oA.png"
                },
                new PostEntity()
                {
                    Id = Guid.NewGuid(),
                    Title = "[Draft] .NET Core Configuration",
                    Description = "[Draft] .NET Core Configuration article description",
                    Content =
                        "<h3>[Draft] .NET Core Configuration</h3><p>.NET Core Configuration article content Part I</p><p>.NET Core Configuration article content Part II</p><p>.NET Core Configuration article content Part III</p><h4>Conclusion</h4>",
                    Slug = "net-core-configuration-2",
                    Status = PostStatus.Draft,
                    Tags = "draft,net-core,csharp,development",
                    CategoryId = categories[0].Id,
                    CoverImageUrl = "https://miro.medium.com/max/2000/1*owH3LC7VnzpM8JV4RtY8oA.png"
                },
                new PostEntity()
                {
                    Id = Guid.NewGuid(),
                    Title = "Spring Boot Cloud Configuration",
                    Description = "Spring Boot Cloud Configuration article description",
                    Content =
                        "<h3>Spring Boot Cloud Configuration</h3><p>Spring Boot Cloud Configuration article content Part I</p><p>Spring Boot Cloud Configuration article content Part II</p><p>Spring Boot Cloud Configuration article content Part III</p><h4>Conclusion</h4>",
                    Slug = "spring-boot-cloud-configuration",
                    Status = PostStatus.Published,
                    Tags = "spring-boot,java,development",
                    CategoryId = categories[1].Id,
                    PublishedDate = DateTime.UtcNow.AddDays(-2),
                    CoverImageUrl =
                        "https://res.cloudinary.com/practicaldev/image/fetch/s--NNcb7stF--/c_imagga_scale,f_auto,fl_progressive,h_900,q_auto,w_1600/https://thepracticaldev.s3.amazonaws.com/i/kqcla6ngdh5sela8ze3b.jpg"
                },
                new PostEntity()
                {
                    Id = Guid.NewGuid(),
                    Title = "[Draft] Spring Boot Cloud Configuration",
                    Description = "[Draft] Spring Boot Cloud Configuration article description",
                    Content =
                        "<h3>[Draft] Spring Boot Cloud Configuration</h3><p>Spring Boot Cloud Configuration article content Part I</p><p>Spring Boot Cloud Configuration article content Part II</p><p>Spring Boot Cloud Configuration article content Part III</p><h4>Conclusion</h4>",
                    Slug = "spring-boot-cloud-configuration-2",
                    Status = PostStatus.Draft,
                    Tags = "draft,spring-boot,java,development",
                    CategoryId = categories[1].Id,
                    CoverImageUrl =
                        "https://res.cloudinary.com/practicaldev/image/fetch/s--NNcb7stF--/c_imagga_scale,f_auto,fl_progressive,h_900,q_auto,w_1600/https://thepracticaldev.s3.amazonaws.com/i/kqcla6ngdh5sela8ze3b.jpg"
                },
            };

            dbContext.Posts.AddRange(posts);
            dbContext.SaveChanges();
        }
    }
}