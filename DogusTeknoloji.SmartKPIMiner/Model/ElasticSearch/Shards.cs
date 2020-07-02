using DogusTeknoloji.SmartKPIMiner.Core;
using Newtonsoft.Json;

namespace DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch
{
    [JsonObject]
    [JsonConverter(typeof(JsonPathConverter))]
    public class Shards
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("successful")]
        public int Successful { get; set; }
        [JsonProperty("skipped")]
        public int Skipped { get; set; }
        [JsonProperty("failed")]
        public int Failed { get; set; }
    }
}
