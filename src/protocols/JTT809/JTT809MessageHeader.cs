using SuperSocket.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT809
{
    /// <summary>
    /// 
    /// </summary>
    public class JTT809MessageHeader : IJTTMessageHeader
    {
        /// <summary>
        /// 数据长度
        /// </summary>
        /// <remarks>
        /// <para>4字节</para>
        /// <para>包括头标识、数据头、数据体和尾标识</para>
        /// </remarks>
        public UInt32 Msg_Length { get; set; }

        /// <summary>
        /// 报文序列号
        /// </summary>
        /// <remarks>
        /// <remarks>
        /// <para>4字节</para>
        /// <para>为发送信息的序列号，用于接收方检测是否有信息的丢失。</para>
        /// <para>上级平台和下级平台按自己发送数据包的个数计数，互不影响。</para>
        /// <para>程序开始运行时等于零，发送第一帧数据时开始计数，到最大数后自动归零</para>
        /// </remarks>
        public UInt32 Msg_SN { get; set; }

        /// <summary>
        /// 协议号/业务数据类型
        /// </summary>
        /// <remarks>2字节</remarks>
        public UInt16 Msg_ID { get; set; }

        /// <summary>
        /// 下级平台接入码
        /// </summary>
        /// <remarks>
        /// <para>32字节</para>
        /// <para>上级平台给下级平台分配的唯一标识号</para>
        /// </remarks>
        public UInt32 Msg_GnsscenterID { get; set; }

        /// <summary>
        /// 协议版本号标识
        /// </summary>
        /// <remarks>
        /// <para>3字节</para>
        /// <para>上下级平台之间采用的标准协议版本编号</para>
        /// <para>0x01 0x02 0x0F 表示的版本号是 V1.2.15，依此类推</para>
        /// </remarks>
        public byte[] Version_Flag { get; set; }

        /// <summary>
        /// 报文加密标识位
        /// </summary>
        /// <remarks>
        /// <para>1字节</para>
        /// <para>用来区分报文是否进行加密，</para>
        /// <para>如果标识为 1，则说明对后续相应业务的数据体采用 ENCRYPT_KEY 对应的密钥进行加密处理。</para>
        /// <para>如果标识为 0，则说明不进行加密处理</para>
        /// </remarks>
        public bool Encrypt_Flag { get; set; }

        /// <summary>
        /// 数据加密的密钥
        /// </summary>
        /// <remarks>4字节</remarks>
        public UInt32 Encrtpt_Key { get; set; }

        /// <summary>
        /// 发送消息时的系统UTC时间
        /// </summary>
        /// <remarks>8字节</remarks>
        public UInt64 Time { get; set; }
    }
}
