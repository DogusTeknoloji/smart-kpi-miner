using System.ServiceProcess;
namespace DogusTeknoloji.SmartKPIMiner.Agent
{
    public static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            SmartKPIMinerAgent minerAgent = new SmartKPIMinerAgent();
#elif RELEASE
            ServiceBase.Run(new SmartKPIMinerAgent());
#endif
        }
    }
}
