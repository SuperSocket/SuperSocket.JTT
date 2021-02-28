using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Model
{
    /// <summary>
    /// 内部结构的Key所属的属性信息
    /// </summary>
    /// <remarks>
    /// <para>消息包解码时 执行方法为对<see cref="Property"/>属性值先使用<see cref="Encode"/>配置编码, 再使用<see cref="Decode"/>配置解码</para>
    /// <para>消息包编码时 执行方法为对<see cref="SuperSocket.JTT.JTTBase.Interface.IJTTProtocol.InternalEntitysMappings"/>的Key先使用<see cref="Decode"/>配置编码, 再使用<see cref="Encode"/>配置解码</para>
    /// </remarks>
    public class InternalKeyInfo
    {
        /// <summary>
        /// 属性名称
        /// <para>多级使用.分隔</para>
        /// <para>例如 A.b.c</para>
        /// <para>.A.b表示当前层级的对象的A属性的b属性</para>
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 属性值是否需要编码
        /// </summary>
        public CodeInfo Encode { get; set; }

        /// <summary>
        /// 属性值是否需要解码
        /// </summary>
        public CodeInfo Decode { get; set; }
    }
}
