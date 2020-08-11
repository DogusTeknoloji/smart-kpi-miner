using DogusTeknoloji.SmartKPIMiner.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch
{
    [JsonObject]
    [JsonConverter(typeof(JsonPathConverter))]
    public class AggregationItem
    {
        [JsonProperty("key")]
        public string Url { get; set; }
        [JsonProperty("doc_count")]
        public int RequestCount { get; set; }
        [JsonProperty("b")]
        public string ServerName { get; set; }
        [JsonProperty("a")]
        public string SiteName { get; set; }
        [JsonProperty("urlname.buckets")]
        public List<AggregationResponseItem> ResponseInformation { get; set; }
    }
}
