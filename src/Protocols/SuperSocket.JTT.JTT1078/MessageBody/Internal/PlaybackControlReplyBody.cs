using SuperSocket.JTT.Base.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 远程录像回放控制应答消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.UP_PLAYBACK_MSG"/></para>
    /// <para>链路类型：主链路</para>
    /// <para>消息方向：企业视频监控平台往政府视频监管平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.UP__PLAYBACK_MSG_CONTROL_ACK"/></para>
    /// <para>描述：企业视频监控平台应答政府视频监管平台下发的回放控制消息。</para>
    /// <para>共1字节</para>
    /// <para>JTT1078-2016表54</para>
    /// </remarks>
    public class PlaybackControlReplyBody : IJTTMessageBody
    {
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
