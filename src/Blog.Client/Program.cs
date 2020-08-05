using System.Net.Http;
using System.Threading.Tasks;
using Blog.Client.Models;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            var config = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            builder.Services.Configure<Author>(options =>
            {
                var author = config.GetSection("Author");
                options.FullName = author["fullName"];
                options.Biography = author["biography"];
                options.ProfileImage = author["profileImage"];
            });

            builder.Services.AddSingleton(services =>
            {
                var backendUrl = config["backendUrl"];

                var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());
                return GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions {HttpHandler = httpHandler});
            });

            await builder.Build().RunAsync();
        }
    }
}