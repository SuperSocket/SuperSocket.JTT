using SuperSocket.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 远程录像回放控制消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.DOWN_PLAYBACK_MSG"/></para>
    /// <para>链路类型：从链路</para>
    /// <para>消息方向：政府视频监管平台向企业视频监控平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.DOWN__PLAYBACK_MSG_CONTROL"/></para>
    /// <para>描述：政府视频监管平台下发该命令给企业视频监控平台对回放进行控制。</para>
    /// <para>共10字节</para>
    /// <para>JTT1078-2016表53</para>
    /// </remarks>
    public class PlaybackControlRequestBody : IJTTMessageBody
    {
        /// <summary>
        /// 回放控制类型
        /// <see cref="Const.PlaybackControlType"/>
        /// </summary>
        /// <remarks>1字节</remarks>
        public byte ControlType { get; set; }

        /// <summary>
        /// 回放控制类型
        /// </summary>
        /// <remarks>映射值</remarks>
        public string ControlType_Mapping { get; set; }

        /// <summary>
        /// 快进或快退倍数
        /// <see cref="Const.PlaybackFastTime"/>
        /// </summary>
        /// <remarks>1字节</remarks>
        public byte FastTime { get; set; }

        /// <summary>
        /// 快进或快退倍数
        /// </summary>
        /// <remarks>映射值</remarks>
        public string FastTime_Mapping { get; set; }

        /// <summary>
        /// 拖动位置时间
        /// </summary>
        /// <remarks>
        /// <para>8字节</para>
        /// <para>UTC时间</para>
        /// <para><see cref="ControlType"/>为 <see cref="Const.PlaybackControlType.拖动回放"/>时, 此字段内容有效</para>
        /// </remarks>
        public DateTime DateTime { get; set; }
    }
}
