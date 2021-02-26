using SuperSocket.JTTBase.Interface;
using SuperSocket.JTTBase.Model;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT808
{
    public class JTT808PackageInfo : IJTTPackageInfo
    {
        public ReadOnlySequence<byte> Buffer { get; set; }
        public ReadOnlyMemory<byte> HeadFlag { get; set; }
        public IJTTMessageHeader MessageHeader { get; set; }
        public JTT808MessageHeader JTT808MessageHeader { get { return (JTT808MessageHeader)MessageHeader; } set { MessageHeader = value; } }
        public IJTTMessageBody MessageBody { get; set; }
        /// <summary>
        /// 额外的分包数据
        /// </summary>
        public List<byte[]> SubPackages { get; set; }
        /// <summary>
        /// 分包数据是否已完全处理
        /// </summary>
        /// <remarks>为 null时消息包未分包</remarks>
        public bool? SubPackagesComplete { get; set; }
        public ReadOnlyMemory<byte> Crc_Code { get; set; }
        public ReadOnlyMemory<byte> EndFlag { get; set; }
        public bool Success { get; set; }
        public byte Step { get; set; }
        public ApplicationException Exception { get; set; }

        public byte[] GetBytes()
        {
            return Buffer.ToArray();
        }
    }
}
