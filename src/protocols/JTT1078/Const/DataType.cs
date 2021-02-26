using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.Const
{
    /// <summary>
    /// 业务类型
    /// </summary>
    /// <remarks>JTT1078-2016表36</remarks>
    public struct DataType
    {
        /// <summary>
        /// 主链路时效口令交互消息
        /// </summary>
        /// <remarks>
        /// <para>0x1700</para>
        /// <para>时效口令业务类</para>
        /// <para>主链路</para>
        /// </remarks>
        public const UInt16 UP_AUTHORIZE_MSG = 0x1700;

        /// <summary>
        /// 从链路时效口令交互消息
        /// </summary>
        /// <remarks>
        /// <para>0x9700</para>
        /// <para>时效口令业务类</para>
        /// <para>从链路</para>
        /// </remarks>
        public const UInt16 DOWN_AUTHORIZE_MSG = 0x9700;

        /// <summary>
        /// 主链路实时音视频交互消息
        /// </summary>
        /// <remarks>
        /// <para>0x1800</para>
        /// <para>实时音视频业务类</para>
        /// <para>主链路</para>
        /// </remarks>
        public const UInt16 UP_REALVIDEO_MSG = 0x1800;

        /// <summary>
        /// 从链路实时音视频交互消息
        /// </summary>
        /// <remarks>
        /// <para>0x9800</para>
        /// <para>实时音视频业务类</para>
        /// <para>从链路</para>
        /// </remarks>
        public const UInt16 DOWN_REALVIDEO_MSG = 0x9800;

        /// <summary>
        /// 主链路远程录像检索交互消息
        /// </summary>
        /// <remarks>
        /// <para>0x1900</para>
        /// <para>远程录像检索</para>
        /// <para>主链路</para>
        /// </remarks>
        public const UInt16 UP_SEARCH_MSG = 0x1900;

        /// <summary>
        /// 从链路远程录像检索交互消息
        /// </summary>
        /// <remarks>
        /// <para>0x9900</para>
        /// <para>远程录像检索</para>
        /// <para>从链路</para>
        /// </remarks>
        public const UInt16 DOWN_SEARCH_MSG = 0x9900;

        /// <summary>
        /// 主链路远程录像回放交互消息
        /// </summary>
        /// <remarks>
        /// <para>0x1A00</para>
        /// <para>远程录像回放</para>
        /// <para>主链路</para>
        /// </remarks>
        public const UInt16 UP_PLAYBACK_MSG = 0x1A00;

        /// <summary>
        /// 从链路远程录像回放交互消息
        /// </summary>
        /// <remarks>
        /// <para>0x9A00</para>
        /// <para>远程录像回放</para>
        /// <para>从链路</para>
        /// </remarks>
        public const UInt16 DOWN_PLAYBACK_MSG = 0x9A00;

        /// <summary>
        /// 主链路远程录像下载交互消息
        /// </summary>
        /// <remarks>
        /// <para>0x1B00</para>
        /// <para>远程录像下载</para>
        /// <para>主链路</para>
        /// </remarks>
        public const UInt16 UP_DOWNLOAD_MSG = 0x1B00;

        /// <summary>
        /// 从链路远程录像下载交互消息
        /// </summary>
        /// <remarks>
        /// <para>0x9B00</para>
        /// <para>远程录像下载</para>
        /// <para>从链路</para>
        /// </remarks>
        public const UInt16 DOWN_DOWNLOAD_MSG = 0x9B00;
    }
}
