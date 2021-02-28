using SuperSocket.JTT.JTT1078.Flag;
using SuperSocket.JTT.JTTBase.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 查询音视频资源目录请求消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.DOWN_SEARCH_MSG"/></para>
    /// <para>链路类型：从链路</para>
    /// <para>消息方向：上级平台向下级平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.DOWN_REALVIDEO_FILELIST_REQ"/></para>
    /// <para>描述：政府视频监管平台向企业视频监控平台，或上级政府平台向下级政府平台</para>
    /// <para>发起查询音视频资源目录请求消息。</para>
    /// <para>共128字节</para>
    /// <para>JTT1078-2016表49</para>
    /// </remarks>
    public class SearchFilelistRequestBody : IJTTMessageBody
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        /// <remarks>
        /// <para>1字节</para>
        /// <para>0表示所有通道</para>
        /// <para>按照JTT1076-2016中的表2</para>
        /// </remarks>
        public byte ChannelID { get; set; }

        /// <summary>
        /// 起始时间
        /// </summary>
        /// <remarks>
        /// <para>8字节</para>
        /// <para>YY-MM-DD-HH-MM-SS</para>
        /// <para>全0表示无起始时间条件</para>
        /// </remarks>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 终止时间
        /// </summary>
        /// <remarks>
        /// <para>8字节</para>
        /// <para>YY-MM-DD-HH-MM-SS</para>
        /// <para>全0表示无终止时间条件</para>
        /// </remarks>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        /// <remarks>
        /// <para>8字节</para>
        /// <para>bit0-31按照JTT808-2011表18报警标志位定义</para>
        /// <para>bit32-63按照JTT1078-2016表9</para>
        /// <para>全0表示无报警类型条件</para>
        /// </remarks>
        public BitArray AlarmType { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        /// <remarks>标志位定义</remarks>
        public VideoAlarmFlag AlarmType_Flag { get; set; }

        /// <summary>
        /// 音视频类型
        /// <see cref="Const.AvitemType"/>
        /// </summary>
        /// <remarks>1字节</remarks>
        public byte AvitemType { get; set; }

        /// <summary>
        /// 音视频类型
        /// </summary>
        /// <remarks>映射值</remarks>
        public string AvitemType_Mapping { get; set; }

        /// <summary>
        /// 码流类型
        /// <see cref="Const.StreamType"/>
        /// </summary>
        /// <remarks>1字节</remarks>
        public byte StreamType { get; set; }

        /// <summary>
        /// 码流类型
        /// </summary>
        /// <remarks>映射值</remarks>
        public string StreamType_Mapping { get; set; }

        /// <summary>
        /// 存储器类型
        /// <see cref="Const.MemType"/>
        /// </summary>
        /// <remarks>1字节</remarks>
        public byte MemType { get; set; }

        /// <summary>
        /// 存储器类型
        /// </summary>
        /// <remarks>映射值</remarks>
        public string MemType_Mapping { get; set; }

        /// <summary>
        /// 时效口令
        /// </summary>
        /// <remarks>64字节</remarks>
        public byte[] AuthorizeCode { get; set; }

        /// <summary>
        /// 车辆进入跨域地区后5min之内的任一位置
        /// </summary>
        /// <remarks>
        /// <para>36字节</para>
        /// <para>仅跨域访问请求时使用此字段</para>
        /// <para>按照JTT809-2011中协议4.5.8.1</para>
        /// </remarks>
        public byte[] GnssData { get; set; }
    }
}
