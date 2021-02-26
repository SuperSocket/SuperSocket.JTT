using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTTServer.MessageBody
{
    /// <summary>
    /// 开始转发应答消息返回结果
    /// </summary>
    public static class ForwardReplyResult
    {
        public const byte 成功 = 0x00;

        public const byte 未登录 = 0x01;

        public const byte 其他 = 0xFF;
    }
}
