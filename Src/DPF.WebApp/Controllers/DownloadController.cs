using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using DPF.WebApp.Clients;
using Microsoft.AspNetCore.Mvc;

namespace DPF.WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DownloadController : Controller
    {
        private readonly StreamingClient _client;

        public DownloadController(StreamingClient client)
        {
            _client = client;
        }

        [HttpGet("buffering")]
        public async Task<IActionResult> WithBuffering()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "dotnet/corefx/zip/v2.2.0-preview3");

            var response = await _client.Client.SendAsync(request);

            var stream = await response.Content.ReadAsStreamAsync();
            var contentType = response.Content.Headers.ContentType.MediaType;
            return new FileStreamResult(stream, contentType);
        }

        [HttpGet("no-buffering")]
        public async Task<IActionResult> WithoutBuffering()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "dotnet/corefx/zip/v2.2.0-preview3");

            var response = await _client.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var stream = await response.Content.ReadAsStreamAsync();
            var contentType = response.Content.Headers.ContentType.MediaType;
            return new FileStreamResult(stream, contentType);
        }

        [HttpGet("stream")]
        public async Task<Stream> Stream()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "dotnet/corefx/zip/v2.2.0-preview3");

            var response = await _client.Client.GetStreamAsync(request.RequestUri);
            return response;
        }


        [HttpGet("no-buffering-with-size")]
        public async Task<IActionResult> WithoutBufferingWithSize()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "dotnet/corefx/zip/v2.2.0-preview3");

            var response = await _client.Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var stream = await response.Content.ReadAsStreamAsync();
            var contentType = response.Content.Headers.ContentType.MediaType;
            Response.ContentLength = response.Content.Headers.ContentLength;
            return new FileStreamResult(stream, contentType);
        }
    }
}
