using Autofac.Extensions.DependencyInjection;
using DebugClient.Log;
using Microservice.Library.Configuration;
using Microservice.Library.ConsoleTool;
using Microservice.Library.Container;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DebugClient
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
                .RegisterNLog(config);

            AutofacHelper.Container = new AutofacServiceProviderFactory()
                .CreateBuilder(services)
                .Build();

            "已应用Autofac容器.".ConsoleWrite();

            var client = new Client();

            "已创建TCP客户端.".ConsoleWrite();

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
                        $"不支持的指令: <{command}>.".ConsoleWrite();
                        break;
                }
            }
        }
    }
}