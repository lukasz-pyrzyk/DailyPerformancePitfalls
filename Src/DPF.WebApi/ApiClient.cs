using System.Net.Http;

namespace DPF.WebApi
{
    public class ApiClient
    {
        public HttpClient Client { get; }

        public ApiClient(HttpClient client)
        {
            Client = client;
        }

        public string CreateUrl()
        {
            return "historical-events/search.php?format=json&begin_date=20121231&end_date=20171231&lang=en";
        }
    }
}
