﻿using DogusTeknoloji.SmartKPIMiner.Core;
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

        public SmartKPIMinerAgent()
        {

        }
        public Task DebugLogAsync()
        {
            _loggingTimer = new Timer(callback: state => context.LogManager.ProcessLogQueue(), state: null, dueTime: 0, period: (long)TimeSpan.FromSeconds(10).TotalMilliseconds);
            return Task.CompletedTask;
        }

        public void LogProcessAsync()
        {
            _loggingTimer?.Change(dueTime: Timeout.Infinite, period: 0);
            context.LogManager.ProcessLogQueue();
            _loggingTimer?.Change(dueTime: 0, period: (long)TimeSpan.FromSeconds(10).TotalMilliseconds);
        }

        public async Task KPIProcessAsync()
        {
            _mainServiceTimer?.Change(dueTime: Timeout.Infinite, period: 0);
            ServiceManager.Initialize();
            await context.ProcessItemsAsync();
            _mainServiceTimer?.Change(dueTime: 0, period: (long)TimeSpan.FromMinutes(15).TotalMilliseconds);
        }
        public async Task Wait()
        {
            while (true)
            {
                Thread.Sleep(Timeout.InfiniteTimeSpan);
            }
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            _loggingTimer = new Timer(callback: state => LogProcessAsync(), state: null, dueTime: 0, period: (long)TimeSpan.FromSeconds(10).TotalMilliseconds);
            _mainServiceTimer = new Timer(callback: async state => await KPIProcessAsync(), state: null, dueTime: 0, period: (long)TimeSpan.FromMinutes(15).TotalMilliseconds);
            Wait();
        }
        protected override void OnStop()
        {
            base.OnStop();
            _mainServiceTimer?.Change(dueTime: Timeout.Infinite, period: 0);
            _mainServiceTimer.DisposeAsync();
            _loggingTimer?.Change(dueTime: Timeout.Infinite, period: 0);
            _loggingTimer.Dispose();
        }
    }
}