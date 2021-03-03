using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.Const
{
    /// <summary>
    /// 回放控制类型
    /// </summary>
    public struct PlaybackControlType
    {
        public const byte 正常回放 = 0x00;

        public const byte 暂停回放 = 0x01;

        public const byte 结束回放 = 0x02;

        public const byte 快进回放 = 0x03;

        public const byte 关键帧快退回放 = 0x04;

        public const byte 拖动回放 = 0x05;

        public const byte 关键帧播放 = 0x06;
    }
}
