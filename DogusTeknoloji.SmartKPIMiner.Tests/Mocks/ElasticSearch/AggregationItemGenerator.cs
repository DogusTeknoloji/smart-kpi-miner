using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Tests.Mocks.ElasticSearch
{
    public static class AggregationItemGenerator
    {
        public static AggregationItem Generate()
        {
            AggregationItem item = new AggregationItem
            {
                RequestCount = RandomNumberGenerator.GetRandomInt(),
                ServerName = RandomStringGenerator.GetRandomString(10),
                SiteName = RandomStringGenerator.GetRandomString(15),
                Url = RandomStringGenerator.GetRandomString(20),
                ResponseInformation = AggregationResponseItemGenerator.Generate(10)
            };
            return item;
        }

        public static List<AggregationItem> Generate(int count)
        {
            List<AggregationItem> aggregationItems = new List<AggregationItem>();
            for (int i = 0; i < count; i++)
            {
                AggregationItem aggregationItem = Generate();
                aggregationItems.Add(aggregationItem);
            }
            return aggregationItems;
        }
    }
}
