using LogTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogComponent
{
    public sealed class NullLogger<T> : ILog<T>
    {
        public List<Action> OnCritical { get; } = new List<Action>();

        public void LogWarning(string message, Exception exception)
        {
        }

        public void LogWarning(string message, params object[] arguments)
        {
        }

        public void LogInformation(string message, Exception exception)
        {
        }

        public void LogInformation(string message)
        {
        }

        public void LogDebug(string message, Exception exception)
        {
        }

        public void LogDebug(string message, params object[] arguments)
        {
        }

        public void LogTrace(string message, params object[] arguments)
        {
        }

        public ILog MakeChild<TNewHost>()
        {
            return this;
        }

        public void LogCritical(string message, Exception exception)
        {
            foreach (var action in OnCritical)
            {
                if (null != action)
                {
                    action();
                }
            }
        }

        public ILog MakeChild(LogProperty[] properties)
        {
            return this;
        }

        public void LogCritical(string message, params object[] arguments)
        {
            foreach (var action in OnCritical)
            {
                if (null != action)
                {
                    action();
                }
            }
        }

        public void LogInformation(string message, params object[] arguments)
        {
        }

		public void StopWithoutFlush()
		{
		}

		public void StopWithFlush()
		{
		}

		public void Write(string message)
		{
		}
	}
}
