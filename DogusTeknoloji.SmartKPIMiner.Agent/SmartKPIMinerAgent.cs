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
        protected Timer _loggingTimer;
        protected OperationContext context = new OperationContext();

        protected bool _mainSvcBLock = false;
        protected bool _logSvcBlock = false;

        public SmartKPIMinerAgent()
        {
        }

        public Task LogProcess()
        {
            if (!_logSvcBlock)
            {
                _logSvcBlock = true;
                context.LogManager.ProcessLogQueue();
                _logSvcBlock = false;
            }

            //var hasNoLock = false;
            //Monitor.TryEnter(_logSvcLocker, ref hasNoLock);
            //if (hasNoLock)
            //{

            //    Monitor.Exit(_logSvcLocker);
            //}
            return Task.CompletedTask;
        }

        public async Task<bool> KPIProcessAsync()
        {
            if (!_mainSvcBLock)
            {
                _mainSvcBLock = true;
                ServiceManager.Initialize();
                await context.ProcessItemsAsync();
                _mainSvcBLock = false;
            }
            else
            {
                return false;
            }

            //var hasNoLock = false;
            //Monitor.TryEnter(_mainSvcLocker, ref hasNoLock);
            //if (hasNoLock)
            //{
            //    ServiceManager.Initialize();
            //    await context.ProcessItemsAsync();
            //    Monitor.Exit(_mainSvcLocker);
            //}
            //else
            //{
            //    return false;
            //}

            return true;
        }

        public void ServiceTrigger()
        {
            _loggingTimer = new Timer(callback: async state => await LogProcess(), state: null, dueTime: 0, period: (long)TimeSpan.FromSeconds(10).TotalMilliseconds);
            _mainServiceTimer = new Timer(callback: async state => await KPIProcessAsync(), state: null, dueTime: 0, period: (long)TimeSpan.FromMinutes(15).TotalMilliseconds);
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            ServiceTrigger();
        }

        protected override void OnStop()
        {
            base.OnStop();

            _mainServiceTimer?.Change(dueTime: Timeout.Infinite, period: Timeout.Infinite);
            _mainServiceTimer.DisposeAsync();

            _loggingTimer?.Change(dueTime: Timeout.Infinite, period: Timeout.Infinite);
            _loggingTimer.DisposeAsync();
        }
    }
}