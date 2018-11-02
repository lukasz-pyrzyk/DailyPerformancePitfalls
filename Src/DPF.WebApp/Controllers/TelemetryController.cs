using System;
using System.Threading.Tasks;
using DPF.WebApp.Db;
using DPF.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DPF.WebApp.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TelemetryController : Controller
    {
        [HttpGet("insert/fast")]
        public async Task<IActionResult> InsertFast([FromServices] DataStorageService db)
        {
            var entry = CreateEntry();
            await db.Insert(entry);

            return NoContent();
        }

        [HttpGet("insert/slow")]
        public async Task<IActionResult> InsertSlow([FromServices] SlowDataStorageService db)
        {
            var entry = CreateEntry();
            await db.Insert(entry);

            return NoContent();
        }


        [HttpGet("insert/tcp")]
        public async Task<IActionResult> InsertTcp([FromServices] DataStorageServiceWithTcp db)
        {
            var entry = CreateEntry();
            await db.Insert(entry);

            return NoContent();
        }

        private TelemetryEntry CreateEntry()
        {
            return new TelemetryEntry
            {
                Timestamp = DateTimeOffset.MaxValue,
                TimeToLive = 1,
                UserId = 1,
            };
        }
    }
}
