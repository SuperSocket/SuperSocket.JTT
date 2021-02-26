using SuperSocket.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 时效口令上报消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.UP_AUTHORIZE_MSG"/></para>
    /// <para>链路类型：主链路</para>
    /// <para>消息方向：下级平台向上级平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.UP_AUTHORIZE_MSG_STARTUP"/></para>
    /// <para>描述：企业视频监控平台向政府视频监管平台</para>
    /// <para>或下级政府视频监管平台向上级政府视频监管平台</para>
    /// <para>主动上报时效口令，</para>
    /// <para>该指令无须应答。</para>
    /// <para>共139字节</para>
    /// <para>JTT1078-2016表39</para>
    /// </remarks>
    public class AuthorizeStartupBody : IJTTMessageBody
    {
        /// <summary>
        /// 企业视频监控平台唯一编码
        /// </summary>
        /// <remarks>
        /// <para>11字节</para>
        /// <para>平台所属企业行政区划代码 + 平台公告编号</para>
        /// </remarks>
        public byte[] PlateformID { get; set; }

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
