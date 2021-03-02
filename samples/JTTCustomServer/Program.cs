using Autofac.Extensions.DependencyInjection;
using JTTCustomServer.Handler;
using JTTCustomServer.Log;
using JTTCustomServer.Model.Config;
using Microservice.Library.Configuration;
using Microservice.Library.ConsoleTool;
using Microservice.Library.Container;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperSocket.JTT.Server.Gen;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JTTCustomServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            "系统启动中.".ConsoleWrite();
            "正在读取配置.".ConsoleWrite();

            var config = new ConfigHelper()
                .GetModel<SystemConfig>("SystemConfig");

            if (config == null)
            {
                "系统配置读取失败, appsettings.json Section: SystemConfig.".ConsoleWrite();
                return;
            }

            Console.Title = config.ProjectName;

            $"使用{config.JTTVersion}协议.".ConsoleWrite();

            var services = new ServiceCollection();

            services.AddSingleton(config)
                    .AddLogging()
                    .RegisterNLog(config)
                    .RegisterServices(config);

            "已应用Autofac容器".ConsoleWrite();
            AutofacHelper.Container = new AutofacServiceProviderFactory().CreateBuilder(services).Build();

            var jttProvider = AutofacHelper.GetService<IJTTProvider>();

            "正在构建主机".ConsoleWrite();

            var jttHostBuilder = jttProvider.GetJTTServerHostBuilder();

            var jttHost = jttHostBuilder.Build();

            PackageHandler.SetUp(jttHost.Services);

            "应用程序已启动".ConsoleWrite();

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
                        Logger.Log(
                            NLog.LogLevel.Error,
                            LogType.系统异常,
                            $"应用程序关闭时时异常.",
                            null,
                            ex);
                    }
                });
        }
    }
}
