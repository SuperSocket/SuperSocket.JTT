using JTTCustomServer.Handler;
using SuperSocket.JTT.Base.Extension;
using SuperSocket.JTT.Base.Filter;
using SuperSocket.JTT.Base.Hadnler;
using SuperSocket.JTT.Base.Interface;
using SuperSocket.JTT.Base.Model;
using System;
using System.Collections.Generic;

namespace JTTCustomServer.Model
{
    /// <summary>
    /// 自定义JTT协议
    /// </summary>
    public class JTTCustomProtocol : IJTTProtocol
    {
        public void Initialization()
        {
            Decoder = new JTTCustomDecoder(this);
            Encoder = new JTTCustomEncoder(this);

            //不指定拦截器时将会使用默认拦截器
            //var handler = this.GetHandler();
            //var beginMark = handler.GetHeadFlagValue();
            //var endMark = handler.GetEndFlagValue();
            //var filter = new JTTCustomPipelineFilter(beginMark, endMark)
            //{
            //    Decoder = this.Decoder
            //};
            //JTTPipelineFilter = filter;
        }

        public FlagInfo HeadFlag { get; set; }

        public FlagInfo EndFlag { get; set; }

        public List<StructureInfo> Structures { get; set; }

        public JTTVersion Version { get; } = JTTVersion.JTTCustom;

        public Type PackageInfoType { get; set; } = typeof(JTTCustomPackageInfo);

        public Type MessageHeaderType { get; set; } = typeof(JTTCustomMessageHeader);

        public IJTTProtocolHandler Handler { get; set; }

        public JTTProtocolHandler JTTHandler => (JTTProtocolHandler)Handler;

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
