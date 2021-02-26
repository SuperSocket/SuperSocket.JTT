using SuperSocket.JTTBase.Model;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Interface
{
    /// <summary>
    /// 消息包
    /// </summary>
    public interface IJTTPackageInfo
    {
        /// <summary>
        /// 流数据
        /// </summary>
        /// <remarks>至包含消息头、消息体，不包含头标识、CRC校验码、尾标识</remarks>
        ReadOnlySequence<byte> Buffer { get; set; }

        /// <summary>
        /// 头标识
        /// </summary>
        ReadOnlyMemory<byte> HeadFlag { get; set; }

        /// <summary>
        /// 消息头
        /// </summary>
        IJTTMessageHeader MessageHeader { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        IJTTMessageBody MessageBody { get; set; }

        /// <summary>
        /// Crc校验码
        /// </summary>
        ReadOnlyMemory<byte> Crc_Code { get; set; }

        /// <summary>
        /// 尾标识
        /// </summary>
        ReadOnlyMemory<byte> EndFlag { get; set; }

        /// <summary>
        /// 是否成功解析
        /// </summary>
        bool Success { get; set; }

        /// <summary>
        /// 步骤
        /// </summary>
        /// <remarks><see cref="DecoderStep"/></remarks>
        byte Step { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public ApplicationException Exception { get; set; }

        /// <summary>
        /// 消息包转字符串
        /// </summary>
        /// <returns></returns>
        string ToString();

        /// <summary>
        /// 获取流数据
        /// </summary>
        /// <returns></returns>
        byte[] GetBytes();
    }
}
