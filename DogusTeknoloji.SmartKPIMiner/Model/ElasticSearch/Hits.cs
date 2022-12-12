using DogusTeknoloji.SmartKPIMiner.Core;
using Newtonsoft.Json;

namespace DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch
{
    [JsonObject]
    [JsonConverter(typeof(JsonPathConverter))]
    public class Hits
    {
        // [JsonProperty("total")]
        // public int Total { get; set; }
        // [JsonProperty("max_score")]
        // public int? MaxScore { get; set; }
    }
}
