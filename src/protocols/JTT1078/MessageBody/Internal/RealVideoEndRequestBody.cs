using SuperSocket.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 主动请求停止实时音视频传输消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.DOWN_REALVIDEO_MSG"/></para>
    /// <para>链路类型：从链路</para>
    /// <para>消息方向：政府视频监管平台向企业视频监控平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.DOWN_REALVIDEO_MSG_END"/></para>
    /// <para>描述：政府视频监管平台向下发该命令给企业视频监控平台，</para>
    /// <para>主动请求停止车辆的实时音视频传输。</para>
    /// <para>共2字节</para>
    /// <remarks>JTT1078-2016表44</remarks>
    /// </remarks>
    public class RealVideoEndRequestBody : IJTTMessageBody
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        /// <remarks>
        /// <para>1字节</para>
        /// <para>0 表示所有通道</para>
        /// <para>按照JTT1076-2016中的表2</para>
        /// </remarks>
        public byte ChannelID { get; set; }

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
    }
}
