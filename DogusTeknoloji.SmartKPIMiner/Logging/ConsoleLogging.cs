using DogusTeknoloji.SmartKPIMiner.Helpers;
using System;

namespace DogusTeknoloji.SmartKPIMiner.Logging
{
    public static class ConsoleLogging
    {
        public enum LogSeverity { Verbose = 0, Info = 1, Warning = 2, Error = 3 }
        public static bool IsFileLoggingEnabled = false;
        public static bool WindowsServiceMode = false;

        private static string _header = $"[{DateTime.Now}] - ";
        private static bool _setBackground = true;

        private static void SetSeverity(LogSeverity severity)
        {
            if (WindowsServiceMode) { return; }
            if (_setBackground)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();
                _setBackground = false;
            }

            Console.ForegroundColor = severity switch
            {
                LogSeverity.Info => ConsoleColor.Cyan,
                LogSeverity.Warning => ConsoleColor.Yellow,
                LogSeverity.Error => ConsoleColor.Red,
                _ => ConsoleColor.White,
            };
        }

        private static void BaseLog(string text, LogSeverity severity, bool header)
        {
            if (!WindowsServiceMode)
            {
                SetSeverity(severity);
                _header = $"[{DateTime.Now}] - ";
                string headerText = header ? _header : "";
                Console.Write(headerText + text);
            }

            if (IsFileLoggingEnabled)
            {
                CommonFunctions.LogManager.Log(text);
            }
        }
        private static void BaseLogLine(string text, LogSeverity severity, bool header)
        {
            if (!WindowsServiceMode)
            {
                SetSeverity(severity);
                _header = $"[{DateTime.Now}] - ";
                string headerText = header ? _header : "";
                Console.WriteLine(headerText + text);
            }

            if (IsFileLoggingEnabled)
            {
                CommonFunctions.LogManager.Log(text);
            }
        }

        public static void LogLine(string text)
        {
            BaseLogLine(text, severity: LogSeverity.Verbose, header: true);
        }
        public static void LogLine(string text, LogSeverity severity)
        {
            BaseLogLine(text, severity, header: true);
        }
        public static void LogLine(string text, bool header)
        {
            BaseLogLine(text, severity: LogSeverity.Verbose, header);
        }
        public static void LogLine(string text, LogSeverity severity, bool header)
        {
            BaseLogLine(text, severity, header);
        }

        public static void Log(string text)
        {
            BaseLog(text, severity: LogSeverity.Verbose, header: true);
        }
        public static void Log(string text, LogSeverity severity)
        {
            BaseLog(text, severity, header: true);
        }
        public static void Log(string text, bool header)
        {
            BaseLog(text, severity: LogSeverity.Verbose, header);
        }
        public static void Log(string text, LogSeverity severity, bool header)
        {
            BaseLog(text, severity, header);
        }
    }
}
