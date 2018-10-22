using System.Threading.Tasks;
using AutoFixture;
using DPF.WebApp.Db;
using DPF.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DPF.WebApp.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TelemetryController : Controller
    {
        private readonly DataStorageService _db;
        private readonly Fixture _fixture = new Fixture();

        public TelemetryController(DataStorageService db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Insert()
        {
            var family = _fixture.Create<TelemetryEntry>();
            await _db.InsertFamily(family);

            return NoContent();
        }
    }
}
