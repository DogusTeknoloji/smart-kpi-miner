using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace DogusTeknoloji.SmartKPIMiner.Agent
{
    public static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            using (SmartKPIMinerAgent minerAgent = new SmartKPIMinerAgent())
            {
                Task t = minerAgent.KPIProcessAsync();
                Task tLog = minerAgent.LogProcess();
                Task.WaitAll(new[] { t, tLog });
            }
#elif RELEASE
            ServiceBase.Run(new SmartKPIMinerAgent());
#endif
        }
    }
}
