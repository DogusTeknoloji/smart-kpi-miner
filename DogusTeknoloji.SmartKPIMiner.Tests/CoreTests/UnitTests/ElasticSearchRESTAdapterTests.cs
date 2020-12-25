using DogusTeknoloji.SmartKPIMiner.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
namespace DogusTeknoloji.SmartKPIMiner.Tests.CoreTests.UnitTests
{
    [TestClass]
    public class ElasticSearchRESTAdapterTests
    {
        #region GET_REQUEST_BODY

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_REQUEST_BODY_GETS_NULL_WHEN_PASS_NULL_START_DATE()
        {
            DateTime? startDate = null;
            string result = ElasticSearchRESTAdapter.GetRequestBody(start: startDate);

            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_REQUEST_BODY_GETS_NULL_WHEN_PASS_NULL_END_DATE()
        {
            DateTime? startDate = DateTime.Now;
            DateTime? endDate = null;

            string result = ElasticSearchRESTAdapter.GetRequestBody(start: startDate, end: endDate);

            Assert.IsNull(result);
        }
        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_REQUEST_BODY_GETS_NULL_WHEN_PASS_BOTH_PARAMETERS_NULL()
        {
            DateTime? startDate = null;
            DateTime? endDate = null;

            string result = ElasticSearchRESTAdapter.GetRequestBody(start: startDate, end: endDate);

            Assert.IsNull(result);
        }
        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_REQUEST_BODY_GETS_NULL_WHEN_PASS_END_DATE_IS_CORRECT_BUT_START_DATE_IS_NULL()
        {
            DateTime? startDate = null;
            DateTime? endDate = DateTime.Now;

            string result = ElasticSearchRESTAdapter.GetRequestBody(start: startDate, end: endDate);

            Assert.IsNull(result);
        }
        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_REQUEST_BODY_RETURNS_NULL_WHEN_START_DATE_GREATER_THAN_END_DATE()
        {
            DateTime? startDate = DateTime.Now;
            DateTime? endDate = DateTime.Now.AddDays(-1);

            string result = ElasticSearchRESTAdapter.GetRequestBody(start: startDate, end: endDate);

            Assert.IsNull(result);
        }
        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_REQUEST_BODY_RETURNS_CORRECT_WHEN_PARAMETERS_CORRECT()
        {
            DateTime? startDate = DateTime.Now.AddDays(-1);
            DateTime? endDate = DateTime.Now;

            string result = ElasticSearchRESTAdapter.GetRequestBody(start: startDate, end: endDate);

            Assert.IsNotNull(result);
        }

        #endregion
        #region GET_FULL_INDEX_NAME
        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_FULL_INDEX_NAME_RETURNS_ASTERIKS_WHEN_INDEX_IS_NULL()
        {
            string indexName = null;
            string indexPattern = "*";
            DateTime? indexPatternValue = DateTime.Now;

            string result = ElasticSearchRESTAdapter.GetFullIndexName(index: indexName, indexPattern: indexPattern, indexPatternValue: indexPatternValue);

            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_FULL_INDEX_NAME_RETURNS_NULL_WHEN_INDEXPATTERN_IS_NULL()
        {
            string indexName = "index-1";
            string indexPattern = null;
            DateTime? indexPatternValue = DateTime.Now;

            string result = ElasticSearchRESTAdapter.GetFullIndexName(index: indexName, indexPattern: indexPattern, indexPatternValue: indexPatternValue);

            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_FULL_INDEX_NAME_RETURNS_NULL_WHEN_INDEXPATTERN_VALUE_IS_NULL()
        {
            string indexName = "index-1";
            string indexPattern = "yyyy.MM.dd";
            DateTime? indexPatternValue = null;

            string result = ElasticSearchRESTAdapter.GetFullIndexName(index: indexName, indexPattern: indexPattern, indexPatternValue: indexPatternValue);

            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_FULL_INDEX_NAME_RETURNS_VALUE_WHEN_INDEXPATTERN_IS_CORRECT()
        {
            string indexName = "index-1";
            string indexPattern = "*";
            DateTime? indexPatternValue = null;

            string result = ElasticSearchRESTAdapter.GetFullIndexName(index: indexName, indexPattern: indexPattern, indexPatternValue: indexPatternValue);

            string expected = indexName + "-" + indexPattern;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_FULL_INDEX_NAME_RETURNS_VALUE_WHEN_INDEXPATTERN_IS_DATE_FORMAT()
        {
            string indexName = "index-1";
            string indexPattern = "yyyy.MM.dd";
            DateTime? indexPatternValue = DateTime.Now;

            string result = ElasticSearchRESTAdapter.GetFullIndexName(index: indexName, indexPattern: indexPattern, indexPatternValue: indexPatternValue);

            string expected = indexName + "-" + indexPatternValue.Value.ToString(indexPattern);
            Assert.AreEqual(expected, result);
        }
        #endregion
        #region GET_RESPONSE_FROM_ELASTIC_URL_ASYNC
        #endregion
    }
}