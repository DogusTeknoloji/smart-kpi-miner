namespace DogusTeknoloji.SmartKPIMiner.Agent
{
    public static class Program
    {
        public static void Main(string[] args)
        {
#if DEBUG
            using (SmartKPIMinerAgent minerAgent = new SmartKPIMinerAgent())
            {
                //System.Threading.Tasks.Task t = minerAgent.KPIProcessAsync();
                //System.Threading.Tasks.Task.WaitAll(t);
                minerAgent.SimulateServiceStart();
                System.Threading.Thread.Sleep(System.Threading.Timeout.InfiniteTimeSpan);
            }
#elif RELEASE
            System.ServiceProcess.ServiceBase.Run(new SmartKPIMinerAgent());
#endif
        }
    }
}
