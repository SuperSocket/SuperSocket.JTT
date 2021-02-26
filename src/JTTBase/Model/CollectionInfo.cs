using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Model
{
    /// <summary>
    /// 数据集合信息
    /// </summary>
    public class CollectionInfo
    {
        /// <summary>
        /// 存储数据量的属性的名称
        /// <para>多级使用.分隔</para>
        /// <para>例如 A.b.c</para>
        /// </summary>
        /// <remarks>该属性必须为数值类型</remarks>
        public string CountProperty { get; set; }
    }
}
