using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Tests.Mocks.ElasticSearch
{
    public static class ShardsGenerator
    {
        public static Shards Generate()
        {
            Shards shards = new Shards
            {
                Failed = RandomNumberGenerator.GetRandomInt(),
                Skipped = RandomNumberGenerator.GetRandomInt(),
                Successful = RandomNumberGenerator.GetRandomInt(),
                Total = RandomNumberGenerator.GetRandomInt(100, 300)
            };
            return shards;
        }

        public static List<Shards> Generate(int count)
        {
            List<Shards> shards = new List<Shards>();
            for (int i = 0; i < count; i++)
            {
                Shards shard = Generate();
                shards.Add(shard);
            }
            return shards;
        }
    }
}