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
        private readonly Fixture _fixture = new Fixture();

        [HttpGet("insert/fast")]
        public async Task<IActionResult> InsertFast([FromServices] DataStorageService db)
        {
            var family = _fixture.Create<TelemetryEntry>();
            await db.InsertFamily(family);

            return NoContent();
        }

        [HttpGet("insert/slow")]
        public async Task<IActionResult> InsertSlow([FromServices] SlowDataStorageService db)
        {
            var family = _fixture.Create<TelemetryEntry>();
            await db.InsertFamily(family);

            return NoContent();
        }
    }
}
