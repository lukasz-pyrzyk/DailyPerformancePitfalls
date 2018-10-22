using System.Net.Http;
using System.Threading.Tasks;
using DPF.WebApi.BankApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DPF.WebApi.Controllers
{
    public class BondsController : Controller
    {
        private readonly BondApi _api;

        public BondsController(BondApi api)
        {
            _api = api;
        }

        [HttpGet("/get-1")]
        public async Task<IActionResult> GetBufferedString()
        {
            var request = _api.GetRequest();
            var json = await _api.Client.GetStringAsync(request.RequestUri);
            var dtos = JsonConvert.DeserializeObject<Bond[]>(json);
            return Ok(dtos);
        }

        [HttpGet("/get-2")]
        public async Task<IActionResult> GetWholeRequestAsync()
        {
            var request = _api.GetRequest();
            using (var response = await _api.Client.GetAsync(request.RequestUri))
            {
                var dtos = response.Content.ReadAsAsync<Bond[]>();
                return Ok(dtos);
            }
        }

        [HttpGet("/get-3")]
        public async Task<IActionResult> GetStreamRequestAsync()
        {
            var request = _api.GetRequest();
            using (var response = await _api.Client.GetAsync(request.RequestUri))
            {
                var dtos = response.Content.ReadAsAsync<Bond[]>();
                return Ok(dtos);
            }
        }
    }
}
