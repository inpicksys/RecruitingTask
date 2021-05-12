using Microsoft.Extensions.Configuration;
using Serilog.Debugging;
using System;
using System.Collections.Generic;
using System.IO;

using Serilog;
using Serilog.Exceptions;


namespace LogComponent.Serilog
{
    public static class SerilogSetup
    {
        public static void Perform(string writablePath, IConfiguration configuration)
        {
            var logsDirectory = LogLocation.GetLogsDirectory(writablePath);

            var selfLogPath = Path.Combine(logsDirectory, "Serilog.log");

            SelfLog.Enable(File.CreateText(selfLogPath));

            var logPath = Path.Combine(logsDirectory, "Log.log");

            Log.Logger
                = new LoggerConfiguration()
                    .Enrich.WithExceptionDetails()
                    .Enrich.WithMachineName()
                    .Enrich.WithThreadId()
                    .Enrich.FromLogContext()
                    .WriteTo.Async(a => a.File(logPath, fileSizeLimitBytes: 10 * 1024 * 1024, retainedFileCountLimit: 1, rollingInterval: RollingInterval.Day))
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
        }
    }
}
