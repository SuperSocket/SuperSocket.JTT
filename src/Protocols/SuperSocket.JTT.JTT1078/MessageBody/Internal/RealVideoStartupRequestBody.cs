using SuperSocket.JTT.Base.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 实时音视频请求消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.DOWN_REALVIDEO_MSG"/></para>
    /// <para>链路类型：从链路</para>
    /// <para>消息方向：发起方平台向接收方平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.DOWN_REALVIDEO_MSG_STARTUP"/></para>
    /// <para>描述：政府视频监管平台向企业视频监控平台、上级政府平台向下级政府平台</para>
    /// <para>或跨域地区政府平台向归属地区政府平台</para>
    /// <para>下发该命令请求车辆的实时音视频。</para>
    /// <para>共102字节</para>
    /// <para>JTT1078-2016表42</para>
    /// </remarks>
    public class RealVideoStartupRequestBody : IJTTMessageBody
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
        /// 时效口令
        /// </summary>
        /// <remarks>64字节</remarks>
        public byte[] AuthorizeCode { get; set; }

        /// <summary>
        /// 车辆进入跨域地区后5min之内的任一位置
        /// </summary>
        /// <remarks>
        /// <para>36字节</para>
        /// <para>仅跨域访问请求时使用此字段</para>
        /// <para>按照JTT809-2011中协议4.5.8.1</para>
        /// </remarks>
        public byte[] GnssData { get; set; }
    }
}
