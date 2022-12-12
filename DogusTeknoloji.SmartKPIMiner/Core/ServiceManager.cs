using DogusTeknoloji.SmartKPIMiner.Data;
using DogusTeknoloji.SmartKPIMiner.Data.DataAccessObjects;
using DogusTeknoloji.SmartKPIMiner.Model.Auth;
using Microsoft.Extensions.Configuration;

namespace DogusTeknoloji.SmartKPIMiner.Core
{
    public static class ServiceManager
    {
        public static long LastRuleId = 0;
        public static readonly IConfiguration Appsettings = AppSettingsManager.GetConfiguration();
        private static readonly string _connectionString = Appsettings["ConnectionStrings:ElasticSearch"];
        public static readonly AuthModel _authModel;

        static ServiceManager()
        {
            _authModel = new AuthModel
            {
                UserName = Appsettings["ElasticAuth:UserName"],
                Password = Appsettings["ElasticAuth:password"]
            };
        }
        
        public static KPIService _kpiService = new KPIService(_connectionString);
        public static void Initialize() => DbInitializer.Initialize(_connectionString);
    }
}