using LogTest;
using Serilog;
using Serilog.Core.Enrichers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComponent.Serilog
{
    public sealed class SerilogAdapter<T> : ILog<T>
    {
        private readonly ILogger _delegate;


        public SerilogAdapter(ILogger parent)
        {
            _delegate = parent.ForContext<T>();
        }

        private SerilogAdapter(ILogger parent, LogProperty[] properties)
        {
            _delegate = parent.ForContext(properties.Select(loggingProperty => new PropertyEnricher(loggingProperty.Name, loggingProperty.Value)).ToArray());
        }

        public ILog MakeChild(LogProperty[] properties)
        {
            return new SerilogAdapter<T>(_delegate, properties);
        }

        public ILog MakeChild<TChildType>()
        {
            return new SerilogAdapter<TChildType>(_delegate);
        }

        public void LogCritical(string message, Exception exception)
        {
            _delegate.Error(exception, LogRecord.FormatExceptionMessage(message, exception));
        }

        public void LogCritical(string message, params object[] arguments)
        {
            _delegate.Error(message, arguments);
        }

        public void LogWarning(string message, Exception exception)
        {
            _delegate.Warning(exception, LogRecord.FormatExceptionMessage(message, exception));
        }

        public void LogWarning(string message, params object[] arguments)
        {
            _delegate.Warning(message, arguments);
        }

        public void LogInformation(string message, Exception exception)
        {
            _delegate.Information(exception, LogRecord.FormatExceptionMessage(message, exception));
        }

        public void LogInformation(string message)
        {
            _delegate.Information(message);
        }

        public void LogInformation(string message, params object[] arguments)
        {
            _delegate.Information(message, arguments);
        }

        public void LogDebug(string message, Exception exception)
        {
            _delegate.Debug(exception, LogRecord.FormatExceptionMessage(message, exception));
        }

        public void LogDebug(string message, params object[] arguments)
        {
            _delegate.Debug(message, arguments);
        }

        public void LogTrace(string message, params object[] arguments)
        {
            _delegate.Verbose(message, arguments);
        }

		public void StopWithoutFlush()
		{
			throw new NotImplementedException();
		}

		public void StopWithFlush()
		{
			throw new NotImplementedException();
		}

		public void Write(string message)
		{
			throw new NotImplementedException();
		}
	}
}
