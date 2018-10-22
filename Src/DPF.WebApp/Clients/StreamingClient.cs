using System.Net.Http;

namespace DPF.WebApp.Clients
{
    public class StreamingClient
    {
        public HttpClient Client { get; }

        public StreamingClient(HttpClient client)
        {
            Client = client;
        }
    }
}
