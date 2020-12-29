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
        protected long _mainSvcDueTime = 0, _mainSvcPeriod = 0;
        protected long _logSvcDueTime = 0, _logSvcPeriod = 0;

        public SmartKPIMinerAgent()
        {
            this._mainSvcDueTime = 0;
            this._mainSvcPeriod = (long)TimeSpan.FromMinutes(15).TotalMilliseconds;

            this._logSvcDueTime = 0;
            this._logSvcPeriod = (long)TimeSpan.FromSeconds(10).TotalMilliseconds;

            _loggingTimer = new Timer(callback: state => LogProcessAsync(), state: null, dueTime: Timeout.Infinite, period: Timeout.Infinite);
            _mainServiceTimer = new Timer(callback: async state => await KPIProcessAsync(), state: null, dueTime: Timeout.Infinite, period: Timeout.Infinite);
        }

        public void LogProcessAsync()
        {
            _loggingTimer?.Change(dueTime: Timeout.Infinite, period: Timeout.Infinite);
            context.LogManager.ProcessLogQueue();
            _loggingTimer?.Change(dueTime: _logSvcDueTime, period: _logSvcPeriod);
        }

        public async Task KPIProcessAsync()
        {
            _mainServiceTimer?.Change(dueTime: Timeout.Infinite, period: Timeout.Infinite);
            ServiceManager.Initialize();
            await context.ProcessItemsAsync();
            _mainServiceTimer?.Change(dueTime: _mainSvcDueTime, period: _mainSvcPeriod);
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            _mainServiceTimer?.Change(dueTime: _mainSvcDueTime, period: _mainSvcPeriod);
            _loggingTimer?.Change(dueTime: _logSvcDueTime, period: _logSvcPeriod);
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