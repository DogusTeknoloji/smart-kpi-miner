using DogusTeknoloji.SmartKPIMiner.Core;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch
{
    [JsonObject]
    [JsonConverter(typeof(JsonPathConverter))]
    public class Aggregation
    {
        [JsonProperty("doc_count_error_upper_bound")]
        public int DocumentCountError { get; set; }
        [JsonProperty("sum_other_doc_count")]
        public int DocumentCountSum { get; set; }
        [JsonProperty("buckets")]
        public IList<AggregationServer> Items { get; set; }
    }
}
