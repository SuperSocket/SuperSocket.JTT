using JTTCustomServer.Logger;
using JTTCustomServer.Model;
using JTTCustomServer.Model.Config;
using JTTCustomServer.Model.MessageBody;
using Library.Container;
using Library.Extension;
using Microsoft.Extensions.DependencyInjection;
using SuperSocket;
using SuperSocket.JTTBase.Extension;
using SuperSocket.JTTBase.Interface;
using SuperSocket.JTTBase.Model;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JTTCustomServer.Handler
{
    public static class PackageHandler
    {
        static readonly SystemConfig Config = AutofacHelper.GetService<SystemConfig>();

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
        public static async ValueTask Handler(IAppSession session, IJTTPackageInfo packageInfo)
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
                        await LoggerHelper.LogAsync(
                             Microsoft.Extensions.Logging.LogLevel.Information,
                             Library.Models.LogType.系统跟踪,
                             $"0x0001, " +
                             $"\r\n\tA: {body_x0001.A}, " +
                             $"\r\n\tB: {body_x0001.B}.");

                        break;
                    default:
                        await LoggerHelper.LogAsync(
                    Microsoft.Extensions.Logging.LogLevel.Information,
                    Library.Models.LogType.系统跟踪,
                    $"\r\n\tMsg_ID: {Protocol.GetHandler().Decode(Protocol.GetHandler().Encode(packageInfo_JTTCustom.JTTCustomMessageHeader.Msg_ID, new CodeInfo { CodeType = CodeType.uint16_hex }), new CodeInfo { CodeType = CodeType.string_hex })}" +
                    $"\r\n\tBuffer: {string.Join('\t', packageInfo.Buffer.ToArray().Select(o => o.To0XString()))}.");

                        break;
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    Microsoft.Extensions.Logging.LogLevel.Error,
                    Library.Models.LogType.系统异常,
                    $"处理消息包时异常, " +
                    $"\r\n\tSessionID: {session.SessionID}, " +
                    $"\r\n\tBuffer: {string.Join('\t', packageInfo.Buffer.ToArray().Select(o => o.To0XString()))}.",
                    ex);
            }
        }
    }
}
