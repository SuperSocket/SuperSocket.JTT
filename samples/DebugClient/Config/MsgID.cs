using System;
using System.Collections.Generic;
using System.Text;

namespace DebugClient
{
    /// <summary>
    /// 协议号/业务数据类型
    /// </summary>
    public static class MsgID
    {
        public const UInt16 LoginRequest = 0x1001;

        public const UInt16 LoginReply = 0x1002;

        public const UInt16 ForwardRequest = 0x6001;

        public const UInt16 ForwardReply = 0x6002;

        public const UInt16 CancelForwardRequest = 0x6003;

        public const UInt16 CancelForwardReply = 0x6004;

        public const UInt16 ForwardErrorBody = 0x6005;

        public const UInt16 GetForwardEndpointRequest = 0x6006;

        public const UInt16 GetForwardEndpointReply = 0x6007;

        public const UInt16 TestForwardRequest = 0x6008;

        public const UInt16 TestForwardReply = 0x6009;
    }
}
