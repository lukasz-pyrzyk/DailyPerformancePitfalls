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
            var entry = CreateEntry();
            await db.InsertFamily(entry);

            return NoContent();
        }

        [HttpGet("insert/slow")]
        public async Task<IActionResult> InsertSlow([FromServices] SlowDataStorageService db)
        {
            var entry = CreateEntry();
            await db.InsertFamily(entry);

            return NoContent();
        }

        private TelemetryEntry CreateEntry()
        {
            return _fixture.Build<TelemetryEntry>()
                .With(x => x.TimeToLive, 1)
                .Create();
        }
    }
}
