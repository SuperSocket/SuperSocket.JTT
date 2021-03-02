using SuperSocket.JTT.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT809.MessageBody
{
    /// <summary>
    /// 主链路登录请求消息数据体
    /// </summary>
    public class LoginRequestBody : IJTTMessageBody
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        /// <remarks>4字节</remarks>
        public UInt32 UserID { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        /// <remarks>8字节</remarks>
        public string Password { get; set; }

        /// <summary>
        /// 下级平台接入码
        /// </summary>
        /// <remarks>
        /// <para>32字节</para>
        /// <para>上级平台给下级平台分配的唯一标识号</para>
        /// </remarks>
        public UInt32 Msg_GnsscenterID { get; set; }

        /// <summary>
        /// 下级平台提供对应的从链路服务端IP地址
        /// </summary>
        /// <remarks>32字节</remarks>
        public string Down_link_IP { get; set; }

        /// <summary>
        /// 下级平台提供对应的从链路服务端口号
        /// </summary>
        /// <remarks>2字节</remarks>
        public UInt16 Down_link_Port { get; set; }
    }
}
