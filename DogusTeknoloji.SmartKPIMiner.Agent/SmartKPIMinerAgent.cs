using DogusTeknoloji.SmartKPIMiner.Core;
using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace DogusTeknoloji.SmartKPIMiner.Agent
{
    public class SmartKPIMinerAgent : ServiceBase
    {
        static EventWaitHandle _waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, "wait-handle");
        static bool _signaled = false;
        // TO DO: Manual Reset Event'e bakılacak.
        protected Timer _mainServiceTimer;
        protected Timer _loggingTimer;
        protected OperationContext context = new OperationContext();

        protected long _mainSvcDueTime = 0, _mainSvcPeriod = 0;
        protected object _mainSvcLocker = new object();

        protected long _logSvcDueTime = 0, _logSvcPeriod = 0;
        protected object _logSvcLocker = new object();

        public SmartKPIMinerAgent()
        {
            this._mainSvcDueTime = 0;
            this._mainSvcPeriod = (long)TimeSpan.FromMinutes(15).TotalMilliseconds;

            this._logSvcDueTime = 0;
            this._logSvcPeriod = (long)TimeSpan.FromSeconds(10).TotalMilliseconds;
        }

        public Task LogProcess()
        {
            var hasNoLock = false;
            Monitor.TryEnter(_logSvcLocker, ref hasNoLock);
            if (hasNoLock)
            {
                context.LogManager.ProcessLogQueue();
                Monitor.Exit(_logSvcLocker);
            }
            return Task.CompletedTask;
        }

        public async Task<bool> KPIProcessAsync()
        {
            var hasNoLock = false;
            Monitor.TryEnter(_mainSvcLocker, ref hasNoLock);
            if (hasNoLock)
            {
                ServiceManager.Initialize();
                await context.ProcessItemsAsync();
                Monitor.Exit(_mainSvcLocker);
            }
            else
            {
                return false;
            }

            return true;
        }

        public void ServiceTrigger()
        {
            _loggingTimer = new Timer(callback: async state => await LogProcess(), state: null, dueTime: _logSvcDueTime, period: _logSvcPeriod);
            _mainServiceTimer = new Timer(callback: async state => await KPIProcessAsync(), state: null, dueTime: _mainSvcDueTime, period: _mainSvcPeriod);
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