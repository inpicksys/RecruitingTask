using System;

namespace LogTest
{
	public interface ILog
	{
		/// <summary>
		/// Stop the logging. If any outstadning logs theses will not be written to Log
		/// </summary>
		void StopWithoutFlush();

		/// <summary>
		/// Stop the logging. The call will not return until all all logs have been written to Log.
		/// </summary>
		void StopWithFlush();

		/// <summary>
		/// WriteLog a message to the Log.
		/// </summary>
		/// <param name="message">The s to written to the log</param>
		void Write(string message);

		void LogCritical(string message, Exception exception);
		void LogCritical(string message, params object[] arguments);

		void LogWarning(string message, Exception exception);
		void LogWarning(string message, params object[] arguments);

		void LogInformation(string message);
		void LogInformation(string message, Exception exception);
		void LogInformation(string message, params object[] arguments);

		void LogDebug(string message, Exception exception);
		void LogDebug(string message, params object[] arguments);

		void LogTrace(string message, params object[] arguments);
	}

	public interface ILog<TComponent> : ILog
	{

	}
}