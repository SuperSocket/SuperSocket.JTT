using SuperSocket.JTT.JTTBase.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 远程录像下载控制请求消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.DOWN_DOWNLOAD_MSG"/></para>
    /// <para>链路类型：从链路</para>
    /// <para>消息方向：政府视频监管平台向企业视频监控平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.DOWN_DOWNLOAD_MSG_CONTROL"/></para>
    /// <para>描述：政府视频监管平台给企业视频监控平台发送下载控制消息。</para>
    /// <para>共3字节</para>
    /// <para>JTT1078-2016表59</para>
    /// </remarks>
    public class DownloadControlRequestBody : IJTTMessageBody
    {
        /// <summary>
        /// 对应平台文件上传消息的流水号
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// </remarks>
        public UInt16 SessionID { get; set; }

        /// <summary>
        /// 
        /// <see cref="Const.DownloadControlType"/>
        /// </summary>
        /// <remarks>1字节</remarks>
        public byte Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>映射值</remarks>
        public string Type_Mapping { get; set; }
    }
}
