using SuperSocket.JTT1078.Flag;
using SuperSocket.JTTBase.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 音视频资源目录项
    /// </summary>
    /// <remarks>
    /// <para>共32字节</para>
    /// <para>JTT1078-2016表47</para>
    /// </remarks>
    public class FilelistItem : IJTTMessageBody
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
        /// 
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
    }
}
