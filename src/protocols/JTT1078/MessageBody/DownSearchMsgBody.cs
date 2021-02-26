using SuperSocket.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.MessageBody
{
    /// <summary>
    /// 从链路远程录像检索交互消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.DOWN_SEARCH_MSG"/></para>
    /// <para>共28+字节</para>
    /// <para>JTT1078-2016 10.3章节</para>
    /// </remarks>
    public class DownSearchMsgBody : IJTTMessageBody
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
        /// </summary>
        /// <remarks>2字节</remarks>
        public UInt16 DataType { get; set; }

        /// <summary>
        /// 子业务类型标识
        /// </summary>
        /// <remarks>映射值</remarks>
        public string DataType_Mapping { get; set; }

        /// <summary>
        /// 后续字段数据长度
        /// </summary>
        /// <remarks>4字节</remarks>
        public UInt32 DataLength { get; set; }

        /// <summary>
        /// 子业务数据体
        /// </summary>
        /// <remarks>
        /// <para>主动上传音视频资源目录请求应答消息数据体<see cref="Internal.UploadFilelistReplyBody"/></para>
        /// <para>查询音视频资源目录请求消息数据体<see cref="Internal.SearchFilelistRequestBody"/></para>
        /// </remarks>
        public IJTTMessageBody SubBody { get; set; }
    }
}
