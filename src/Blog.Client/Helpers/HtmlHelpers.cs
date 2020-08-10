using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Blog.Client.Helpers
{
    public class HtmlHelpers
    {
        private readonly IJSRuntime _jsRuntime;

        public HtmlHelpers(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetTitle(string title)
        {
            await _jsRuntime.InvokeAsync<string>("setTitle", title);
        }
    }
}