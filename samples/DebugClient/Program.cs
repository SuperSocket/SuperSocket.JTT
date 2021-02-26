using Autofac.Extensions.DependencyInjection;
using Library.Configuration;
using Library.Container;
using Library.Log;
using Library.ConsoleTool;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DebugClient
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

            var service = new ServiceCollection();

            service.AddSingleton(config);

            //注册文件日志
            if (config.LoggerType == Library.Models.LoggerType.File)
            {
                LoggerConfigura.RegisterFileTarget();
                service.AddSingleton(new NLogHelper(LoggerConfig.LoggerName).GetLogger());
            }
            else
                service.AddSingleton(log);

            AutofacHelper.Container = new AutofacServiceProviderFactory()
                .CreateBuilder(service)
                .Build();

            log.Trace("已应用Autofac容器.");

            var client = new Client();

            log.Trace("已创建TCP客户端.");

            var command = string.Empty;
            while (command != "exit")
            {
                command = Extension.ReadInput()?.ToLower().Trim();
                switch (command)
                {
                    case "connect":
                        await client.ConnectAsync();
                        break;
                    case "close":
                        await client.CloseAsync();
                        break;
                    case "login":
                        await client.Login();
                        break;
                    case "gf":
                        await client.GetForward();
                        break;
                    case "f":
                        await client.Forward();
                        break;
                    case "tf":
                        await client.TestForward();
                        break;
                    case "cf":
                        await client.CancelForward();
                        break;
                    case "1078":
                        await client.SendJTT1078Msg();
                        break;
                    default:
                        log.Trace($"不支持的指令: <{command}>.");
                        break;
                }
            }
        }
    }
}