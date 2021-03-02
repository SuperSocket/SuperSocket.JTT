using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.Const
{
    /// <summary>
    /// 回放控制快进或快退倍数
    /// </summary>
    public struct PlaybackFastTime
    {
        public const byte 无效 = 0x00;

        public const byte 一倍 = 0x01;

        public const byte 二倍 = 0x02;

        public const byte 四倍 = 0x03;

        public const byte 八倍 = 0x04;

        public const byte 十六倍 = 0x05;
    }
}
