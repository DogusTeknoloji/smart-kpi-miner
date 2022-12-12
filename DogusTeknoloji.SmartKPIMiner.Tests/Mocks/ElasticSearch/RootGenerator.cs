using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Tests.Mocks.ElasticSearch
{
    public static class RootGenerator
    {
        public static Root Generate()
        {
            Root root = new Root
            {
                Aggregation = AggregationGenerator.Generate(),
                // Hits = HitsGenerator.Generate(),
                IsTimedOut = false,
                Took = RandomNumberGenerator.GetRandomInt(),
                Shards = ShardsGenerator.Generate()
            };
            return root;
        }

        public static List<Root> Generate(int count)
        {
            List<Root> roots = new List<Root>();
            for (int i = 0; i < count; i++)
            {
                Root root = Generate();
                roots.Add(root);
            }
            return roots;
        }
    }
}