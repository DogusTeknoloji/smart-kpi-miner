using DogusTeknoloji.SmartKPIMiner.Helpers;
using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using DogusTeknoloji.SmartKPIMiner.Tests.Mocks.ElasticSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DogusTeknoloji.SmartKPIMiner.Tests.HelpersTests
{
    [TestClass]
    public class ComputeExtensionsTest
    {
        [TestMethod]
        public void COMPUTES_CORRECT_WHEN_SUCCESS_RATE_CALLED()
        {
            AggregationItem aggregationItem = AggregationItemGenerator.Generate();

            int successCount = 0;
            double successAvgResTime = 0;
            double percentage = aggregationItem.GetSuccessRate(out successCount, out successAvgResTime);

        }
    }
}