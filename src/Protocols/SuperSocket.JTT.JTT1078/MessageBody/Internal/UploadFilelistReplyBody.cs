using SuperSocket.JTT.Base.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 主动上传音视频资源目录请求应答消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.DOWN_SEARCH_MSG"/></para>
    /// <para>链路类型：从链路</para>
    /// <para>消息方向：上级平台向下级平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.DOWN_FILELIST_MSG_ACK"/></para>
    /// <para>描述：政府视频监管平台应答企业视频监控平台</para>
    /// <para>发送的主动上传音视频资源目录请求消息。</para>
    /// <para>共2字节</para>
    /// <para>JTT1078-2016表48</para>
    /// </remarks>
    public class UploadFilelistReplyBody : IJTTMessageBody
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
        /// 资源目录总数
        /// </summary>
        /// <remarks>
        /// <para>1字节</para>
        /// </remarks>
        public byte ItemNumber { get; set; }
    }
}
