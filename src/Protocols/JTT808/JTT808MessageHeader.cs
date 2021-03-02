using SuperSocket.JTT.JTT808.Const;
using SuperSocket.JTT.JTT808.Internal;
using SuperSocket.JTT.JTTBase.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT808
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>JTT808-2019表2</remarks>
    public class JTT808MessageHeader : IJTTMessageHeader
    {
        /// <summary>
        /// 消息ID
        /// </summary>
        /// <remarks>2字节</remarks>
        public UInt16 MsgID { get; set; }

        /// <summary>
        /// 消息体属性
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// </remarks>
        public UInt16 MsgBodyProperty { get; set; }

        /// <summary>
        /// 消息体属性
        /// </summary>
        public MsgBodyProperty MsgBodyPropertyInfo { get; set; }

        /// <summary>
        /// 协议版本号
        /// </summary>
        /// <remarks>
        /// <para>1字节</para>
        /// <para>每次关键修订递增，初始版本为1</para>
        /// </remarks>
        public byte Version { get; set; }

        /// <summary>
        /// 终端手机号码
        /// </summary>
        /// <remarks>
        /// <para>10字节</para>
        /// <para>根据安装后终端自身的手机号转换。</para>
        /// <para>手机号不足位的，则在前补充数字0</para>
        /// </remarks>
        public string Tel { get; set; }

        /// <summary>
        /// 消息流水号
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// <para>按发送顺序从0开始循环累加。</para>
        /// </remarks>
        public UInt16 Msg_SN { get; set; }

        /// <summary>
        /// 消息包封装项
        /// </summary>
        /// <remarks>
        /// <para>4字节</para>
        /// <para>如果消息体属性中相关标识位确定消息分包处理，则该项有内容，否则无该项</para>
        /// </remarks>
        public SubPackage SubPackage { get; set; }
    }
}
