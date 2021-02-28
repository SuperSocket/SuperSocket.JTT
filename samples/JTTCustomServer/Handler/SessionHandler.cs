using SuperSocket;
using System.Threading.Tasks;

namespace JTTCustomServer.Handler
{
    /// <summary>
    /// 会话处理类
    /// </summary>
    public static class SessionHandler
    {
        /// <summary>
        /// 已建立连接
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static async ValueTask OnConnected(IAppSession session)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 连接已关闭
        /// </summary>
        /// <param name="session"></param>
        /// <param name="args"></param>
        /// <returns></returns>
#pragma warning disable IDE0060 // 删除未使用的参数
        public static async ValueTask OnClosed(IAppSession session, SuperSocket.Channel.CloseEventArgs args)
#pragma warning restore IDE0060 // 删除未使用的参数
        {
            await Task.CompletedTask;
        }
    }
}
