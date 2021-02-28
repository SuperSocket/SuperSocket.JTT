using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT809.Const
{
    /// <summary>
    /// 登录应答消息处理结果
    /// </summary>
    public static class LoginReplyResult
    {
        public const byte 成功 = 0x00;

        public const byte IP地址不正确 = 0x01;

        public const byte 接入码不正确 = 0x02;

        public const byte 用户没注册 = 0x03;

        public const byte 密码错误 = 0x04;

        /// <summary>
        /// 资源紧张，稍后再连接（已经占用）
        /// </summary>
        public const byte 资源紧张 = 0x05;

        public const byte 其他 = 0xFF;
    }
}
