using DogusTeknoloji.SmartKPIMiner.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DogusTeknoloji.SmartKPIMiner.Tests.CoreTests.UnitTests
{

    [TestClass]
    public class AppSettingsManagerTests
    {

        //public static IConfiguration GetConfiguration()
        [TestMethod]
        [TestCategory("Unit-Test")]
        public void GETS_CORRECT_VALUE_WHEN_ENVIRONMENT_IS_DEVELOPMENT()
        {
            IConfiguration result = AppSettingsManager.GetConfiguration();

            Assert.IsNotNull(result);
        }

    }
}
