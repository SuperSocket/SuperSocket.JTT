using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.Const
{
    /// <summary>
    /// 视频终端与视频平台间消息对照信表
    /// </summary>
    /// <remarks>JTT1078-2016表A.1</remarks>
    public struct VideoTerminalAndVideoPlatformMessageComparisonInfo
    {
        public const UInt16 查询终端音视频属性 = 0x9003;

        public const UInt16 终端上传音视频属性 = 0x1003;

        public const UInt16 实时音视频传输请求 = 0x9101;

        public const UInt16 终端上传乘客流量 = 0x1005;

        public const UInt16 音视频实时传输控制 = 0x9105;

        /// <summary>
        /// 无对照
        /// </summary>
        public const UInt16 实时音视频流及透传数据传输 = default;

        public const UInt16 实时音视频传输状态通知 = 0x9105;

        public const UInt16 查询资源列表 = 0x9205;

        public const UInt16 终端上传音视频资源列表 = 0x1205;

        public const UInt16 平台下发远程录像回放请求 = 0x9201;

        public const UInt16 平台下发远程录像回放控制 = 0x9202;

        public const UInt16 文件上传指令 = 0x9206;

        public const UInt16 文件上传完成通知 = 0x1206;

        public const UInt16 文件上传控制 = 0x9207;

        public const UInt16 云台旋转 = 0x9301;

        public const UInt16 云台调整焦距控制 = 0x9302;

        public const UInt16 云台调整光圈控制 = 0x9303;

        public const UInt16 云台雨刷控制 = 0x9304;

        public const UInt16 红外补光控制 = 0x9305;

        public const UInt16 云台变倍控制 = 0x9306;

        /// <summary>
        /// 短消息
        /// </summary>
        public const string 平台手工唤醒请求 = "WAKEUPXX";
    }
}
