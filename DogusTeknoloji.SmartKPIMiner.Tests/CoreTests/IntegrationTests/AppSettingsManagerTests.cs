using DogusTeknoloji.SmartKPIMiner.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DogusTeknoloji.SmartKPIMiner.Tests.CoreTests.IntegrationTests
{

    [TestClass]
    public class AppSettingsManagerTests
    {

        //public static IConfiguration GetConfiguration(string filename)
        [TestMethod]
        [TestCategory("Integration-Test")]
        public void GETS_NULL_VALUE_WHEN_FILE_NAME_IS_NOT_FOUND()
        {
            string filePath = "not-existent_file.random";
            IConfiguration result = AppSettingsManager.GetConfiguration(filePath);

            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("Integration-Test")]
        public void GETS_NULL_VALUE_WHEN_FILE_NAME_PARAMETER_IS_NULL()
        {
            string filepath = null;
            IConfiguration result = AppSettingsManager.GetConfiguration(filepath);

            Assert.IsNull(result);
        }
        [TestMethod]
        [TestCategory("Integration-Test")]
        public void GETS_VALUE_WHEN_PARAMETER_IS_VALID()
        {
            string filepath = "appsettings.json";
            IConfiguration result = AppSettingsManager.GetConfiguration(filepath);

            Assert.IsNotNull(result);
        }
    }
}
