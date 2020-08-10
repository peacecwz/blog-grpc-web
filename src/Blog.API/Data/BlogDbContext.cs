using Blog.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Data
{
    public class BlogDbContext : DbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<PostEntity> Posts { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryEntity>()
                .HasIndex(x => x.Slug)
                .IsUnique();

            modelBuilder.Entity<CategoryEntity>()
                .HasIndex(x => new {x.CreatedOn, x.IsActive, x.IsDeleted});

            modelBuilder.Entity<PostEntity>()
                .HasIndex(x => x.Slug)
                .IsUnique();

            modelBuilder.Entity<PostEntity>()
                .HasIndex(x => new {x.CategoryId, x.CreatedOn, x.IsActive, x.IsDeleted});

            base.OnModelCreating(modelBuilder);
        }
    }
}