using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Tests.Mocks.ElasticSearch
{
    public static class AggregationSiteGenerator
    {
        public static AggregationSite Generate()
        {
            AggregationSite site = new AggregationSite
            {
                SiteName = RandomStringGenerator.GetRandomString(10),
                Urls = AggregationItemGenerator.Generate(20)
            };
            return site;
        }

        public static List<AggregationSite> Generate(int count)
        {
            List<AggregationSite> aggregationSites = new List<AggregationSite>();
            for (int i = 0; i < count; i++)
            {
                AggregationSite aggregationSite = Generate();
                aggregationSites.Add(aggregationSite);
            }
            return aggregationSites;
        }
    }
}