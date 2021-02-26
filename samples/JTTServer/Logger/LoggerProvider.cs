using Library.Container;
using Library.Log;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTTServer
{
    public class NLoggerProvider : ILoggerProvider
    {
        public NLoggerProvider()
        {

        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        public void Dispose() => GC.SuppressFinalize(this);

        private class MyLogger : Microsoft.Extensions.Logging.ILogger
        {
            public MyLogger()
            {

            }

            static NLog.ILogger Logger => AutofacHelper.GetService<NLog.Logger>();

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                var log = new NLog.LogEventInfo(
                    NLog.LogLevel.FromString(logLevel.ToString()),
                    Logger.Name,
                    formatter(state, exception));
                log.Properties.Add("Microsoft.Extensions.Logging.LogLevel", logLevel);
                log.Exception = exception;
                Logger.Log(log);
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }
}
