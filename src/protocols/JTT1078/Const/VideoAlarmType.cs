using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.Const
{
    /// <summary>
    /// 通过平台上报的视频报警类型编码
    /// </summary>
    /// <remarks>JTT1078-2016表38</remarks>
    public struct VideoAlarmType
    {
        public const UInt16 视频信号丢失报警 = 0x0101;

        public const UInt16 视频信号遮挡报警 = 0x0102;

        public const UInt16 存储单元故障报警 = 0x0103;

        public const UInt16 其他视频设备故障报警 = 0x0104;

        public const UInt16 客车超员报警 = 0x0105;

        public const UInt16 异常驾驶行为报警 = 0x0106;

        public const UInt16 特殊报警录像达到存储阈值报警 = 0x0106;
    }
}
