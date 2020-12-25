using DogusTeknoloji.SmartKPIMiner.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogusTeknoloji.SmartKPIMiner.Tests.HelpersTests
{
    [TestClass]
    public class IntituitiveSiteMatcherExtensionsTests
    {
        [TestMethod]
        [TestCategory("Unit-Test")]
        public void IS_PART_EXCLUDED_RETURNS_TRUE_WHEN_PART_IS_COM()
        {
            string partStr = "com";
            bool result = partStr.IsPartExcluded();
            bool expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void IS_PART_EXCLUDED_RETURNS_TRUE_WHEN_PART_IS_FW()
        {
            string partStr = "fw";
            bool result = partStr.IsPartExcluded();
            bool expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void IS_PART_EXCLUDED_RETURNS_TRUE_WHEN_PART_IS_HTTP()
        {
            string partStr = "http";
            bool result = partStr.IsPartExcluded();
            bool expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void IS_PART_EXCLUDED_RETURNS_TRUE_WHEN_PART_IS_HTTPS()
        {
            string partStr = "https";
            bool result = partStr.IsPartExcluded();
            bool expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void IS_PART_EXCLUDED_RETURNS_TRUE_WHEN_PART_IS_WWW()
        {
            string partStr = "www";
            bool result = partStr.IsPartExcluded();
            bool expected = true;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void IS_PART_EXCLUDED_RETURNS_FALSE_WHEN_PART_IS_HELLO()
        {
            string partStr = "hello";
            bool result = partStr.IsPartExcluded();
            bool expected = false;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void CHECK_SIMILARITY_RETURNS_100_WHEN_INPUT_IS_CORRECT()
        {
            string potentialAppName = "CORRECT";
            potentialAppName.AddAsPotentialAppName();

            double similarityPercentage = potentialAppName.CheckSimilarity();
            double expected = 100;

            Assert.AreEqual(expected, similarityPercentage);
        }

        [TestMethod]
        [TestCategory("Unit-Test")]
        public void CHECK_SIMILARITY_RETURNS_LOWER_THAN_50_INPUT_IS_DIFFERENT()
        {
            string potentialAppName = "CORRECT";
            string secondaryAppName = "WRONG";
            potentialAppName.AddAsPotentialAppName();

            double similarityPercentage = secondaryAppName.CheckSimilarity();
            double expectedLowerThan = 50;

            Assert.IsTrue(similarityPercentage < expectedLowerThan);
        }
    }
}
