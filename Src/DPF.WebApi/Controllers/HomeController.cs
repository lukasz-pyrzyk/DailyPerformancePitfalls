using System.Threading.Tasks;
using DPF.WebApi.BankApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DPF.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly BondApi _apiClient;

        public HomeController(BondApi apiClient)
        {
            _apiClient = apiClient;
        }

        [Route("slow")]
        public async Task<IActionResult> Index()
        {
            var url = _apiClient.CreateUrl();
            var json = await _apiClient.Client.GetStringAsync(url);

            var response = JsonConvert.DeserializeObject<Bond[]>(json);
            return Json(response);
        }
    }
}
