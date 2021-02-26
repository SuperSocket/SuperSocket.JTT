using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.Const
{
    /// <summary>
    /// 子业务类型
    /// </summary>
    /// <remarks>JTT1078-2016表37</remarks>
    public struct SubDataType
    {
        /// <summary>
        /// 时效口令上报消息
        /// </summary>
        /// <remarks>
        /// <para>0x1701</para>
        /// <para>主链路时效口令业务类消息 <see cref="DataType.UP_AUTHORIZE_MSG"/></para>
        /// </remarks>
        public const UInt16 UP_AUTHORIZE_MSG_STARTUP = 0x1701;

        /// <summary>
        /// 时效口令请求消息
        /// </summary>
        /// <remarks>
        /// <para>0x1702</para>
        /// <para>主链路时效口令业务类消息 <see cref="DataType.UP_AUTHORIZE_MSG"/></para>
        /// </remarks>
        public const UInt16 UP_AUTHORIZE_MSG_STARTUP_REQ = 0x1702;

        /// <summary>
        /// 时效口令请求应答消息
        /// </summary>
        /// <remarks>
        /// <para>0x9702</para>
        /// <para>从链路时效口令业务类消息 <see cref="DataType.DOWN_BASE_DATA_MSG"/></para>
        /// <para>（上面的业务数据类型是文档中所指定的类型，可能是错的，正确的应该是 <see cref="DataType.DOWN_AUTHORIZE_MSG"/>）</para>
        /// </remarks>
        public const UInt16 DOWN_AUTHORIZE_MSG_STARTUP_REQ_ACK = 0x9702;

        /// <summary>
        /// 实时音视频请求应答消息
        /// </summary>
        /// <remarks>
        /// <para>0x1801</para>
        /// <para>主链路实时音视频交互消息 <see cref="DataType.UP_REALVIDEO_MSG"/></para>
        /// </remarks>
        public const UInt16 UP_REALVIDEO_MSG_STARTUP_ACK = 0x1801;

        /// <summary>
        /// 主动请求停止实时音视频传输应答消息
        /// </summary>
        /// <remarks>
        /// <para>0x1802</para>
        /// <para>主链路实时音视频交互消息 <see cref="DataType.UP_REALVIDEO_MSG"/></para>
        /// </remarks>
        public const UInt16 UP_REALVIDEO_MSG_END_ACK = 0x1802;

        /// <summary>
        /// 实时音视频请求消息
        /// </summary>
        /// <remarks>
        /// <para>0x9801</para>
        /// <para>从链路实时音视频交互消息 <see cref="DataType.DOWN_REALVIDEO_MSG"/></para>
        /// </remarks>
        public const UInt16 DOWN_REALVIDEO_MSG_STARTUP = 0x9801;

        /// <summary>
        /// 主动请求停止实时音视频传输消息
        /// </summary>
        /// <remarks>
        /// <para>0x9802</para>
        /// <para>从链路实时音视频交互消息 <see cref="DataType.DOWN_REALVIDEO_MSG"/></para>
        /// </remarks>
        public const UInt16 DOWN_REALVIDEO_MSG_END = 0x9802;

        /// <summary>
        /// 主动上传音视频资源目录信息消息
        /// </summary>
        /// <remarks>
        /// <para>0x1901</para>
        /// <para>主链路远程录像检索交互消息 <see cref="DataType.UP_SEARCH_MSG"/></para>
        /// </remarks>
        public const UInt16 UP_FILELIST_MSG = 0x1901;

        /// <summary>
        /// 查询音视频资源目录应答消息
        /// </summary>
        /// <remarks>
        /// <para>0x1902</para>
        /// <para>主链路远程录像检索交互消息 <see cref="DataType.UP_SEARCH_MSG"/></para>
        /// </remarks>
        public const UInt16 UP_REALVIDEO_FILELIST_REQ_ACK = 0x1902;

        /// <summary>
        /// 主动上传音视频资源目录信息应答消息
        /// </summary>
        /// <remarks>
        /// <para>0x9901</para>
        /// <para>从链路远程录像检索交互消息 <see cref="DataType.DOWN_SEARCH_MSG"/></para>
        /// </remarks>
        public const UInt16 DOWN_FILELIST_MSG_ACK = 0x9901;

        /// <summary>
        /// 查询音视频资源目录请求消息
        /// </summary>
        /// <remarks>
        /// <para>0x9902</para>
        /// <para>从链路远程录像检索交互消息 <see cref="DataType.DOWN_SEARCH_MSG"/></para>
        /// </remarks>
        public const UInt16 DOWN_REALVIDEO_FILELIST_REQ = 0x9902;

        /// <summary>
        /// 远程录像回放请求应答消息
        /// </summary>
        /// <remarks>
        /// <para>0x1A01</para>
        /// <para>主链路远程录像回放交互消息 <see cref="DataType.UP_PLAYBACK_MSG"/></para>
        /// </remarks>
        public const UInt16 UP_PLAYBACK_MSG_STARTUP_ACK = 0x1A01;

        /// <summary>
        /// 远程录像回放控制应答消息
        /// </summary>
        /// <remarks>
        /// <para>0x1A02</para>
        /// <para>主链路远程录像回放交互消息 <see cref="DataType.UP_PLAYBACK_MSG"/></para>
        /// </remarks>
        public const UInt16 UP__PLAYBACK_MSG_CONTROL_ACK = 0x1A02;

        /// <summary>
        /// 远程录像回放请求消息
        /// </summary>
        /// <remarks>
        /// <para>0x9A01</para>
        /// <para>从链路远程录像回放交互消息 <see cref="DataType.DOWN_PLAYBACK_MSG"/></para>
        /// </remarks>
        public const UInt16 DOWN_PLAYBACK_MSG_STARTUP = 0x9A01;

        /// <summary>
        /// 远程录像回放控制消息
        /// </summary>
        /// <remarks>
        /// <para>0x9A02</para>
        /// <para>从链路远程录像回放交互消息 <see cref="DataType.DOWN_PLAYBACK_MSG"/></para>
        /// </remarks>
        public const UInt16 DOWN__PLAYBACK_MSG_CONTROL = 0x9A02;

        /// <summary>
        /// 远程录像下载请求应答消息
        /// </summary>
        /// <remarks>
        /// <para>0x1B01</para>
        /// <para>主链路远程录像下载交互消息 <see cref="DataType.UP_DOWNLOAD_MSG"/></para>
        /// </remarks>
        public const UInt16 UP_DOWNLOAD_MSG_STARTUP_ACK = 0x1B01;

        /// <summary>
        /// 远程录像下载完成通知消息
        /// </summary>
        /// <remarks>
        /// <para>0x1B02</para>
        /// <para>主链路远程录像下载交互消息 <see cref="DataType.UP_DOWNLOAD_MSG"/></para>
        /// </remarks>
        public const UInt16 UP_DOWNLOAD_MSG_END_INFORM = 0x1B02;

        /// <summary>
        /// 远程录像下载控制应答消息
        /// </summary>
        /// <remarks>
        /// <para>0x1B03</para>
        /// <para>主链路远程录像下载交互消息 <see cref="DataType.UP_DOWNLOAD_MSG"/></para>
        /// </remarks>
        public const UInt16 UP_DOWNLOAD_MSG_CONTROL_ACK = 0x1B03;

        /// <summary>
        /// 远程录像下载请求消息
        /// </summary>
        /// <remarks>
        /// <para>0x9B01</para>
        /// <para>从链路远程录像下载交互消息 <see cref="DataType.DOWN_DOWNLOAD_MSG"/></para>
        /// </remarks>
        public const UInt16 DOWN_DOWNLOAD_MSG_STARTUP = 0x9B01;

        /// <summary>
        /// 远程录像下载完成通知应答消息
        /// </summary>
        /// <remarks>
        /// <para>0x9B02</para>
        /// <para>从链路远程录像下载交互消息 <see cref="DataType.DOWN_DOWNLOAD_MSG"/></para>
        /// </remarks>
        public const UInt16 UP_DOWNLOAD_MSG_END_INFORM_ACK = 0x9B02;

        /// <summary>
        /// 远程录像下载控制消息
        /// </summary>
        /// <remarks>
        /// <para>0x9B03</para>
        /// <para>从链路远程录像下载交互消息 <see cref="DataType.DOWN_DOWNLOAD_MSG"/></para>
        /// </remarks>
        public const UInt16 DOWN_DOWNLOAD_MSG_CONTROL = 0x9B03;
    }
}
