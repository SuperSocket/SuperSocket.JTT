using SuperSocket.JTT.Base.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 远程录像下载控制请求应答消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.UP_DOWNLOAD_MSG"/></para>
    /// <para>链路类型：主链路</para>
    /// <para>消息方向：企业视频监控平台向政府视频监管平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.UP_DOWNLOAD_MSG_CONTROL_ACK"/></para>
    /// <para>描述：企业视频监控平台对政府视频监管平台发送的下载控制请求的应答消息。</para>
    /// <para>共1字节</para>
    /// <para>JTT1078-2016表60</para>
    /// </remarks>
    public class DownloadControlReplyBody : IJTTMessageBody
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
