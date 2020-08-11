using DogusTeknoloji.SmartKPIMiner.Core;
using Newtonsoft.Json;

namespace DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch
{
    [JsonObject]
    [JsonConverter(typeof(JsonPathConverter))]
    public class AggregationResponseItem
    {
        [JsonProperty("key")]
        public int HttpResponseCode { get; set; }
        [JsonProperty("doc_count")]
        public int DocumentCount { get; set; }
        [JsonProperty("averagetime.value")]
        public double AverageResponseTime { get; set; }
        [JsonProperty("maxtime.value")]
        public double MaxResponseTime { get; set; }
    }
}
