using SuperSocket.JTT.Base.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DebugClient.MessageBody
{
    /// <summary>
    /// 转发异常应答消息数据体
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
