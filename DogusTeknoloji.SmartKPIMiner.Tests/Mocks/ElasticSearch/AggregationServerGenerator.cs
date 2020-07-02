using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Tests.Mocks.ElasticSearch
{
    public static class AggregationServerGenerator
    {
        public static AggregationServer Generate()
        {
            AggregationServer aggregationServer = new AggregationServer
            {
                ServerName = RandomStringGenerator.GetRandomString(10),
                Sites = AggregationSiteGenerator.Generate(10)
            };
            return aggregationServer;
        }

        public static List<AggregationServer> Generate(int count)
        {
            List<AggregationServer> aggregationServers = new List<AggregationServer>();
            for (int i = 0; i < count; i++)
            {
                AggregationServer aggregationServer = Generate();
                aggregationServers.Add(aggregationServer);
            }
            return aggregationServers;
        }
    }
}
