using System.IO;

namespace LogComponent
{
    public static class LogLocation
    {
        public static string GetLogsDirectory(string writablePath)
        {
            var logsDirectory = Path.Combine(writablePath, "LogTest");

            if (false == Directory.Exists(logsDirectory))
            {
                Directory.CreateDirectory(logsDirectory);
            }

            return logsDirectory;
        }
    }
}
