using DogusTeknoloji.SmartKPIMiner.Data;
using DogusTeknoloji.SmartKPIMiner.Data.DataAccessObjects;
using Microsoft.Extensions.Configuration;

namespace DogusTeknoloji.SmartKPIMiner.Core
{
    public static class ServiceManager
    {
        public static long LastRuleId = 0;
        public static readonly IConfiguration Appsettings = AppSettingsManager.GetConfiguration();
        private static readonly string _connectionString = Appsettings["ConnectionStrings:ElasticSearch"];

        public static KPIService _kpiService = new KPIService(_connectionString);
        public static void Initialize() => DbInitializer.Initialize(_connectionString);
    }
}