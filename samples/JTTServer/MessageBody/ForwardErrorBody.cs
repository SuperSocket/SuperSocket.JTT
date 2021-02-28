using SuperSocket.JTT.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace JTTServer.MessageBody
{
    /// <summary>
    /// 转发异常消息数据体
    /// </summary>
    public class ForwardErrorBody : IJTTMessageBody
    {
        /// <summary>
        /// 原因
        /// </summary>
        /// <remarks>1字节</remarks>
        public byte Reason { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        /// <remarks>映射值</remarks>
        public string Reason_Mapping { get; set; }
    }
}
