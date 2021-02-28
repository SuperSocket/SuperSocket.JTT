using SuperSocket.JTT.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace DebugClient.MessageBody
{
    /// <summary>
    /// 开始转发应答消息数据体
    /// </summary>
    public class ForwardReplyBody : IJTTMessageBody
    {
        /// <summary>
        /// 处理结果
        /// </summary>
        /// <remarks>1字节</remarks>
        public byte Result { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        /// <remarks>映射值</remarks>
        public string Result_Mapping { get; set; }
    }
}
