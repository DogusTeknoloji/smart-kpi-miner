using DogusTeknoloji.SmartKPIMiner.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DogusTeknoloji.SmartKPIMiner.Tests.HelpersTests
{
    [TestClass]
    public class CommonFunctionsTests
    {
        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GET_UNIX_TIMESTAMP_RETURNS_VALUE_WHEN_PASS_CORRECT_DATE()
        {
            DateTime methodParameter = new DateTime(year: 2020,
                                                    month: 01,
                                                    day: 01,
                                                    hour: 06,
                                                    minute: 30,
                                                    second: 00,
                                                    DateTimeKind.Local);
            long result = CommonFunctions.GetCurrentUnixTimestampMillisec(date: methodParameter);
            long expected = 1577849400000;

            Assert.AreEqual(expected, result);
        }

        public void GET_UNIX_TIMESTAMP_RETURNS_NULL_WHEN_PASS_INVALID_DATE()
        {
            DateTime methodParameter = new DateTime(year: 0001, month: 01, day: 01);
            long result = CommonFunctions.GetCurrentUnixTimestampMillisec(methodParameter);

            Assert.AreEqual(default(long), result);
        }
    }
}