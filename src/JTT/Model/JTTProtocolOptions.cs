using SuperSocket.JTTBase.Filter;
using SuperSocket.JTTBase.Interface;
using SuperSocket.JTTBase.Model;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Model
{
    /// <summary>
    /// JTT协议配置
    /// </summary>
    public class JTTProtocolOptions
    {
        /// <summary>
        /// JTT协议版本
        /// </summary>
        public JTTVersion Version { get; set; }

        /// <summary>
        /// <para>使用自定义JTT协议时，必须手动指定以下文件所在的命名空间</para>
        /// <para><see cref="IJTTProtocol"/></para>
        /// </summary>
        public string JTTCustomAssemblyName { get; set; }

        /// <summary>
        /// Json配置文件路径
        /// </summary>
        public string ConfigFilePath { get; set; }

        /// <summary>
        /// 生成流数据拦截器
        /// </summary>
        /// <remarks>为空时将会使用默认拦截器</remarks>
        public Func<IJTTProtocol, object, IPipelineFilter<IJTTPackageInfo>> PipelineFilterFactory { get; set; }

        /// <summary>
        /// 独立配置结构集合
        /// </summary>
        /// <remarks>使用此属性可以避免把所有配置全写在一个Json文件中</remarks>
        public List<StructureInfo> Structures { get; set; }

        /// <summary>
        /// 独立配置数据映射集合
        /// </summary>
        /// <remarks>使用此属性可以避免把所有配置全写在一个Json文件中</remarks>
        public Dictionary<string, List<MatcheInfo>> DataMappings { get; set; }

        /// <summary>
        /// 独立配置内部实体映射集合
        /// </summary>
        /// <remarks>使用此属性可以避免把所有配置全写在一个Json文件中</remarks>
        public Dictionary<string, InternalEntitysMappingInfo> InternalEntitysMappings { get; set; }
    }
}
