using SuperSocket.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT809.MessageBody
{
    /// <summary>
    /// 主链路登录应答消息数据体
    /// </summary>
    public class LoginReplyBody : IJTTMessageBody
    {
        /// <summary>
        /// 验证结果
        /// <see cref="Const.LoginReplyResult"/>
        /// </summary>
        /// <remarks>1字节</remarks>
        public byte Result { get; set; }

        /// <summary>
        /// 验证结果
        /// </summary>
        /// <remarks>映射值</remarks>
        public string Result_Mapping { get; set; }

        /// <summary>
        /// 校验码
        /// </summary>
        /// <remarks>4字节</remarks>
        public UInt32 Verify_Code { get; set; }
    }
}
