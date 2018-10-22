using System;
using System.Net.Http;

namespace DPF.WebApi.BankApi
{
    public class BondApi
    {
        public HttpClient Client { get; }

        public BondApi(HttpClient client)
        {
            Client = client;
            Client.BaseAddress = new Uri("https://bank.gov.ua/");
            Client.Timeout = TimeSpan.FromSeconds(10);
        }

        public HttpRequestMessage GetRequest()
        {
            return new HttpRequestMessage(HttpMethod.Get, "NBUStatService/v1/statdirectory/ovdp?json");
        }
    }
}
