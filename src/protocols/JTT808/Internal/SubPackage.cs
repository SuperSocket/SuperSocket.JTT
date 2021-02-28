using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT808.Internal
{
    /// <summary>
    /// 消息包封装项内容
    /// </summary>
    /// <remarks>JTT808-2019表3</remarks>
    public class SubPackage
    {
        /// <summary>
        /// 消息包总数
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// <para>该消息分包后的总包数</para>
        /// </remarks>
        public UInt16 Total { get; set; }

        /// <summary>
        /// 包序号
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// <para>从1开始</para>
        /// </remarks>
        public UInt16 Order { get; set; }
    }
}
