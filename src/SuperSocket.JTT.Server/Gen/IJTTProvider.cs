using Microsoft.Extensions.Hosting;
using SuperSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Server.Gen
{
    /// <summary>
    /// JTT服务器构造器
    /// </summary>
    public interface IJTTProvider
    {
        /// <summary>
        /// 获取JTT服务器
        /// </summary>
        /// <returns></returns>
        ISuperSocketHostBuilder GetJTTServerHostBuilder();

        /// <summary>
        /// 获取JTT服务器
        /// </summary>
        /// <returns></returns>
        IHost GetJTTServer();
    }
}
