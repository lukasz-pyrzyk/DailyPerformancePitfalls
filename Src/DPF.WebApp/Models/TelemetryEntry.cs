using System;

namespace DPF.WebApp.Models
{
    public class TelemetryEntry
    {
        public DateTimeOffset Timestamp { get; set; }
        public int UserId { get; set; }
    }
}
