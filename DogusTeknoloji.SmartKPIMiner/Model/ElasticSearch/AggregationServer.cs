using DogusTeknoloji.SmartKPIMiner.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch
{
    [JsonObject]
    [JsonConverter(typeof(JsonPathConverter))]
    public class AggregationServer
    {
        [JsonProperty("key")]
        public string ServerName { get; set; }
        [JsonProperty("hostname.buckets")]
        public IList<AggregationSite> Sites { get; set; }
    }
}
