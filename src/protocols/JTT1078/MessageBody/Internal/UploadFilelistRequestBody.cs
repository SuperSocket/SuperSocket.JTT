using SuperSocket.JTT.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 主动上传音视频资源目录信息消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.UP_SEARCH_MSG"/></para>
    /// <para>链路类型：主链路</para>
    /// <para>消息方向：下级平台向上级平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.UP_FILELIST_MSG"/></para>
    /// <para>描述：企业视频监控平台向政府视频监管平台，或下级政府平台向上级政府平台</para>
    /// <para>主动发送带有特殊报警标识的音视频资源目录。</para>
    /// <para>共36字节</para>
    /// <para>JTT1078-2016表46</para>
    /// </remarks>
    public class UploadFilelistRequestBody : IJTTMessageBody
    {
        /// <summary>
        /// 资源目录项总数
        /// </summary>
        /// <remarks>
        /// <para>4字节</para>
        /// </remarks>
        public UInt32 ItemNum { get; set; }

        /// <summary>
        /// 资源目录项列表
        /// </summary>
        public List<FilelistItem> ItemList { get; set; }
    }
}
