using SuperSocket.JTTBase.Interface;
using System;

namespace JTTCustomServer.Model
{
    /// <summary>
    /// 自定义消息头
    /// </summary>
    public class JTTCustomMessageHeader : IJTTMessageHeader
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        /// <remarks>2字节</remarks>
        public UInt16 Msg_ID { get; set; }

        /// <summary>
        /// 消息体属性
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// <para>消息包大小</para>
        /// </remarks>
        public UInt16 Msg_Length { get; set; }

        /// <summary>
        /// 终端标识
        /// </summary>
        /// <remarks>
        /// <para>6字节</para>
        /// <para>终端标识第一字节为10，后5字节为智能服务终端ID</para>
        /// </remarks>
        public string Endpoint_ID { get; set; }

        /// <summary>
        /// 消息体流水号
        /// </summary>
        /// <remarks>
        /// <remarks>
        /// <para>2字节</para>
        /// <para>为发送信息的序列号，用于接收方检测是否有信息的丢失。</para>
        /// </remarks>
        public UInt16 Msg_SN { get; set; }
    }
}
