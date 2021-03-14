using JTTServer.Config;
using Microservice.Library.Container;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SuperSocket.JTT.JTT809;

namespace JTTServer
{
    /// <summary>
    /// 配置JTT809服务器
    /// </summary>
    public static class JTT809ServerConfigura
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection RegisterJTTServer(this IServiceCollection services, SystemConfig config)
        {
            services.AddJTT(options =>
            {
                options.ProtocolOptions = new SuperSocket.JTT.Server.Model.JTTProtocolOptions
                {
                    Version = config.JTTVersion,
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

                    PackageHandler = PackageHandler.Handler,
                    OnConnected = SessionHandler.OnConnected,
                    OnClosed = SessionHandler.OnClosed,

                    InProcSessionContainer = true,

                    ConfigureServices = (context, services, protocol) =>
                    {
                        ((JTT809Protocol)protocol).DefaultVersionFlag = config.JTT809VersionFlag;
                    },
                    ConfigureAppConfiguration = (hostCtx, configApp, protocol) =>
                    {

                    }
                };

                options.LoggingOptions = new SuperSocket.JTT.Server.Model.JTTLoggingOptions
                {
                    AddConsole = true,
                    AddDebug = true
                };
            });

            return services;
        }
    }
}
