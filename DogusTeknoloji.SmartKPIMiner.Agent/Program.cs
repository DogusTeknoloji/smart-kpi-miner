namespace DogusTeknoloji.SmartKPIMiner.Agent
{
    public static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            using (SmartKPIMinerAgent minerAgent = new SmartKPIMinerAgent())
            {
                System.Threading.Tasks.Task t = minerAgent.KPIProcessAsync();
                System.Threading.Tasks.Task.WaitAll(t);
            }
#elif RELEASE
            ServiceBase.Run(new SmartKPIMinerAgent());
#endif
        }
    }
}
