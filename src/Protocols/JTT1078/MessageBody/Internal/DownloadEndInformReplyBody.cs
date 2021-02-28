using SuperSocket.JTT.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 远程录像下载完成通知应答消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.DOWN_DOWNLOAD_MSG"/></para>
    /// <para>链路类型：从链路</para>
    /// <para>消息方向：政府视频监管平台向企业视频监控平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.UP_DOWNLOAD_MSG_END_INFORM_ACK"/></para>
    /// <para>描述：企业视频监控平台对政府视频监管平台发送的下载车辆音视频请求的应答消息。</para>
    /// <para>共3字节</para>
    /// <para>JTT1078-2016表58</para>
    /// </remarks>
    public class DownloadEndInformReplyBody : IJTTMessageBody
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

        /// <summary>
        /// 对应平台文件上传消息的流水号
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// <para><see cref="Result"/>为 <see cref="Const.ReplyResult.成功"/>时有效</para>
        /// </remarks>
        public UInt16 SessionID { get; set; }
    }
}
