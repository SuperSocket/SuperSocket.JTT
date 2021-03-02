using SuperSocket.JTT.Base.Filter;
using SuperSocket.JTT.Base.Hadnler;
using SuperSocket.JTT.Base.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Interface
{
    /// <summary>
    /// JTT协议
    /// </summary>
    public interface IJTTProtocol
    {
        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        void Initialization();

        #endregion

        #region 配置信息

        /// <summary>
        /// 协议名称
        /// </summary>
        public JTTVersion Version { get; }

        /// <summary>
        /// 消息包类型
        /// </summary>
        public Type PackageInfoType { get; set; }

        /// <summary>
        /// 消息头类型
        /// </summary>
        public Type MessageHeaderType { get; set; }

        /// <summary>
        /// 流数据拦截器
        /// </summary>
        public JTTPipelineFilter JTTPipelineFilter { get; set; }

        /// <summary>
        /// 处理类
        /// </summary>
        public IJTTProtocolHandler Handler { get; set; }

        /// <summary>
        /// 解码器
        /// </summary>
        public IJTTDecoder Decoder { get; set; }

        /// <summary>
        /// 编码器
        /// </summary>
        public IJTTEncoder Encoder { get; set; }

        /// <summary>
        /// 数据流是否遵循大端
        /// </summary>
        public bool BigEndian { get; set; }

        /// <summary>
        /// 汉字编码
        /// </summary>
        public string ZHCNEncoding { get; set; }

        /// <summary>
        /// 定长字符的补位配置
        /// </summary>
        public PaddingConfig Padding { get; set; }

        /// <summary>
        /// CRC数据校验配置
        /// </summary>
        public CrcCcittConfig CrcCcitt { get; set; }

        /// <summary>
        /// 加密配置
        /// </summary>
        public EncryptConfig Encrypt { get; set; }

        /// <summary>
        /// 转义集合
        /// </summary>
        public List<EscapesConfig> Escapes { get; set; }

        /// <summary>
        /// 数据映射集合
        /// </summary>
        /// <remarks>key 映射标识</remarks>
        public Dictionary<string, List<MatcheInfo>> DataMappings { get; set; }

        /// <summary>
        /// 内部结构映射实体集合
        /// </summary>
        /// <remarks>key 映射标识</remarks>
        public Dictionary<string, InternalEntitysMappingInfo> InternalEntitysMappings { get; set; }

        #endregion

        #region 基础信息

        /// <summary>
        /// 头标识
        /// </summary>
        public FlagInfo HeadFlag { get; set; }

        /// <summary>
        /// 尾标识
        /// </summary>
        public FlagInfo EndFlag { get; set; }

        /// <summary>
        /// 结构集合
        /// </summary>
        public List<StructureInfo> Structures { get; set; }

        #endregion
    }
}
