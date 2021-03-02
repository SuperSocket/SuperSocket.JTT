using SuperSocket.JTT.JTTBase.Filter;
using SuperSocket.JTT.JTTBase.Hadnler;
using SuperSocket.JTT.JTTBase.Interface;
using SuperSocket.JTT.JTTBase.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT809
{
    /// <summary>
    /// JTT809协议
    /// </summary>
    public class JTT809Protocol : IJTTProtocol
    {
        public void Initialization()
        {
            //不指定时将会使用默认方法
            //JTTPipelineFilter = new JTT809PipelineFilter(this);

            Handler = new JTT809ProtocolHandler(this);
            Decoder = new JTT809Decoder(this);
            Encoder = new JTT809Encoder(this);
        }

        public FlagInfo HeadFlag { get; set; }

        public FlagInfo EndFlag { get; set; }

        public List<StructureInfo> Structures { get; set; }

        public JTTVersion Version { get; } = JTTVersion.JTT809;

        /// <summary>
        /// 默认协议版本号标识
        /// </summary>
        /// <remarks>
        /// <para>3字节</para>
        /// <para>上下级平台之间采用的标准协议版本编号</para>
        /// <para>0x01 0x02 0x0F 表示的版本号是 V1.2.15，依此类推</para>
        /// </remarks>
        public byte[] DefaultVersionFlag { get; set; } = new byte[] { 1, 0, 0 };

        public Type PackageInfoType { get; set; } = typeof(JTT809PackageInfo);

        public Type MessageHeaderType { get; set; } = typeof(JTT809MessageHeader);

        public IJTTProtocolHandler Handler { get; set; }

        public JTT809ProtocolHandler JTT809Handler => (JTT809ProtocolHandler)Handler;

        public JTTPipelineFilter JTTPipelineFilter { get; set; }

        public IJTTDecoder Decoder { get; set; }

        public IJTTEncoder Encoder { get; set; }

        public bool BigEndian { get; set; }

        public string ZHCNEncoding { get; set; }

        public PaddingConfig Padding { get; set; }

        public CrcCcittConfig CrcCcitt { get; set; }

        public EncryptConfig Encrypt { get; set; }

        public List<EscapesConfig> Escapes { get; set; }

        public Dictionary<string, List<MatcheInfo>> DataMappings { get; set; }

        public Dictionary<string, InternalEntitysMappingInfo> InternalEntitysMappings { get; set; }
    }
}
