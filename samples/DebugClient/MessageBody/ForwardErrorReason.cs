using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugClient.MessageBody
{
    /// <summary>
    /// 转发异常消息原因
    /// </summary>
    public static class ForwardErrorReason
    {
        public const byte 目标终端不在线 = 0x00;

        public const byte 未登录 = 0x01;

        public const byte 系统繁忙 = 0x02;
    }
}
