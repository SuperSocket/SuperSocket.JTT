using JTTCustomServer.Handler;
using JTTCustomServer.Model.Config;
using Microservice.Library.Container;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace JTTCustomServer
{
    /// <summary>
    /// JTT协议服务器配置类
    /// </summary>
    public static class JTTServerConfigura
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, SystemConfig config)
        {
            services.AddJTT(options =>
            {
                options.ProtocolOptions = new SuperSocket.JTT.Server.Model.JTTProtocolOptions
                {
                    Version = config.JTTVersion,
                    JTTCustomAssemblyName = Assembly.GetExecutingAssembly().GetName().Name,
                    ConfigFilePath = config.JTTConfigFilePath,

                    Structures = config.Structures,
                    DataMappings = config.DataMappings,
                    InternalEntitysMappings = config.InternalEntitysMappings,
                };

                options.ServerOptions = new SuperSocket.JTT.Server.Model.JTTServerOptions
                {
                    Name = config.ServerName,
                    IP = config.ServerIP,
                    Port = config.ServerPort,
                    BackLog = config.ServerBackLog,

                    UseUdp = true,

                    PackageHandler = PackageHandler.Handler,
                    OnConnected = SessionHandler.OnConnected,
                    OnClosed = SessionHandler.OnClosed,

                    InProcSessionContainer = true,

                    ConfigureServices = (context, services, protocol) =>
                    {

                    },
                    ConfigureAppConfiguration = (hostCtx, configApp, protocol) =>
                    {

                    }
                };

                options.LoggingOptions = new SuperSocket.JTT.Server.Model.JTTLoggingOptions
                {
                    AddConsole = true,
                    AddDebug = true,
                    Provider = AutofacHelper.GetService<ILoggerProvider>()
                };
            });

            return services;
        }
    }
}
