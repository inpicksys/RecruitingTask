using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogComponent;
using System.Collections.Immutable;

namespace LogTest
{
	public class AsyncLogger : ILog
	{
		private Task logger;
		private CancellationTokenSource cancelToken;
		private AutoResetEvent awaitReplyOnRequestEvent = new AutoResetEvent(false);

		private ImmutableList<LogRecord> _lines = ImmutableList.Create<LogRecord>();

		private StreamWriter _writer;

		private bool _exit;
		private string dateFormat = "yyyyMMdd HHmmss fff";
		public string defaultLogDirectory = "/LogTest/Log";

		public string LogsFolder {
			get {
				var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, defaultLogDirectory);
				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);
				return dir;
			}
		}
		public string TimestampRecordFormat {
			get {
				return "yyyy-MM-dd HH:mm:ss:fff";
			}
		}

		public string Header {
			get {
				return "Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine;
			}
		}

		public AsyncLogger()
		{

			

			var fileName = DateTime.UtcNow.ToString(dateFormat) + ".log";
			_writer = File.AppendText(Path.Combine(LogsFolder, DateTime.UtcNow.ToString(dateFormat) + ".log"));

			_writer.Write(Header);

			_writer.AutoFlush = true;
			cancelToken = new CancellationTokenSource();
			var token = cancelToken.Token;
			logger = Task.Run(() => this.MainLoop(), token);
		}

		private bool _QuitWithFlush = false;

		DateTime _curDate = DateTime.UtcNow;

		private void MainLoop()
		{
			while (!_exit)
			{
				if (_lines.Count > 0)
				{
					int f = 0;
					var _handled = new List<LogRecord>();

					foreach (var logRecord in _lines)
					{
						f++;

						if (f > 5)
							continue;

						if (!_exit || _QuitWithFlush)
						{
							_handled.Add(logRecord);

							var stringBuilder = new StringBuilder();

							if ((DateTime.UtcNow - _curDate).Days != 0)
							{
								_curDate = DateTime.UtcNow;
								this._writer = File.AppendText(Path.Combine(LogsFolder, DateTime.Now.ToString(dateFormat) + ".log"));
								_writer.Write("Timestamp".PadRight(25, ' ') + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine);
								stringBuilder.Append(Environment.NewLine);
								_writer.Write(stringBuilder.ToString());
								_writer.AutoFlush = true;
							}

							stringBuilder.Append(logRecord.Timestamp.ToString(TimestampRecordFormat));
							stringBuilder.Append("\t");
							stringBuilder.Append(logRecord.LineText());
							stringBuilder.Append("\t");

							stringBuilder.Append(Environment.NewLine);

							_writer.Write(stringBuilder.ToString());
						}
					}

					for (int y = 0; y < _handled.Count; y++)
					{
						_lines. Remove(_handled[y]);
					}

					if (_QuitWithFlush && _lines.Count == 0)
						_exit = true;

					Thread.Sleep(50);
				}
			}
		}

		public void StopWithoutFlush()
		{
			_exit = true;
			try
			{
				cancelToken.Cancel();
			}
			catch (TaskCanceledException)
			{
				Console.WriteLine("Task cancelled with timeout");
			}
			finally
			{
				cancelToken.Dispose();
			}
		}

		public void StopWithFlush()
		{
			_QuitWithFlush = true;
			try
			{
				cancelToken.CancelAfter(1000);
			}
			catch (TaskCanceledException)
			{
				Console.WriteLine("Task cancelled with timeout");
			}
			finally
			{
				cancelToken.Dispose();
			}

		}

		public void Write(string message)
		{
			_lines.Add(new LogRecord { Text = message, Timestamp = DateTime.UtcNow });
		}

		public void LogCritical(string message, Exception exception)
		{
			throw new NotImplementedException();
		}

		public void LogCritical(string message, params object[] arguments)
		{
			throw new NotImplementedException();
		}

		public void LogWarning(string message, Exception exception)
		{
			throw new NotImplementedException();
		}

		public void LogWarning(string message, params object[] arguments)
		{
			throw new NotImplementedException();
		}

		public void LogInformation(string message)
		{
			throw new NotImplementedException();
		}

		public void LogInformation(string message, Exception exception)
		{
			throw new NotImplementedException();
		}

		public void LogInformation(string message, params object[] arguments)
		{
			throw new NotImplementedException();
		}

		public void LogDebug(string message, Exception exception)
		{
			throw new NotImplementedException();
		}

		public void LogDebug(string message, params object[] arguments)
		{
			throw new NotImplementedException();
		}

		public void LogTrace(string message, params object[] arguments)
		{
			throw new NotImplementedException();
		}
	}
}