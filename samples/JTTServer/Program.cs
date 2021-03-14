using Autofac.Extensions.DependencyInjection;
using JTTServer.Config;
using JTTServer.Log;
using Microservice.Library.Configuration;
using Microservice.Library.ConsoleTool;
using Microservice.Library.Container;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperSocket;
using SuperSocket.JTT.Server.Gen;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JTTServer
{
    class Program
    {
#pragma warning disable IDE0060 // 删除未使用的参数
        static async Task Main(string[] args)
#pragma warning restore IDE0060 // 删除未使用的参数
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
                    .RegisterJTTServer(config);

            var provider = services.BuildServiceProvider();

            var jttProvider = provider.GetService<IJTTProvider>();

            "正在构建主机".ConsoleWrite();

            var jttHostBuilder = jttProvider.GetJTTServerHostBuilder();

            if (config.EnableForward)
                jttHostBuilder.UseMiddleware<ForwardMiddleware>();

            var jttHost = jttHostBuilder
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton(config)
                            .AddLogging()
                            .RegisterNLog(config);
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())//使用Autofac替换自带IOC
                .Build();

            AutofacHelper.Container = jttHost.Services.GetAutofacRoot();

            var jttServer = jttHost.AsServer();

            SessionHandler.SetSessionContainer(jttServer.GetSessionContainer());

            "应用程序已启动\r\n".ConsoleWrite();

#pragma warning disable IDE0090 // 使用 "new(...)"
            CancellationToken cancelToken = new CancellationToken(false);
#pragma warning restore IDE0090 // 使用 "new(...)"
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
