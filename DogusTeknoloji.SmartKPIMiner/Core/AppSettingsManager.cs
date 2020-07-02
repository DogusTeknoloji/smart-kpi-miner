using DogusTeknoloji.SmartKPIMiner.Helpers;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace DogusTeknoloji.SmartKPIMiner.Core
{
    public static class AppSettingsManager
    {
        private static readonly Dictionary<string, IConfiguration> _settings = new Dictionary<string, IConfiguration>();

        public static IConfiguration GetConfiguration()
        {
#if DEBUG
            IConfiguration result = GetConfiguration("appsettings.development.json");
#elif RELEASE
            IConfiguration result = GetConfiguration("appsettings.json");
#endif
            return result;
        }

        public static IConfiguration GetConfiguration(string filename)
        {
            string result = Path.Combine(CommonFunctions.AssemblyDirectory, filename);
            if (string.IsNullOrEmpty(filename) || !File.Exists(result)) { return null; }

            if (!_settings.ContainsKey(filename))
            {

                IConfigurationRoot confBuilder = new ConfigurationBuilder()
                    .AddJsonFile(filename)
                    .Build();
                _settings.Add(filename, confBuilder);
            }

            return _settings[filename];
        }
    }
}
