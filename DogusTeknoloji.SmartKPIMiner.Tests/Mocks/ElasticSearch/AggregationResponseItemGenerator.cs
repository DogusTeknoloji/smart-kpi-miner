using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Tests.Mocks.ElasticSearch
{
    public static class AggregationResponseItemGenerator
    {
        public static AggregationResponseItem Generate()
        {
            AggregationResponseItem responseItem = new AggregationResponseItem
            {
                AverageResponseTime = RandomNumberGenerator.GetRandomDouble(),
                DocumentCount = RandomNumberGenerator.GetRandomInt(),
                HttpResponseCode = RandomNumberGenerator.GetRandomInt(100, 599),
                MaxResponseTime = RandomNumberGenerator.GetRandomDouble(0, 5)
            };

            return responseItem;
        }

        public static List<AggregationResponseItem> Generate(int count)
        {
            List<AggregationResponseItem> responseItems = new List<AggregationResponseItem>();
            for (int i = 0; i < count; i++)
            {
                AggregationResponseItem responseItem = Generate();
                responseItems.Add(responseItem);
            }
            return responseItems;
        }
    }
}