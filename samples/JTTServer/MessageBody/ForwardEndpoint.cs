using SuperSocket.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTTServer.MessageBody
{
    /// <summary>
    /// 转发客户端
    /// </summary>
    /// <remarks>
    /// <para>共34字节</para>
    /// </remarks>
    public class ForwardEndpoint : IJTTMessageBody
    {
        /// <summary>
        /// IP地址
        /// </summary>
        /// <remarks>
        /// <para>32字节</para>
        /// </remarks>
        public string IP { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// </remarks>
        public UInt16 Port { get; set; }
    }
}
