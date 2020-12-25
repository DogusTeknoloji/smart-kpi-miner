using DogusTeknoloji.SmartKPIMiner.Helpers;
using DogusTeknoloji.SmartKPIMiner.Model.ElasticSearch;
using DogusTeknoloji.SmartKPIMiner.Tests.Mocks.ElasticSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DogusTeknoloji.SmartKPIMiner.Tests.HelpersTests
{
    [TestClass]
    public class ComputeExtensionsTest
    {
        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_SUCCESS_RATE_RETURNS_VALUE_WHEN_CORRECT_PARAMETER_PASSED()
        {
            AggregationItem aggregationItem = AggregationItemGenerator.Generate();

            int successCount = 0;
            double successAvgResTime = 0;
            double percentage = aggregationItem.GetSuccessRate(out successCount, out successAvgResTime);

            Assert.IsNotNull(percentage);

        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_FAILED_RATE_RETURNS_VALUE_WHEN_CORRECT_PARAMETER_PASSED()
        {
            AggregationItem aggregationItem = AggregationItemGenerator.Generate();

            int failedCount = 0;
            double failedAvgResTime = 0;
            double percentage = aggregationItem.GetFailedRate(out failedCount, out failedAvgResTime);

            Assert.IsNotNull(percentage);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_AS_AGGREGATION_ITEMS_RETURNS_VALUE_WHEN_CORRECT_PARAMETER_PASSED()
        {
            Aggregation aggregation = AggregationGenerator.Generate();
            List<AggregationItem> result = aggregation.GetAsAggregationItems();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void SPLIT_AND_GET_LAST_PART_OF_STRING_RETURNS_VALUE_WHEN_CORRECT_PARAMETER_PASSED()
        {
            string mockStr = "this.text.will.be.splitted";
            char separator = '.';
            _ = mockStr.SplitAndGetLastPartOfString(separator, out string lastPart);

            string expected = "splitted";
            Assert.AreEqual(expected, lastPart);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void SPLIT_AND_GET_LAST_PART_OF_STRING_RETURNS_ALLOF_VARIABLE_WHEN_SEPARATOR_IS_NOT_VALID()
        {
            string mockStr = "this.text.won't.be.splitted";
            char separator = '-';
            _ = mockStr.SplitAndGetLastPartOfString(separator, out string lastPart);

            string expected = "this.text.won't.be.splitted";
            Assert.AreEqual(expected, lastPart);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void SPLIT_AND_GET_LAST_PART_OF_STRING_RETURNS_NULL_WHEN_DATA_IS_NULL()
        {
            string mockStr = null;
            char separator = '.';
            mockStr.SplitAndGetLastPartOfString(separator, out string lastPart);

            string expected = null;

            Assert.AreEqual(expected, lastPart);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void CHECK_IS_FORMAT_INCLUDED_RETURNS_FALSE_WHEN_URL_HAS_FILE_FORMAT()
        {
            string mockStr = "http://mock.url/image.jpg";
            bool result = mockStr.CheckIsFormatIncluded();
            bool expected = false;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void CHECK_IS_FORMAT_INCLUDED_RETURNS_TRUE_WHEN_URL_HAS_NOT_FILE_FORMAT()
        {
            string mockStr = "http://mock.url/path";
            bool result = mockStr.CheckIsFormatIncluded();
            bool expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void VALIDATE_DATE_FORMAT_RETURNS_TRUE_WHEN_FORMAT_SEPARATOR_IS_HYPHEN()
        {
            string mockFormat = "yy-MM-dd";
            bool result = mockFormat.ValidateDateFormat();
            bool expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void VALIDATE_DATE_FORMAT_RETURNS_TRUE_WHEN_FORMAT_SEPARATOR_IS_DOT()
        {
            string mockFormat = "yy.MM.dd";
            bool result = mockFormat.ValidateDateFormat();
            bool expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void VALIDATE_DATE_FORMAT_RETURNS_TRUE_WHEN_FORMAT_SEPARATOR_IS_SLASH()
        {
            string mockFormat = "yy/MM/dd";
            bool result = mockFormat.ValidateDateFormat();
            bool expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void VALIDATE_DATE_FORMAT_RETURNS_FALSE_WHEN_FORMAT_IS_INVALID()
        {
            string mockFormat = "abcde";
            bool result = mockFormat.ValidateDateFormat();
            bool expected = false;

            Assert.AreEqual(expected, result);
        }
    }
}