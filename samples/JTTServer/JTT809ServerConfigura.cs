using JTTServer.Config;
using Microsoft.Extensions.DependencyInjection;
using SuperSocket.JTT809;
using SuperSocket.JTTBase.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTTServer
{
    /// <summary>
    /// 配置JTT809服务器
    /// </summary>
    public class JTT809ServerConfigura
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterServices(IServiceCollection services, SystemConfig config)
        {
            services.AddJTT(options =>
            {
                options.ProtocolOptions = new SuperSocket.JTT.Model.JTTProtocolOptions
                {
                    Version = config.JTTVersion,
                    ConfigFilePath = config.JTTConfigFilePath,

                    Structures = config.Structures,
                    DataMappings = config.DataMappings,
                    InternalEntitysMappings = config.InternalEntitysMappings,
                };

                options.ServerOptions = new SuperSocket.JTT.Model.JTTServerOptions
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

                options.LoggingOptions = new SuperSocket.JTT.Model.JTTLoggingOptions
                {
                    AddConsole = true,
                    AddDebug = true,
                    Provider = new NLoggerProvider()
                };
            });
        }
    }
}
