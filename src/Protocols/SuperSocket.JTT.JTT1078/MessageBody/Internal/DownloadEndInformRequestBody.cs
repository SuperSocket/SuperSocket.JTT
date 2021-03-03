using SuperSocket.JTT.Base.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT1078.MessageBody.Internal
{
    /// <summary>
    /// 远程录像下载完成通知消息数据体
    /// </summary>
    /// <remarks>
    /// <para>业务类：<see cref="Const.DataType.UP_DOWNLOAD_MSG"/></para>
    /// <para>链路类型：主链路</para>
    /// <para>消息方向：企业视频监控平台向政府视频监管平台</para>
    /// <para>子业务类型标识：<see cref="Const.SubDataType.UP_DOWNLOAD_MSG_END_INFORM"/></para>
    /// <para>描述：企业视频监控平台向政府视频监管平台发送，</para>
    /// <para>通知政府视频监管平台录像文件已从终端下载完成。</para>
    /// <para>共308字节</para>
    /// <para>JTT1078-2016表57</para>
    /// </remarks>
    public class DownloadEndInformRequestBody : IJTTMessageBody
    {
        /// <summary>
        /// 
        /// <see cref="Const.ReplyResult"/>
        /// </summary>
        /// <remarks>1字节</remarks>
        public byte Result { get; set; }

        /// <summary>
        /// 
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

        /// <summary>
        /// FTP服务器IP地址
        /// </summary>
        /// <remarks>
        /// <para>32字节</para>
        /// <para><see cref="Result"/>为 <see cref="Const.ReplyResult.成功"/>时有效</para>
        /// </remarks>
        public string ServerIP { get; set; }

        /// <summary>
        /// FTP端口
        /// </summary>
        /// <remarks>
        /// <para>2字节</para>
        /// <para><see cref="Result"/>为 <see cref="Const.ReplyResult.成功"/>时有效</para>
        /// </remarks>
        public UInt16 TCPPort { get; set; }

        /// <summary>
        /// FTP用户名
        /// </summary>
        /// <remarks>
        /// <para>49字节</para>
        /// <para><see cref="Result"/>为 <see cref="Const.ReplyResult.成功"/>时有效</para>
        /// </remarks>
        public string Username { get; set; }

        /// <summary>
        /// FTP密码
        /// </summary>
        /// <remarks>
        /// <para>22字节</para>
        /// <para><see cref="Result"/>为 <see cref="Const.ReplyResult.成功"/>时有效</para>
        /// </remarks>
        public string Password { get; set; }

        /// <summary>
        /// 文件存储路径
        /// </summary>
        /// <remarks>
        /// <para>200字节</para>
        /// <para><see cref="Result"/>为 <see cref="Const.ReplyResult.成功"/>时有效</para>
        /// </remarks>
        public string FilePath { get; set; }
    }
}
