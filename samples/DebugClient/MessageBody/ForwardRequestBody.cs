using SuperSocket.JTT.Base.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DebugClient.MessageBody
{
    /// <summary>
    /// 开始转发请求消息数据体
    /// </summary>
    public class ForwardRequestBody : IJTTMessageBody
    {
        /// <summary>
        /// 目标IP地址
        /// </summary>
        /// <remarks>
        /// <para>32字节</para>
        /// <para>消息转发的目标服务端</para>
        /// <para>不指定时, 将进行广播</para>
        /// </remarks>
        public string Target_IP { get; set; }

        /// <summary>
        /// 目标端口号
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// <para>消息转发的目标服务端</para>
        /// <para>不指定时, 将进行广播</para>
        /// </remarks>
        public UInt16 Target_Port { get; set; }
    }
}
