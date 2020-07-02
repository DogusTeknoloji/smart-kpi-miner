using DogusTeknoloji.SmartKPIMiner.Core;
using Newtonsoft.Json;

namespace DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch
{
    [JsonObject]
    [JsonConverter(typeof(JsonPathConverter))]
    public class Root
    {
        [JsonProperty("took")]
        public int Took { get; set; }
        [JsonProperty("timed_out")]
        public bool IsTimedOut { get; set; }
        [JsonProperty("_shards")]
        public Shards Shards { get; set; }
        [JsonProperty("hits")]
        public Hits Hits { get; set; }
        [JsonProperty("aggregations.allData")]
        public Aggregation Aggregation { get; set; }
    }
}
