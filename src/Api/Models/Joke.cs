using Newtonsoft.Json;
using System.Collections.Generic;

namespace Api.Models
{
    public class Joke
    {
        public List<string> Categories { get; set; } = new List<string>();
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}