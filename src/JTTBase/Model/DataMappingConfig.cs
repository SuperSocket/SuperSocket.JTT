using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Model
{
    /// <summary>
    /// 数据映射配置
    /// </summary>
    public class DataMappingConfig
    {
        /// <summary>
        /// 映射标识
        /// </summary>
        /// <remarks>对应<see cref="SuperSocket.JTT.JTTBase.Interface.IJTTProtocol.DataMappings"/>的Key</remarks>
        public string Key { get; set; }

        /// <summary>
        /// 存储映射值的属性
        /// </summary>
        /// <remarks>
        /// <para>多级使用.分隔</para>
        /// </remarks>
        public string SetProperty { get; set; }

        /// <summary>
        /// 使用解码后的值进行匹配
        /// </summary>
        /// <remarks>
        /// <para>默认 true</para>
        /// <para>true 对使用<see cref="StructureInfo.Encode"/>配置解码后的值, 先使用<see cref="Encode"/>配置编码, 再使用<see cref="Decode"/>配置解码后进行匹配</para>
        /// <para>false 对原始数据（流数据）, 使用<see cref="Decode"/>配置解码后进行匹配</para>
        /// </remarks>
        public bool UseDecodeValue { get; set; } = true;

        /// <summary>
        /// 是否需要编码后再进行匹配
        /// </summary>
        public CodeInfo Encode { get; set; }

        /// <summary>
        /// 是否需要解码后再进行匹配
        /// </summary>
        public CodeInfo Decode { get; set; }
    }
}
