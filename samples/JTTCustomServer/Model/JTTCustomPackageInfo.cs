using SuperSocket.JTTBase.Interface;
using System;
using System.Buffers;

namespace JTTCustomServer.Model
{
    /// <summary>
    /// 自定义消息包
    /// </summary>
    public class JTTCustomPackageInfo : IJTTPackageInfo
    {
        public ReadOnlySequence<byte> Buffer { get; set; }
        public ReadOnlyMemory<byte> HeadFlag { get; set; }
        public IJTTMessageHeader MessageHeader { get; set; }
        public JTTCustomMessageHeader JTTCustomMessageHeader { get { return (JTTCustomMessageHeader)MessageHeader; } set { MessageHeader = value; } }
        public IJTTMessageBody MessageBody { get; set; }
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
