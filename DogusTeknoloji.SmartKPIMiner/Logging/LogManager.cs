using System;
using System.Collections.Generic;
using System.IO;

namespace DogusTeknoloji.SmartKPIMiner.Logging
{
    public class LogManager
    {
        const string LOG_DIRECTORY = @"C:\SmartKPIMiner_LOGS\Logs";
        private Queue<string> _logQueue = new Queue<string>();

        public void Log(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return; }
            _logQueue.Enqueue(text);
        }

        public void Log(string text, string header)
        {
            if (string.IsNullOrWhiteSpace(header)) { header = ""; }
            if (string.IsNullOrWhiteSpace(text)) { return; }
            _logQueue.Enqueue($"[{header}] - {text}");
        }

        public void ProcessLogQueue()
        {
            StreamWriter logWriter = GetLogStream();
            if (logWriter == null) { return; }
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
            try
            {
                AutoPathRepair(LOG_DIRECTORY);
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
            catch (IOException)
            {

            }

            return null;
        }

        public bool AutoPathRepair(string dirPath)
        {
            try
            {
                string CheckLoc = string.Empty;
                string[] location = dirPath.Split('\\');
                for (int i = 0; i < location.Length - 1; i++)
                {
                    CheckLoc = CheckLoc.Insert(CheckLoc.Length, string.Format("{0}\\", location[i]));
                    if (!Directory.Exists(CheckLoc))
                    {
                        Directory.CreateDirectory(CheckLoc);
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
