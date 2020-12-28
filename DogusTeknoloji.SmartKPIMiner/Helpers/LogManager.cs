using System;
using System.Collections.Generic;
using System.IO;

namespace DogusTeknoloji.SmartKPIMiner.Helpers
{
    public class LogManager
    {
        private Queue<string> _logQueue = new Queue<string>();

        public void Log(string text)
        {
            _logQueue.Enqueue(text);
        }

        public void ProcessLogQueue()
        {
            StreamWriter logWriter = GetLogStream();
            while (_logQueue.Count > 0)
            {
                string item = _logQueue.Dequeue();
                string logText = $"{DateTime.Now} - {item}";
                logWriter.WriteLine(logText);
            }
            logWriter.Flush();
            logWriter.Close();
            logWriter.Dispose();
        }

        private StreamWriter GetLogStream()
        {
            const string LOG_DIRECTORY = "Logs";
            string logFileName = "SmartKPI_Log_" + DateTime.Now.ToString("yyyy-MM-dd");

            if (!Directory.Exists(LOG_DIRECTORY))
            {
                Directory.CreateDirectory(LOG_DIRECTORY);
            }

            string fullPath = Path.Combine(LOG_DIRECTORY, logFileName);
            bool headerFlag = false;

            if (!File.Exists(fullPath)) { headerFlag = true; }

            StreamWriter logWriter = new StreamWriter(path: fullPath, append: true);

            if (headerFlag)
            {
                logWriter.WriteLine($"------ SMART KPI MINER - PROCESS LOGS - {DateTime.Now} ------");
                logWriter.Flush();
            }

            return logWriter;
        }
    }
}
