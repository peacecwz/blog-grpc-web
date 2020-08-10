using System.Linq;
using Blog.API.Data;
using Blog.API.Mappers;
using Blog.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddDbContext<BlogDbContext>(options => { options.UseSqlite("filename=blog.db"); });
            services.AddSingleton<IPostMapper, PostMapper>();
            services.AddSingleton<ICategoryMapper, CategoryMapper>();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] {"application/octet-stream"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }

            app.UseStaticFiles();
            app.UseBlazorFrameworkFiles();

            app.UseRouting();

            app.UseGrpcWeb();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<BlogDbContext>();
                DbInitializer.Initialize(context);
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<CategoriesService>().EnableGrpcWeb();
                endpoints.MapGrpcService<PostsService>().EnableGrpcWeb();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}