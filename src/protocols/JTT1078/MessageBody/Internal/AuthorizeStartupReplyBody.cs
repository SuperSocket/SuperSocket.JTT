using SuperSocket.JTT.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 时效口令请求应答消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.DOWN_AUTHORIZE_MSG"/></para>
    /// <para>链路类型：从链路</para>
    /// <para>消息方向：上级政府视频监管平台向跨域地政府视频监管平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.DOWN_AUTHORIZE_MSG_STARTUP_REQ_ACK"/></para>
    /// <para>描述：上级政府视频监管平台应答跨域地政府视频监管平台发送的时效口令请求消息，</para>
    /// <para>上级政府视频监管平台根据请求车辆5min之内的地理位置确定应答的内容。</para>
    /// <para>共128字节</para>
    /// <para>JTT1078-2016表41</para>
    /// </remarks>
    public class AuthorizeStartupReplyBody : IJTTMessageBody
    {
        /// <summary>
        /// 归属地区政府平台使用的时效口令
        /// </summary>
        /// <remarks>
        /// <para>64字节</para>
        /// </remarks>
        public byte[] AuthorizeCode1 { get; set; }

        /// <summary>
        /// 跨域地区政府平台使用的时效口令
        /// </summary>
        /// <remarks>
        /// <para>64字节</para>
        /// </remarks>
        public byte[] AuthorizeCode2 { get; set; }
    }
}
