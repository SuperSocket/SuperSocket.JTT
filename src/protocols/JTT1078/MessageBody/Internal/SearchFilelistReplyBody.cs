using SuperSocket.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 查询音视频资源目录应答消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.UP_SEARCH_MSG"/></para>
    /// <para>链路类型：主链路</para>
    /// <para>消息方向：下级平台向上级平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.UP_REALVIDEO_FILELIST_REQ_ACK"/></para>
    /// <para>描述：企业视频监控平台应答政府视频监管平台</para>
    /// <para>或下级政府平台向上级政府平台</para>
    /// <para>应答音视频资源目录消息。</para>
    /// <para>共37字节</para>
    /// <para>JTT1078-2016表50</para>
    /// </remarks>
    public class SearchFilelistReplyBody : IJTTMessageBody
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
