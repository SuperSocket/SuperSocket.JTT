using SuperSocket.JTT.JTTBase.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody
{
    /// <summary>
    /// 从链路远程录像下载交互消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.DOWN_DOWNLOAD_MSG"/></para>
    /// <para>共28+字节</para>
    /// <para>JTT1078-2016 10.5章节</para>
    /// </remarks>
    public class DownDownloadMsgBody : IJTTMessageBody
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
        /// <para>远程录像下载请求消息数据体<see cref="Internal.DownloadStartupRequestBody"/></para>
        /// <para>远程录像下载完成通知应答消息数据体<see cref="Internal.DownloadEndInformReplyBody"/></para>
        /// <para>远程录像下载控制请求消息数据体<see cref="Internal.DownloadControlRequestBody"/></para>
        /// </remarks>
        public IJTTMessageBody SubBody { get; set; }
    }
}
