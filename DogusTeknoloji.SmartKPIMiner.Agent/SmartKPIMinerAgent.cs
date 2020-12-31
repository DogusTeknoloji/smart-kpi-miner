using DogusTeknoloji.SmartKPIMiner.Core;
using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace DogusTeknoloji.SmartKPIMiner.Agent
{
    public class SmartKPIMinerAgent : ServiceBase
    {
        protected Timer _mainServiceTimer;
        protected OperationContext context = new OperationContext();
        protected bool notIsProcessLocked = true;
        public SmartKPIMinerAgent()
        {

        }
        public async Task KPIProcessAsync()
        {
            if (notIsProcessLocked)
            {
                notIsProcessLocked = false;
                ServiceManager.Initialize();
                await context.ProcessItemsAsync();
                notIsProcessLocked = true;
            }
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            _mainServiceTimer = new Timer(callback: async state => await KPIProcessAsync(), state: null, dueTime: 0, period: (int)TimeSpan.FromMinutes(15).TotalMilliseconds);
        }
        protected override void OnStop()
        {
            base.OnStop();
            _mainServiceTimer?.Change(dueTime: Timeout.Infinite, period: 0);
            _mainServiceTimer.DisposeAsync();
        }
    }
}