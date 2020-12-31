using System;

namespace DogusTeknoloji.SmartKPIMiner.Logging
{
    public static class ConsoleLogging
    {
        public enum LogSeverity { Verbose = 0, Info = 1, Warning = 2, Error = 3 }

        private static string _header = $"[{DateTime.Now}] - ";
        private static bool _setBackground = true;

        private static void SetSeverity(LogSeverity severity)
        {
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

        public static void LogLine(string text)
        {
            SetSeverity(LogSeverity.Verbose);
            Console.WriteLine(_header + text);
        }
        public static void LogLine(string text, LogSeverity severity)
        {
            SetSeverity(severity);
            Console.WriteLine(_header + text);
        }
        public static void LogLine(string text, bool header)
        {
            SetSeverity(LogSeverity.Verbose);
            string headerText = header ? _header : "";
            Console.WriteLine(headerText + text);
        }
        public static void LogLine(string text, LogSeverity severity, bool header)
        {
            SetSeverity(severity);
            string headerText = header ? _header : "";
            Console.WriteLine(headerText + text);
        }

        public static void Log(string text)
        {
            SetSeverity(LogSeverity.Verbose);
            Console.Write(_header + text);
        }
        public static void Log(string text, LogSeverity severity)
        {
            SetSeverity(severity);
            Console.Write(_header + text);
        }
        public static void Log(string text, bool header)
        {
            SetSeverity(LogSeverity.Verbose);
            string headerText = header ? _header : "";
            Console.Write(headerText + text);
        }
        public static void Log(string text, LogSeverity severity, bool header)
        {
            SetSeverity(severity);
            string headerText = header ? _header : "";
            Console.Write(headerText + text);
        }
    }
}
