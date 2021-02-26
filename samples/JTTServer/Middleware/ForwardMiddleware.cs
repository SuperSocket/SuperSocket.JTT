using JTTServer.MessageBody;
using Library.Extension;
using Library.Log;
using Library.Models;
using Microsoft.Extensions.Logging;
using SuperSocket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace JTTServer
{
    /// <summary>
    /// 转发中间件
    /// </summary>
    public class ForwardMiddleware : MiddlewareBase
    {
        public ForwardMiddleware(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

#pragma warning disable IDE0052 // 删除未读的私有成员
        readonly IServiceProvider ServiceProvider;
#pragma warning restore IDE0052 // 删除未读的私有成员

        /// <summary>
        /// 流数据
        /// </summary>
        /// <remarks>
        /// <para>(会话ID, 终端, 流数据)</para>
        /// <para>会话ID和终端都是流数据来源的信息</para>
        /// </remarks>
        ConcurrentQueue<(string sessinID, EndPoint endPoint, byte[] buffer)> BufferQueue;

        /// <summary>
        /// 终端和对应的会话ID
        /// </summary>
        ConcurrentDictionary<EndPoint, string> ClientWithSessionsID;

        /// <summary>
        /// 会话ID和目标终端
        /// </summary>
        ConcurrentDictionary<string, EndPoint> SessionsIDWithTarget;

        TaskCompletionSource<bool> TCS;

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="server"></param>
        public override void Start(IServer server)
        {
            BufferQueue = new ConcurrentQueue<(string, EndPoint, byte[])>();
            ClientWithSessionsID = new ConcurrentDictionary<EndPoint, string>();
            SessionsIDWithTarget = new ConcurrentDictionary<string, EndPoint>();
            Run();
            LoggerHelper.Log(LogLevel.Debug, LogType.系统跟踪, "转发中间件已启动");
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="server"></param>
        public override void Shutdown(IServer server)
        {
            if (TCS == null)
                TCS = new TaskCompletionSource<bool>();
            TCS?.SetResult(false);
            LoggerHelper.Log(LogLevel.Debug, LogType.系统跟踪, "转发中间件已关闭");
        }

        /// <summary>
        /// 获取终端列表
        /// </summary>
        /// <returns></returns>
        public async Task GetClientList(IAppSession session)
        {
            if (!AuthenticateHandler.IsAuthenticated(session.SessionID))
                await session.SendAsync(new ForwardErrorBody
                {
                    Reason = ForwardErrorReason.未登录
                });
            else
            {
                var endpoints = ClientWithSessionsID.Keys.Select(o => new ForwardEndpoint
                {
                    IP = ((IPEndPoint)o).Address.ToString(),
                    Port = (UInt16)((IPEndPoint)o).Port
                }).ToList();

                await session.SendAsync(new GetForwardEndpointReplyBody
                {
                    Total = (UInt32)endpoints.Count,
                    Endpoints = endpoints
                });
            }
        }

        /// <summary>
        /// 获取终端列表
        /// </summary>
        /// <returns></returns>
        public List<ForwardEndpoint> GetClientList()
        {
            return ClientWithSessionsID.Keys.Select(o => new ForwardEndpoint
            {
                IP = ((IPEndPoint)o).Address.ToString(),
                Port = (UInt16)((IPEndPoint)o).Port
            }).ToList();
        }

        /// <summary>
        /// 新增终端
        /// </summary>
        /// <param name="sessionID">会话ID</param>
        /// <param name="endPoint">终端</param>
        public void Add(string sessionID, EndPoint endPoint)
        {
            ClientWithSessionsID.AddOrUpdate(
               endPoint,
               sessionID,
               (key, oldValue) => oldValue = sessionID);
        }

        /// <summary>
        /// 移除终端
        /// </summary>
        /// <param name="endPoint">终端</param>
        public void Remove(EndPoint endPoint)
        {
            ClientWithSessionsID.TryRemove(endPoint, out _);
        }

        /// <summary>
        /// 新增需要转发的会话
        /// </summary>
        /// <param name="session">会话</param>
        /// <param name="ip">目标IP</param>
        /// <param name="port">目标端口</param>
        public async Task Add(IAppSession session, string ip, ushort port)
        {
            if (!AuthenticateHandler.IsAuthenticated(session.SessionID))
                await session.SendAsync(new ForwardReplyBody
                {
                    Result = ForwardReplyResult.未登录
                });
            else
            {
                try
                {
                    var endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                    if (session.RemoteEndPoint.ToString() == endPoint.ToString())
                    {
                        await session.SendAsync(new ForwardReplyBody
                        {
                            Result = ForwardReplyResult.其他
                        });
                        return;
                    }

                    SessionsIDWithTarget.AddOrUpdate(
                        session.SessionID,
                        endPoint,
                        (key, oldValue) => oldValue = endPoint);

                    await session.SendAsync(new ForwardReplyBody
                    {
                        Result = ForwardReplyResult.成功
                    });
                }
                catch (Exception ex)
                {
                    LoggerHelper.Log(
                        LogLevel.Error,
                        LogType.系统异常,
                        $"新增转发会话时异常, " +
                        $"\r\n\tSessionID: {session.SessionID}, " +
                        $"\r\n\tip: {ip}, " +
                        $"\r\n\tport: {port}.",
                        ex);

                    await session.SendAsync(new ForwardReplyBody
                    {
                        Result = ForwardReplyResult.其他
                    });
                }
            }
        }

        /// <summary>
        /// 移除需要转发的会话
        /// </summary>
        /// <param name="session">会话</param>
        public async Task Remove(IAppSession session)
        {
            if (!AuthenticateHandler.IsAuthenticated(session.SessionID))
                await session.SendAsync(new CancelForwardReplyBody
                {
                    Result = ForwardReplyResult.未登录
                });
            else
            {
                if (!SessionsIDWithTarget.ContainsKey(session.SessionID))
                    await session.SendAsync(new CancelForwardReplyBody
                    {
                        Result = ForwardReplyResult.其他
                    });
                else
                {
                    SessionsIDWithTarget.TryRemove(session.SessionID, out _);

                    await session.SendAsync(new CancelForwardReplyBody
                    {
                        Result = ForwardReplyResult.成功
                    });
                }
            }
        }

        /// <summary>
        /// 新增转发数据
        /// </summary>
        /// <param name="sessionID">会话ID(数据来源)</param>
        /// <param name="endPoint">终端</param>
        /// <param name="buffer">流数据</param>
        public void Add(string sessionID, EndPoint endPoint, byte[] buffer)
        {
            BufferQueue.Enqueue((sessionID, endPoint, buffer));

            //开始推送
            TCS?.SetResult(true);
        }

        /// <summary>
        /// 运行
        /// </summary>
        private async void Run()
        {
            while (true)
            {
                if (TCS != null)
                {
                    if (!await TCS.Task)
                        return;
                    TCS = null;
                }

                if (BufferQueue.IsEmpty)
                {
                    TCS = new TaskCompletionSource<bool>();
                    continue;
                }

                try
                {
                    await Processing();
                }
                catch (Exception ex)
                {
                    LoggerHelper.Log(
                        LogLevel.Error,
                        LogType.系统异常,
                        "处理转发时异常.",
                        ex);
                }
            }
        }

        /// <summary>
        /// 处理转发
        /// </summary>
        private async Task Processing()
        {
            var Queue = BufferQueue;
            var Count = Queue.Count;

            await LoggerHelper.LogAsync(
                LogLevel.Debug,
                LogType.系统跟踪,
                $"转发中间件正在转发, 总量: {Count}.");

            for (int i = 0; i < Count; i++)
            {
                Queue.TryDequeue(out (string sessionID, EndPoint endPoint, byte[] buffer) item);

                try
                {
                    if (SessionsIDWithTarget.ContainsKey(item.sessionID))
                    {
                        if (!ClientWithSessionsID.ContainsKey(SessionsIDWithTarget[item.sessionID]))
                        {
                            await item.sessionID.SendAsync(new ForwardErrorBody { Reason = ForwardErrorReason.目标终端不在线 });
                        }
                        else
                            await ClientWithSessionsID[SessionsIDWithTarget[item.sessionID]].SendAsync(item.buffer);
                    }
                    else
                    {
                        foreach (var session in SessionsIDWithTarget)
                        {
                            if (session.Value.Equals(item.endPoint))
                                await session.Key.SendAsync(item.buffer);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await item.sessionID.SendAsync(new ForwardErrorBody { Reason = ForwardErrorReason.系统繁忙 });

                    await LoggerHelper.LogAsync(
                        LogLevel.Error,
                        LogType.系统异常,
                        $"处理转发时异常, " +
                        $"\r\n\tsessionID: {item.sessionID}, " +
                        $"\r\n\tEndPoint: {item.endPoint}, " +
                        $"\r\n\tBuffer: {item.buffer.To0XString()}.",
                        ex);
                }
            }
        }
    }
}
