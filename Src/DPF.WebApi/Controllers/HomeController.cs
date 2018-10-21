using System.Threading.Tasks;
using DPF.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DPF.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiClient _apiClient;

        public HomeController(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index()
        {
            var request = _apiClient.CreateUrl();
            var json = await _apiClient.Client.GetStringAsync(request);

            var response = JsonConvert.DeserializeObject<RootObject>(json);
            return Json(response);
        }
    }
}
