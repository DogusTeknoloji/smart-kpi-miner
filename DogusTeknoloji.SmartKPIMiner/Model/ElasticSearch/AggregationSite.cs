using DogusTeknoloji.SmartKPIMiner.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch
{
    [JsonObject]
    [JsonConverter(typeof(JsonPathConverter))]
    public class AggregationSite
    {
        [JsonProperty("key")]
        public string SiteName { get; set; }
        [JsonProperty("Sitename.buckets")]
        public IList<AggregationItem> Urls { get; set; }
    }
}
