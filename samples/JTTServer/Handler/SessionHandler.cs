using JTTServer.Config;
using Microservice.Library.Container;
using Microsoft.Extensions.DependencyInjection;
using SuperSocket;
using SuperSocket.Channel;
using SuperSocket.JTT.Base.Interface;
using SuperSocket.JTT.JTT809;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JTTServer
{
    /// <summary>
    /// 会话处理类
    /// </summary>
    public static class SessionHandler
    {
        static readonly SystemConfig Config = AutofacHelper.GetService<IServiceProvider>()
            .GetService<SystemConfig>();

        static readonly JTT809Protocol Protocol = AutofacHelper.GetService<IServiceProvider>()
            .GetService<IJTTProtocol>() as JTT809Protocol;

        static readonly ForwardMiddleware Forward = AutofacHelper.GetService<IServiceProvider>()
            .GetServices<IMiddleware>()
            .OfType<ForwardMiddleware>()
            .FirstOrDefault();

        static ISessionContainer SessionContainer;

        public static void SetSessionContainer(ISessionContainer sessionContainer)
        {
            SessionContainer = sessionContainer;
        }

        /// <summary>
        /// 已建立连接
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static async ValueTask OnConnected(IAppSession session)
        {
            Forward.Add(session.SessionID, session.RemoteEndPoint);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 连接已关闭
        /// </summary>
        /// <param name="session"></param>
        /// <param name="args"></param>
        /// <returns></returns>
#pragma warning disable IDE0060 // 删除未使用的参数
        public static async ValueTask OnClosed(IAppSession session, CloseEventArgs args)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            Forward.Remove(session.RemoteEndPoint);
            AuthenticateHandler.Remove(session.SessionID);
            await Task.CompletedTask;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="buffer">流数据</param>
        /// <returns></returns>
        public static async ValueTask SendAsync(this string sessionID, byte[] buffer)
        {
            await SessionContainer.GetSessionByID(sessionID).SendAsync(buffer);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="messageBody">消息体</param>
        /// <returns></returns>
        public static async ValueTask SendAsync(this string sessionID, IJTTMessageBody messageBody)
        {
            await SessionContainer.GetSessionByID(sessionID).SendAsync(messageBody);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="messageBody">消息体</param>
        /// <returns></returns>
        public static async ValueTask SendAsync(this IAppSession session, IJTTMessageBody messageBody)
        {
            await session.SendAsync(new JTT809PackageInfo
            {
                MessageHeader = Protocol.JTT809Handler.GetMessageHeader(0, Config.JTT809VersionFlag),
                MessageBody = messageBody
            });
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="packageInfo">消息包</param>
        /// <returns></returns>
        public static async ValueTask SendAsync(this string sessionID, IJTTPackageInfo packageInfo)
        {
            await SessionContainer.GetSessionByID(sessionID).SendAsync(packageInfo);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="session"></param>
        /// <param name="packageInfo">消息包</param>
        /// <returns></returns>
        public static async ValueTask SendAsync(this IAppSession session, IJTTPackageInfo packageInfo)
        {
            await session.SendAsync(Protocol.Encoder, packageInfo);
        }
    }
}
