using Microsoft.AspNetCore.Mvc;

namespace DPF.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello world");
        }
    }
}
