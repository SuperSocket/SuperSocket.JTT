using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Model
{
    /// <summary>
    /// CRC数据校验配置
    /// </summary>
    public class CrcCcittConfig
    {
        /// <summary>
        /// 解码时是否进行CRC校验
        /// </summary>
        /// <remarks>默认 true</remarks>
        public bool Check { get; set; } = true;

        /// <summary>
        /// 类型
        /// </summary>
        /// <remarks>位数</remarks>
        public CrcType Type { get; set; }

        /// <summary>
        /// 初始值
        /// </summary>
        public InitialCrcValue InitialValue { get; set; }

        /// <summary>
        /// 需要跳过部分的长度
        /// <para>可选</para>
        /// </summary>
        public CrcIgnoreLength IgnoreLength { get; set; }

        /// <summary>
        /// 取值长度
        /// <para>可选</para>
        /// <para>负数为跳过指定长度后再取剩下的值</para>
        /// <para>如果crc校验码位于流数据末端，则应该设置为负数</para>
        /// </summary>
        public int ValueLength { get; set; }

        /// <summary>
        /// 属性名称
        /// <para>可选</para>
        /// <para>将Crc校验码写入PackageInfo</para>
        /// </summary>
        public string Property { get; set; }
    }
}
