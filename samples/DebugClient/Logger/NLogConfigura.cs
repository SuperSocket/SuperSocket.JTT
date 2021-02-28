using Microservice.Library.NLogger.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NLog.Targets;
using System.IO;
using System.Text;

namespace DebugClient.Log
{
    /// <summary>
    /// NLog配置类
    /// </summary>
    public static class NLogConfigura
    {
        /// <summary>
        /// 注册NLog服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static IServiceCollection RegisterNLog(this IServiceCollection services, SystemConfig config)
        {
            services.AddNLogger(s =>
            {
                s.TargetGeneratorOptions
                .Add(new TargetGeneratorOptions
                {
                    MinLevel = NLog.LogLevel.FromOrdinal(config.MinLogLevel),
                    Target = config.LoggerType switch
                    {
                        LoggerType.File => new FileTarget
                        {
                            Name = config.LoggerName,
                            Layout = config.LoggerLayout ?? LoggerConfig.Layout,
                            FileName = Path.Combine(Directory.GetCurrentDirectory(), LoggerConfig.FileDic, LoggerConfig.FileName),
                            Encoding = Encoding.UTF8
                        },
                        _ => new ColoredConsoleTarget
                        {
                            Name = config.LoggerName,
                            Layout = config.LoggerLayout ?? LoggerConfig.Layout
                        },
                    }
                });
            })
            .AddMSLogger();

            return services;
        }

        /// <summary>
        /// 配置NLog
        /// </summary>
        /// <param name="app"></param>
        /// <param name="config"></param>
        public static IApplicationBuilder ConfiguraNLog(this IApplicationBuilder app, SystemConfig config)
        {
            return app;
        }
    }
}
