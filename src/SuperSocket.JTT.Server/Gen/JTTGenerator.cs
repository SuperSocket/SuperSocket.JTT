using SuperSocket.JTT.Server.Model;
using SuperSocket.JTT.Base.Extension;
using SuperSocket.JTT.Base.Filter;
using SuperSocket.JTT.Base.Interface;
using SuperSocket.JTT.Base.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SuperSocket;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using SuperSocket.JTT.Base.Hadnler;

namespace SuperSocket.JTT.Server.Gen
{
    /// <summary>
    /// 微信服务生成器
    /// </summary>
    public class JTTGenerator : IJTTProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public JTTGenerator(JTTGenOptions options)
        {
            Options = options ?? new JTTGenOptions();
        }

        #region 私有成员

        JTTGenOptions Options { get; }

        ISuperSocketHostBuilder Builder { get; set; }

        #endregion

        public ISuperSocketHostBuilder GetJTTServerHostBuilder()
        {
            if (Builder != null)
                return Builder;

            //var dll = $"{AppDomain.CurrentDomain.BaseDirectory}/SuperSocket.{Options.ProtocolOptions.Version}.dll";
            //if (!File.Exists(dll))
            //    throw new JTTException($"未找到文件 {dll},请检查项目中是否添加了该NuGet包.");

            var assemblyName = Options.ProtocolOptions.Version == JTTVersion.JTTCustom
                ? Options.ProtocolOptions.JTTCustomAssemblyName
                : $"SuperSocket.{Options.ProtocolOptions.Version}";

            var assembly = Assembly.Load(assemblyName);
            if (assembly == null)
            {
                if (Options.ProtocolOptions.Version == JTTVersion.JTTCustom)
                    throw new JTTException($"未找到命名空间 {assemblyName}.");
                else
                    throw new JTTException($"未找到命名空间 {assemblyName},请检查项目中是否添加了 SuperSocket.{Options.ProtocolOptions.Version} NuGet包.");
            }

            var jttTypes = new Dictionary<Type, Type>();

            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (JTTTypes.Protocol.IsAssignableFrom(type))
                    jttTypes.Add(JTTTypes.Protocol, type);
                //else if (JTTTypes.PipelineFilter.IsAssignableFrom(type))
                //    jttTypes.Add(JTTTypes.PipelineFilter, type);
                //else if (JTTTypes.PackageInfo.IsAssignableFrom(type))
                //    jttTypes.Add(JTTTypes.PackageInfo, type);
                //else if (JTTTypes.MessageHeader.IsAssignableFrom(type))
                //    jttTypes.Add(JTTTypes.MessageHeader, type);
                //else if (JTTTypes.Decoder.IsAssignableFrom(type))
                //    jttTypes.Add(JTTTypes.Decoder, type);
                //else if (JTTTypes.Encoder.IsAssignableFrom(type))
                //    jttTypes.Add(JTTTypes.Encoder, type);

                if (jttTypes.Count == 5)
                    break;
            }

            if (!jttTypes.ContainsKey(JTTTypes.Protocol)) throw new JTTException($"未找到继承了 {JTTTypes.Protocol.FullName}的类.");
            //if (!jttTypes.ContainsKey(JTTTypes.PipelineFilter)) throw new JTTException($"未找到继承了 {JTTTypes.PipelineFilter.FullName}的类.");
            //if (!jttTypes.ContainsKey(JTTTypes.PackageInfo)) throw new JTTException($"未找到继承了 {JTTTypes.PackageInfo.FullName}的类.");
            //if (!jttTypes.ContainsKey(JTTTypes.MessageHeader)) throw new JTTException($"未找到继承了 {JTTTypes.MessageHeader.FullName}的类.");
            //if (!jttTypes.ContainsKey(JTTTypes.Decoder)) jttTypes[JTTTypes.Decoder] = typeof(JTTDecoder);//throw new JTTException($"未找到继承了 {JTTTypes.Decoder.FullName}的类.");
            //if (!jttTypes.ContainsKey(JTTTypes.Encoder)) throw new JTTException($"未找到继承了 {JTTTypes.Encoder.FullName}的类.");

            var protocol = typeof(JTTProtocolExtension).GetMethod(nameof(JTTProtocolExtension.GetProtocol), 1, new Type[] { typeof(string), typeof(JTTVersion) })
                .MakeGenericMethod(jttTypes[typeof(IJTTProtocol)])
                .Invoke(null, new object[] { Options.ProtocolOptions.ConfigFilePath, Options.ProtocolOptions.Version }) as IJTTProtocol;

            protocol.Initialization();

            if (protocol.Decoder == null)
                throw new JTTException($"请在IJTTProtocol.Initialization方法中指定Decoder.");

            if (protocol.Encoder == null)
                throw new JTTException($"请在IJTTProtocol.Initialization方法中指定Encoder.");

            if (Options.ProtocolOptions.Structures?.Any() == true)
                protocol.Structures = Options.ProtocolOptions.Structures;

            if (Options.ProtocolOptions.DataMappings?.Any() == true)
                protocol.DataMappings = Options.ProtocolOptions.DataMappings;

            if (Options.ProtocolOptions.InternalEntitysMappings?.Any() == true)
                protocol.InternalEntitysMappings = Options.ProtocolOptions.InternalEntitysMappings;

            //var handler = protocol.GetHandler();
            //var head = handler.GetFrameValue(protocol.HeadFrame);
            //var end = handler.GetFrameValue(protocol.EndFrame);

            //var pipelineFilter = jttTypes[JTTTypes.PipelineFilter].GetConstructor(new Type[] { typeof(byte[]), typeof(byte[]), JTTTypes.Decoder })
            //    .Invoke(new object[] { head, end, protocol.Decoder }) as IPipelineFilter<IJTTPackageInfo>;

            var supersocketHostBuilder = typeof(SuperSocketHostBuilder).GetMethod(nameof(SuperSocketHostBuilder.Create), 1, Type.EmptyTypes)
                .MakeGenericMethod(JTTTypes.PackageInfo)
                .Invoke(null, null) as ISuperSocketHostBuilder<IJTTPackageInfo>;

            supersocketHostBuilder.UsePipelineFilterFactory(o => Options.ProtocolOptions.PipelineFilterFactory?.Invoke(protocol, o) ?? protocol.GetFilter());

            IHostBuilder hostBuilder = null;

            if (Options.ServerOptions != null)
            {
                supersocketHostBuilder.UsePackageHandler(
                    Options.ServerOptions.PackageHandler,
                    Options.ServerOptions.ErrorHandler);

                typeof(ISuperSocketHostBuilder<IJTTPackageInfo>).GetMethod(nameof(ISuperSocketHostBuilder<IJTTPackageInfo>.UsePackageDecoder), 1, Type.EmptyTypes)
                    .MakeGenericMethod(protocol.Decoder.GetType())
                    .Invoke(supersocketHostBuilder, null);

                supersocketHostBuilder.UseSessionHandler(
                    Options.ServerOptions.OnConnected,
                    Options.ServerOptions.OnClosed);

                if (Options.ServerOptions.InProcSessionContainer)
                    supersocketHostBuilder.UseInProcSessionContainer();

                hostBuilder = supersocketHostBuilder.ConfigureServices((context, services) =>
                {
                    services.AddSingleton(protocol);
                    Options.ServerOptions.ConfigureServices?.Invoke(context, services, protocol);
                })
                .ConfigureAppConfiguration((hostCtx, configApp) =>
                {
                    configApp.AddInMemoryCollection(new Dictionary<string, string>
                           {
                                { "serverOptions:name", Options.ServerOptions?.Name ?? $"{Options.ProtocolOptions.Version} Server" },
                                { "serverOptions:listeners:0:ip", Options.ServerOptions?.IP },
                                { "serverOptions:listeners:0:port", Options.ServerOptions?.Port.ToString() },
                                { "serverOptions:listeners:0:backLog", Options.ServerOptions?.BackLog.ToString() }
                           });

                    Options.ServerOptions.ConfigureAppConfiguration?.Invoke(hostCtx, configApp, protocol);
                });
            }

            if (Options.LoggingOptions != null)
                hostBuilder = (hostBuilder ?? supersocketHostBuilder).ConfigureLogging((hostCtx, loggingBuilder) =>
                 {
                     if (Options.LoggingOptions?.Provider != null)
                         loggingBuilder.AddProvider(Options.LoggingOptions.Provider);
                     if (Options.LoggingOptions?.AddConsole == true)
                         loggingBuilder.AddConsole();
                     if (Options.LoggingOptions?.AddDebug != null)
                         loggingBuilder.AddDebug();
                 });

            Builder = (hostBuilder ?? supersocketHostBuilder) as ISuperSocketHostBuilder;

            if (Options.ServerOptions.UseUdp)
                Builder.UseUdp();

            return Builder;
        }

        public IHost GetJTTServer()
        {
            return GetJTTServerHostBuilder()
                .Build();
        }
    }
}
