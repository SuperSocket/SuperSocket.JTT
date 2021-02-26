using Library.Container;
using Library.Extension;
using Library.Log;
using Library.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JTTServer
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LoggerHelper
    {
        static readonly NLog.Logger Logger = AutofacHelper.GetService<NLog.Logger>();

        public static async Task LogAsync(Microsoft.Extensions.Logging.LogLevel logLevel, LogType logType, string message, Exception exception = null)
        {
            await Task.Run(() => { Log(logLevel, logType, message, exception); });
        }

        public static void Log(Microsoft.Extensions.Logging.LogLevel logLevel, LogType logType, string message, Exception exception = null)
        {
            var log = new NLog.LogEventInfo(
                NLog.LogLevel.FromString(logLevel.ToString()),
                LoggerConfig.LoggerName,
                message + (exception == null ? "" : $"\r\n\t{exception.GetExceptionAllMsg()}"))
            {
                Exception = exception
            };
            log.Properties[LoggerConfig.LogType] = logType.ToString();
            Logger.Log(log);
        }
    }
}
