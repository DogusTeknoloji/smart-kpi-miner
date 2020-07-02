using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Tests.Mocks.ElasticSearch
{
    public static class AggregationGenerator
    {
        public static Aggregation Generate()
        {
            Aggregation aggregation = new Aggregation
            {
                DocumentCountError = RandomNumberGenerator.GetRandomInt(0, 10),
                DocumentCountSum = RandomNumberGenerator.GetRandomInt(10, 100),
                Items = AggregationServerGenerator.Generate(25)
            };
            return aggregation;
        }

        public static List<Aggregation> Generate(int count)
        {
            List<Aggregation> aggregations = new List<Aggregation>();
            for (int i = 0; i < count; i++)
            {
                Aggregation aggregation = Generate();
                aggregations.Add(aggregation);
            }
            return aggregations;
        }
    }
}