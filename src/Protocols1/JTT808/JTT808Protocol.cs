using SuperSocket.JTT.JTT808.Extension;
using SuperSocket.JTT.JTTBase.Filter;
using SuperSocket.JTT.JTTBase.Hadnler;
using SuperSocket.JTT.JTTBase.Interface;
using SuperSocket.JTT.JTTBase.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTT808
{
    /// <summary>
    /// JTT809协议
    /// </summary>
    public class JTT808Protocol : IJTTProtocol
    {
        public void Initialization()
        {
            //不指定时将会使用默认方法
            //JTTPipelineFilter = new JTT808PipelineFilter(this);

            Handler = new JTT808ProtocolHandler(this);
            Decoder = new JTT808Decoder(this);
            Encoder = new JTT808Encoder(this);
        }

        public FlagInfo HeadFlag { get; set; }

        public FlagInfo EndFlag { get; set; }

        public List<StructureInfo> Structures { get; set; }

        public JTTVersion Version { get; } = JTTVersion.JTT808;

        /// <summary>
        /// 默认协议版本号
        /// </summary>
        /// <remarks>
        /// <para>1字节</para>
        /// <para>每次关键修订递增</para>
        /// <para>初始版本为1</para>
        /// </remarks>
        public byte DefaultVersion { get; set; } = 1;

        /// <summary>
        /// 分包数据的过期策略
        /// </summary>
        /// <remarks>默认 <see cref="SubPackageExpire.threshold"/></remarks>
        public SubPackageExpire SubPackageExpire { get; set; } = SubPackageExpire.threshold;

        /// <summary>
        /// 分包数据的存储空间阈值
        /// </summary>
        /// <remarks>
        /// <para>单位 MB</para>
        /// <para>默认值 100MB</para>
        /// </remarks>
        public int SubPackageThreshold { get; set; } = 100;

        /// <summary>
        /// 分包数据的存储空间阈值
        /// </summary>
        /// <remarks>
        /// <para>单位 Byte</para>
        /// </remarks>
        internal long SubPackageThresholdBytes => SubPackageThreshold * 1024 * 1024;

        /// <summary>
        /// 分包数据的超时时间
        /// </summary>
        /// <remarks>
        /// <para>默认值 1小时</para>
        /// </remarks>
        public TimeSpan SubPackageTimeout { get; set; } = TimeSpan.FromHours(1);

        public Type PackageInfoType { get; set; } = typeof(JTT808PackageInfo);

        public Type MessageHeaderType { get; set; } = typeof(JTT808MessageHeader);

        public IJTTProtocolHandler Handler { get; set; }

        public JTT808ProtocolHandler JTT808Handler => (JTT808ProtocolHandler)Handler;

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
