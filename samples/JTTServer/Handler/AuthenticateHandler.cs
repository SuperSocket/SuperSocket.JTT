using JTTServer.Config;
using Library.Container;
using Microsoft.Extensions.DependencyInjection;
using SuperSocket;
using SuperSocket.JTT809.Const;
using SuperSocket.JTT809.MessageBody;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace JTTServer
{
    public static class AuthenticateHandler
    {
        static readonly SystemConfig Config = AutofacHelper.GetService<IServiceProvider>()
            .GetService<SystemConfig>();

        /// <summary>
        /// 已验证的会话
        /// </summary>
        static readonly ConcurrentDictionary<string, (UInt32 UserID, UInt32 Msg_GnsscenterID)> VerifiedSession = new();

        /// <summary>
        /// 验证身份
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="requestBody">消息体</param>
        /// <returns></returns>
        public static async Task Authenticate(IAppSession session, LoginRequestBody requestBody)
        {
            if (!Config.GnsscenterID.Contains(requestBody.Msg_GnsscenterID))
            {
                await session.SendAsync(new LoginReplyBody
                {
                    Result = LoginReplyResult.接入码不正确,
                    Verify_Code = 0
                });
            }
            else if (requestBody.Password != Config.Password)
            {
                await session.SendAsync(new LoginReplyBody
                {
                    Result = LoginReplyResult.密码错误,
                    Verify_Code = 1
                });
            }
            else
            {
                Add(session.SessionID, requestBody.UserID, requestBody.Msg_GnsscenterID);

                await session.SendAsync(new LoginReplyBody
                {
                    Result = LoginReplyResult.成功,
                    Verify_Code = 0
                });
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sessionID">会话ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="gnsscenterID">下级平台接入码</param>
        public static void Add(string sessionID, UInt32 userID, UInt32 gnsscenterID)
        {
            VerifiedSession.AddOrUpdate(
               sessionID,
               (userID, gnsscenterID),
               (key, oldValue) => oldValue = (userID, gnsscenterID));

            LoggerHelper.Log(
                Microsoft.Extensions.Logging.LogLevel.Information,
                Library.Models.LogType.系统跟踪,
                $"登录, " +
                $"\r\n\tSessionID: {sessionID}, " +
                $"\r\n\tUserID: {userID}, " +
                $"\r\n\tGnsscenterID: {gnsscenterID}.");
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sessionID">会话ID</param>
        public static void Remove(string sessionID)
        {
            if (!VerifiedSession.TryRemove(sessionID, out (UInt32 userID, UInt32 gnsscenterID) value))
                return;

            LoggerHelper.Log(
                Microsoft.Extensions.Logging.LogLevel.Information,
                Library.Models.LogType.系统跟踪,
                $"注销, " +
                $"\r\n\tSessionID: {sessionID}, " +
                $"\r\n\tUserID: {value.userID}, " +
                $"\r\n\tGnsscenterID: {value.gnsscenterID}.");
        }

        /// <summary>
        /// 会话是否已验证
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="userID">用户ID</param>
        /// <param name="gnsscenterID">接入码</param>
        /// <returns></returns>
        public static bool IsAuthenticated(string sessionID, UInt32? userID = null, UInt32? gnsscenterID = null)
        {
            return VerifiedSession.ContainsKey(sessionID) && (userID == null || VerifiedSession[sessionID].UserID == userID) && (gnsscenterID == null || VerifiedSession[sessionID].Msg_GnsscenterID == gnsscenterID);
        }
    }
}
