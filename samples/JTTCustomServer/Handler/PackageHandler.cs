using JTTCustomServer.Log;
using JTTCustomServer.Model;
using JTTCustomServer.Model.Config;
using JTTCustomServer.Model.MessageBody;
using Microservice.Library.Container;
using Microservice.Library.Extension;
using Microsoft.Extensions.DependencyInjection;
using SuperSocket;
using SuperSocket.JTT.JTTBase.Extension;
using SuperSocket.JTT.JTTBase.Interface;
using SuperSocket.JTT.JTTBase.Model;
using System;
using System.Buffers;
using System.Linq;
using System.Threading.Tasks;

namespace JTTCustomServer.Handler
{
    public static class PackageHandler
    {
#pragma warning disable IDE0052 // 删除未读的私有成员
        static readonly SystemConfig Config = AutofacHelper.GetService<SystemConfig>();
#pragma warning restore IDE0052 // 删除未读的私有成员

        static JTTCustomProtocol Protocol;

        public static void SetUp(IServiceProvider serviceProvider)
        {
            Protocol = serviceProvider.GetService<IJTTProtocol>() as JTTCustomProtocol;
        }

        /// <summary>
        /// 处理消息包
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="packageInfo">消息包</param>
        /// <returns></returns>
#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        public static async ValueTask Handler(IAppSession session, IJTTPackageInfo packageInfo)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            try
            {
                if (!packageInfo.Success)
                {

                    throw packageInfo.Exception;
                }

                var packageInfo_JTTCustom = packageInfo as JTTCustomPackageInfo;
                switch (packageInfo_JTTCustom.JTTCustomMessageHeader.Msg_ID)
                {
                    case MsgID.自定义数据x0001:
                        var body_x0001 = packageInfo_JTTCustom.MessageBody as x0001;
                        Logger.Log(
                             NLog.LogLevel.Trace,
                             LogType.系统跟踪,
                             $"0x0001, " +
                             $"\r\n\tA: {body_x0001.A}, " +
                             $"\r\n\tB: {body_x0001.B}.");

                        break;
                    default:
                        Logger.Log(
                             NLog.LogLevel.Trace,
                             LogType.系统跟踪,
                             $"\r\n\tMsg_ID: {Protocol.GetHandler().Decode(Protocol.GetHandler().Encode(packageInfo_JTTCustom.JTTCustomMessageHeader.Msg_ID, new CodeInfo { CodeType = CodeType.uint16_hex }), new CodeInfo { CodeType = CodeType.string_hex })}" +
                             $"\r\n\tBuffer: {string.Join('\t', packageInfo.Buffer.ToArray().Select(o => o.To0XString()))}.");

                        break;
                }
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                Logger.Log(
                    NLog.LogLevel.Error,
                    LogType.系统异常,
                    $"处理消息包时异常, " +
                    $"\r\n\tSessionID: {session.SessionID}, " +
                    $"\r\n\tBuffer: {string.Join('\t', packageInfo.Buffer.ToArray().Select(o => o.To0XString()))}.",
                    null,
                    ex);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}
