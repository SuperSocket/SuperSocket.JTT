using SuperSocket.JTT.Base.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 远程录像回放请求应答消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.UP_PLAYBACK_MSG"/></para>
    /// <para>链路类型：主链路</para>
    /// <para>消息方向：接收方平台向发起方平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.UP_PLAYBACK_MSG_STARTUP_ACK"/></para>
    /// <para>描述：企业视频监控平台应答政府视频监管平台、</para>
    /// <para>下级政府平台应答上级政府平台或归属地区政府平台</para>
    /// <para>应答跨域地区政府平台发送的录像回放请求消息。</para>
    /// <para>共35字节</para>
    /// <para>JTT1078-2016表52</para>
    /// </remarks>
    public class PlaybackStartupReplyBody : IJTTMessageBody
    {
        /// <summary>
        /// 企业视频服务器IP地址
        /// </summary>
        /// <remarks>32字节</remarks>
        public string ServerIP { get; set; }

        /// <summary>
        /// 企业视频服务器端口号
        /// </summary>
        /// <remarks>2字节</remarks>
        public UInt16 ServerPort { get; set; }

        /// <summary>
        /// 应答结果
        /// <see cref="Const.ReplyResult"/>
        /// </summary>
        /// <remarks>1字节</remarks>
        public byte Result { get; set; }

        /// <summary>
        /// 应答结果
        /// </summary>
        /// <remarks>映射值</remarks>
        public string Result_Mapping { get; set; }
    }
}
