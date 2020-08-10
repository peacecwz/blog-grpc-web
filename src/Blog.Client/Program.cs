using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Blog.Client.Helpers;
using Blog.Client.Services;
using Blog.Client.ViewModels;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Blog.Client
{
    public class Program
    {
        private static IConfiguration Configuration { get; set; }

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            Configuration = builder.Configuration;

            builder.Services.Configure<AuthorViewModel>(options =>
            {
                var config = Configuration.GetSection("author");
                options.FullName = config["fullName"];
                options.Biography = config["biography"];
                options.ProfileImage = config["profileImage"];
            });

            builder.Services.AddSingleton(services =>
            {
                var backendUrl = Configuration["backendUrl"];

                var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
                return GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions {HttpHandler = httpHandler});
            });

            builder.Services.AddSingleton(provider => new CategoriesService(provider.GetService<GrpcChannel>()));
            builder.Services.AddSingleton(provider => new PostsService(provider.GetService<GrpcChannel>()));
            builder.Services.AddSingleton(provider => new HtmlHelpers(provider.GetRequiredService<IJSRuntime>()));
            
            await builder.Build().RunAsync();
        }
    }
}