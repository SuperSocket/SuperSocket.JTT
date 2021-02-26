using Autofac.Extensions.DependencyInjection;
using JTTCustomServer.Handler;
using JTTCustomServer.Logger;
using JTTCustomServer.Model.Config;
using Library.Configuration;
using Library.Container;
using Library.Log;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperSocket;
using SuperSocket.JTT.Gen;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JTTCustomServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //注册控制台跟踪日志
            LoggerConfigura.RegisterConsoleTargetForTrace();

            var log = new NLogHelper("Launch Trace").GetLogger();

            log.Trace("系统启动中.");
            log.Trace("正在读取配置.");

            var config = new ConfigHelper()
                .GetModel<SystemConfig>("SystemConfig");

            if (config == null)
            {
                log.Trace("系统配置读取失败, appsettings.json Section: SystemConfig.");
                return;
            }

            Console.Title = config.ProjectName;

            log.Trace($"使用{config.JTTVersion}协议.");

            var services = new ServiceCollection();

            services.AddSingleton(config);

            //注册文件日志
            if (config.LoggerType == LoggerType.File)
            {
                LoggerConfigura.RegisterFileTarget();
                services.AddSingleton(new NLogHelper(LoggerConfig.LoggerName).GetLogger());
            }
            else
                services.AddSingleton(log);

            JTTServerConfigura.RegisterServices(services, config);

            log.Trace("已应用Autofac容器");
            AutofacHelper.Container = new AutofacServiceProviderFactory().CreateBuilder(services).Build();

            var jttProvider = AutofacHelper.GetService<IJTTProvider>();

            log.Trace("正在构建主机");

            var jttHostBuilder = jttProvider.GetJTTServerHostBuilder();

            var jttHost = jttHostBuilder.Build();

            PackageHandler.SetUp(jttHost.Services);

            log.Trace("应用程序已启动");

            await jttHost.RunAsync();

            CancellationToken cancelToken = new CancellationToken(false);
            await jttHost.RunAsync(cancelToken)
                .ContinueWith(task =>
                {
                    try
                    {
                        cancelToken.ThrowIfCancellationRequested();
                    }
                    catch (Exception ex)
                    {
                        LoggerHelper.Log(
                            Microsoft.Extensions.Logging.LogLevel.Error,
                            Library.Models.LogType.系统异常,
                            $"应用程序关闭时时异常.",
                            ex);
                    }
                });
        }
    }
}
