using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Tests.Mocks.ElasticSearch
{
    public static class HitsGenerator
    {
        public static Hits Generate()
        {
            Hits hits = new Hits
            {
                MaxScore = RandomNumberGenerator.GetRandomInt(),
                Total = RandomNumberGenerator.GetRandomInt(100, 200)
            };
            return hits;
        }

        public static List<Hits> Generate(int count)
        {
            List<Hits> hits = new List<Hits>();
            for (int i = 0; i < count; i++)
            {
                Hits hit = Generate();
                hits.Add(hit);
            }
            return hits;
        }
    }
}