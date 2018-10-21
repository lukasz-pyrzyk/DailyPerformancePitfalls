using System;
using System.Net.Http;

namespace DPF.WebApi.BankApi
{
    public class ApiClient
    {
        public HttpClient Client { get; }

        public ApiClient(HttpClient client)
        {
            Client = client;
            Client.BaseAddress = new Uri("https://bank.gov.ua/");
            Client.Timeout = TimeSpan.FromSeconds(10);
        }

        public string CreateUrl()
        {
            return "NBUStatService/v1/statdirectory/ovdp?json";
        }
    }
}
