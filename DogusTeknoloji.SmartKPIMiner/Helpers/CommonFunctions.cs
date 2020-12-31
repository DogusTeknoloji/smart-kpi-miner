using DogusTeknoloji.SmartKPIMiner.Logging;
using System;
using System.IO;
using System.Reflection;

namespace DogusTeknoloji.SmartKPIMiner.Helpers
{
    public static class CommonFunctions
    {
        public static LogManager LogManager = new LogManager();
        public static DateTime UnixEPoch => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static readonly int UnifyingConstant = 15;
        public static long GetCurrentUnixTimestampMillisec(DateTime date)
        {
            DateTime localDateTime, universalDatetime;
            localDateTime = date;
            universalDatetime = localDateTime.ToUniversalTime();
            return (long)(universalDatetime - UnixEPoch).TotalMilliseconds;
        }

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

    }
}
