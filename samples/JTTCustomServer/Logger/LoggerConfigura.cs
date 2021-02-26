using NLog;
using NLog.Config;
using NLog.Targets;
using System.IO;
using System.Text;

namespace JTTCustomServer.Logger
{
    /// <summary>
    /// 注册日志
    /// </summary>
    public class LoggerConfigura
    {
        static LoggerConfigura()
        {
            LoggingConfiguration = new LoggingConfiguration();
        }

        private static LoggingConfiguration LoggingConfiguration { get; set; }

        public static void RegisterConsoleTargetForTrace()
        {
            var target = new ConsoleTarget()
            {
                Name = "Launch Trace",
                Layout = LoggerConfig.LayoutSimplify
            };
            LoggingConfiguration.AddTarget(target);
            LoggingConfiguration.AddRule(
                LogLevel.Trace,
                LogLevel.Fatal,
                target.Name);
            LogManager.Configuration = LoggingConfiguration;
        }

        public static void RegisterFileTarget()
        {
            var target = new FileTarget()
            {
                Name = LoggerConfig.LoggerName,
                Layout = LoggerConfig.LayoutSimplify,
                FileName = Path.Combine(Directory.GetCurrentDirectory(), "logs", "${date:format=yyyy-MM-dd}.txt"),
                Encoding = Encoding.UTF8
            };
            LoggingConfiguration.AddTarget(target);
            LoggingConfiguration.AddRule(LogLevel.Debug, LogLevel.Fatal, target);
            LogManager.Configuration = LoggingConfiguration;
        }
    }
}
