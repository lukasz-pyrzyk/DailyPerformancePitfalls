using System;
using Newtonsoft.Json;

namespace DPF.WebApp.Models
{
    public class TelemetryEntry
    {
        public DateTimeOffset Timestamp { get; set; }

        public int UserId { get; set; }

        [JsonProperty(PropertyName = "ttl", NullValueHandling = NullValueHandling.Ignore)]
        public int? TimeToLive { get; set; } = 1;
    }
}
