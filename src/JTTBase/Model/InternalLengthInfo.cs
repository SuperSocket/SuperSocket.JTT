using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Model
{
    /// <summary>
    /// 内部结构的长度信息
    /// </summary>
    public class InternalLengthInfo
    {
        /// <summary>
        /// 存储内部结构总长度的属性名称
        /// <para>多级使用.分隔</para>
        /// <para>例如 A.b.c</para>
        /// <para>.A.b表示当前层级的对象的A属性的b属性</para>
        /// </summary>
        public string Property { get; set; }
    }
}
