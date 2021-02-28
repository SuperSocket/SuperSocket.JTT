using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.Const
{
    /// <summary>
    /// 应答消息应答结果
    /// </summary>
    public struct ReplyResult
    {
        public const byte 成功 = 0x00;

        public const byte 失败 = 0x01;

        public const byte 不支持 = 0x02;

        public const byte 会话结束 = 0x03;

        public const byte 时效口令错误 = 0x04;

        public const byte 不满足跨域条件 = 0x05;
    }
}
