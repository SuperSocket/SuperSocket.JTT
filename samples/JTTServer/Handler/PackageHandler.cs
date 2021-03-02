using JTTServer.Config;
using JTTServer.Log;
using JTTServer.MessageBody;
using Microservice.Library.Container;
using Microservice.Library.Extension;
using Microsoft.Extensions.DependencyInjection;
using SuperSocket;
using SuperSocket.JTT.Base.Interface;
using SuperSocket.JTT.Base.Model;
using SuperSocket.JTT.JTT809;
using SuperSocket.JTT.JTT809.MessageBody;
using System;
using System.Buffers;
using System.Linq;
using System.Threading.Tasks;

namespace JTTServer
{
    public static class PackageHandler
    {
        static readonly JTT809Protocol Protocol = AutofacHelper.GetService<IJTTProtocol>() as JTT809Protocol;

        static readonly ForwardMiddleware Forward = AutofacHelper.GetService<IServiceProvider>()
            .GetServices<IMiddleware>()
            .OfType<ForwardMiddleware>()
            .FirstOrDefault();

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
                    if (packageInfo.Step > DecoderStep.AnalysisBuffer)
                        Forward.Add(session.SessionID,
                            session.RemoteEndPoint,
                            packageInfo.HeadFlag.ToArray()
                                .Concat(packageInfo.Buffer.ToArray())
                                .Concat(packageInfo.Crc_Code.ToArray())
                                .Concat(packageInfo.EndFlag.ToArray())
                                .ToArray());

                    throw packageInfo.Exception;
                }

                var packageInfo_JTT809 = packageInfo as JTT809PackageInfo;
                switch (packageInfo_JTT809.JTT809MessageHeader.Msg_ID)
                {
                    case MsgID.LoginRequest:
                        var body_LoginRequest = packageInfo_JTT809.MessageBody as LoginRequestBody;
                        Logger.Log(
                             NLog.LogLevel.Trace,
                             LogType.系统跟踪,
                             $"LoginRequest, " +
                             $"\r\n\tUserID: {body_LoginRequest.UserID}, " +
                             $"\r\n\tPassword: {body_LoginRequest.Password}, " +
                             $"\r\n\tMsg_GnsscenterID: {body_LoginRequest.Msg_GnsscenterID}, " +
                             $"\r\n\tDown_link_IP: {body_LoginRequest.Down_link_IP}, " +
                             $"\r\n\tDown_link_Port: {body_LoginRequest.Down_link_Port}.");

                        await AuthenticateHandler.Authenticate(session, body_LoginRequest);
                        break;
                    case MsgID.GetForwardEndpointRequest:
                        var body_GetForwardEndpointRequest = packageInfo_JTT809.MessageBody as GetForwardEndpointRequestBody;
                        Logger.Log(
                             NLog.LogLevel.Trace,
                             LogType.系统跟踪,
                             $"GetForwardEndpointRequest.");

                        await Forward.GetClientList(session);
                        break;
                    case MsgID.ForwardRequest:
                        var body_ForwardRequest = packageInfo_JTT809.MessageBody as ForwardRequestBody;
                        Logger.Log(
                          NLog.LogLevel.Trace,
                          LogType.系统跟踪,
                          $"ForwardRequest, " +
                          $"\r\n\tTarget_IP: {body_ForwardRequest.Target_IP}, " +
                          $"\r\n\tTarget_Port: {body_ForwardRequest.Target_Port}.");

                        await Forward.Add(session, body_ForwardRequest.Target_IP, body_ForwardRequest.Target_Port);
                        break;
                    case MsgID.CancelForwardRequest:
                        var body_CancelForwardRequest = packageInfo_JTT809.MessageBody as CancelForwardRequestBody;
                        Logger.Log(
                             NLog.LogLevel.Trace,
                             LogType.系统跟踪,
                             $"CancelForwardRequest.");

                        await Forward.Remove(session);
                        break;
                    default:
                        Logger.Log(
                            NLog.LogLevel.Trace,
                            LogType.系统跟踪,
                            $"\r\n\tMsg_ID: {Protocol.JTT809Handler.Decode(Protocol.JTT809Handler.Encode(packageInfo_JTT809.JTT809MessageHeader.Msg_ID, new CodeInfo { CodeType = CodeType.uint16_hex }), new CodeInfo { CodeType = CodeType.string_hex })}");

                        Forward.Add(
                            session.SessionID,
                            session.RemoteEndPoint,
                            packageInfo.HeadFlag.ToArray()
                                .Concat(packageInfo.Buffer.ToArray())
                                .Concat(packageInfo.Crc_Code.ToArray())
                                .Concat(packageInfo.EndFlag.ToArray())
                                .ToArray());
                        break;
                }
            }
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
        }
    }
}
