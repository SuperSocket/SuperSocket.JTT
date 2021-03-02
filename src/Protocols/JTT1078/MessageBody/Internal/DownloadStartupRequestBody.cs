using SuperSocket.JTT.JTT1078.Flag;
using SuperSocket.JTT.JTTBase.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 远程录像下载请求消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.DOWN_DOWNLOAD_MSG"/></para>
    /// <para>链路类型：从链路</para>
    /// <para>消息方向：政府视频监管平台向企业视频监控平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.DOWN_DOWNLOAD_MSG_STARTUP"/></para>
    /// <para>描述：政府视频监管平台下发该命令给企业视频监控平台下载车辆的录像音视频。</para>
    /// <para>共132字节</para>
    /// <para>JTT1078-2016表55</para>
    /// </remarks>
    public class DownloadStartupRequestBody : IJTTMessageBody
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
        /// 
        /// </summary>
        /// <remarks>
        /// <para>8字节</para>
        /// <para>UTC时间</para>
        /// </remarks>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// <para>8字节</para>
        /// <para>UTC时间</para>
        /// </remarks>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        /// <remarks>
        /// <para>8字节</para>
        /// <para>bit0-31按照JTT808-2011表18报警标志位定义</para>
        /// <para>bit32-63按照JTT1078-2016表10</para>
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
        /// 文件大小
        /// </summary>
        /// <remarks>
        /// <para>4字节</para>
        /// <para>单位字节（BYTE）</para>
        /// </remarks>
        public UInt32 FileSize { get; set; }

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
