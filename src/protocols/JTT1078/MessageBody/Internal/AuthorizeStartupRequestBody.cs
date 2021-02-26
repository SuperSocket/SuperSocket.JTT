using SuperSocket.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 时效口令请求消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.UP_AUTHORIZE_MSG"/></para>
    /// <para>链路类型：主链路</para>
    /// <para>消息方向：跨域地政府视频监管平台向上级政府视频监管平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.UP_AUTHORIZE_MSG_STARTUP_REQ"/></para>
    /// <para>描述：跨域地政府视频监管平台向上级政府视频监管平台</para>
    /// <para>获取指定车辆所在企业视频监控平台的当日时效口令。</para>
    /// <para>共28字节</para>
    /// <para>JTT1078-2016表40</para>
    /// </remarks>
    public class AuthorizeStartupRequestBody : IJTTMessageBody
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        /// <remarks>21字节</remarks>
        public string VehicleNo { get; set; }

        /// <summary>
        /// 车牌颜色
        /// </summary>
        /// <remarks>
        /// <para>1字节</para>
        /// <para>按照JTT415-2006中协议5.4.12</para>
        /// </remarks>
        public byte VehicleColor { get; set; }

        /// <summary>
        /// 子业务类型标识
        /// <see cref="Const.SubDataType.UP_AUTHORIZE_MSG_STARTUP_REQ"/>
        /// </summary>
        /// <remarks>2字节</remarks>
        public UInt16 DataType { get; set; }

        /// <summary>
        /// 子业务类型标识
        /// </summary>
        /// <remarks>映射值</remarks>
        public string DataType_Mapping { get; set; }

        /// <summary>
        /// 后续数据长度
        /// </summary>
        /// <remarks>
        /// <para>4字节</para>
        /// <para>值为 0x00000000</para>
        /// </remarks>
        public UInt32 DataLength { get; set; }
    }
}
