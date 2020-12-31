using DogusTeknoloji.SmartKPIMiner.Core;
using DogusTeknoloji.SmartKPIMiner.Helpers;
using DogusTeknoloji.SmartKPIMiner.Logging;
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

        protected Timer _loggingSvcTimer;
        protected bool notIsLoggingLocked = true;
        public SmartKPIMinerAgent()
        {
            ConsoleLogging.IsFileLoggingEnabled = true;
#if RELEASE
            ConsoleLogging.WindowsServiceMode = true;
#endif
        }
        public async Task KPIProcessAsync()
        {
            ConsoleLogging.LogLine("{{{PULSE}}} KPI Process", severity: ConsoleLogging.LogSeverity.Info);
            if (notIsProcessLocked)
            {
                notIsProcessLocked = false;
                ServiceManager.Initialize();
                await context.ProcessItemsAsync();
                notIsProcessLocked = true;
            }
            else
            {
                ConsoleLogging.LogLine("[[[BYPASS]]] KPI Process is locked and in progress!", severity: ConsoleLogging.LogSeverity.Warning);
            }
        }

        public Task LogProcessAsync()
        {
            ConsoleLogging.LogLine("{{{PULSE}}} Log Queue Process", severity: ConsoleLogging.LogSeverity.Info);
            if (notIsLoggingLocked)
            {
                notIsLoggingLocked = false;
                CommonFunctions.LogManager.ProcessLogQueue();
                notIsLoggingLocked = true;
            }
            else
            {
                ConsoleLogging.LogLine("[[[BYPASS]]] Log Queue Process is locked and in progress!", severity: ConsoleLogging.LogSeverity.Warning);
            }
            return Task.CompletedTask;
        }
        public void SimulateServiceStart()
        {
            this.OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            _mainServiceTimer = new Timer(callback: async state => await KPIProcessAsync(), state: null, dueTime: 0, period: (int)TimeSpan.FromMinutes(15).TotalMilliseconds);
            _loggingSvcTimer = new Timer(callback: state => LogProcessAsync(), state: null, dueTime: 0, period: (int)TimeSpan.FromSeconds(2).TotalMilliseconds);
        }
        protected override void OnStop()
        {
            base.OnStop();
            _mainServiceTimer?.Change(dueTime: Timeout.Infinite, period: 0);
            _mainServiceTimer.DisposeAsync();

            _loggingSvcTimer?.Change(dueTime: Timeout.Infinite, period: 0);
            _loggingSvcTimer.DisposeAsync();
        }
    }
}