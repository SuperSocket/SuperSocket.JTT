using SuperSocket.JTT.Base.Interface;
using SuperSocket.JTT.Base.Model;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT809
{
    public class JTT809PackageInfo : IJTTPackageInfo
    {
        public ReadOnlySequence<byte> Buffer { get; set; }
        public ReadOnlyMemory<byte> HeadFlag { get; set; }
        public IJTTMessageHeader MessageHeader { get; set; }
        public JTT809MessageHeader JTT809MessageHeader { get { return (JTT809MessageHeader)MessageHeader; } set { MessageHeader = value; } }
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
