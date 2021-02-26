using JTTCustomServer.Handler;
using JTTCustomServer.Logger;
using JTTCustomServer.Model.Config;
using Library.Container;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperSocket;
using SuperSocket.JTT.Gen;
using System.Reflection;
using System.Threading.Tasks;

namespace JTTCustomServer
{
    /// <summary>
    /// JTT协议服务器配置类
    /// </summary>
    public class JTTServerConfigura
    {
        public static void RegisterServices(IServiceCollection services, SystemConfig config)
        {
            services.AddJTT(options =>
            {
                options.ProtocolOptions = new SuperSocket.JTT.Model.JTTProtocolOptions
                {
                    Version = config.JTTVersion,
                    JTTCustomAssemblyName = Assembly.GetExecutingAssembly().GetName().Name,
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
