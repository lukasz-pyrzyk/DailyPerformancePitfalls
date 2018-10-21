using Newtonsoft.Json;

namespace DPF.WebApi.Models
{
    public class Event
    {
        public string Date { get; set; }
        public string Description { get; set; }
        public string Lang { get; set; }
        public string Category1 { get; set; }
        public string Granularity { get; set; }
    }

    public class EventsResponse
    {
        [JsonProperty("count")]
        public string Count { get; set; }
        public Event Event { get; set; }
    }

    public class RootObject
    {
        public EventsResponse Result { get; set; }
    }
}
