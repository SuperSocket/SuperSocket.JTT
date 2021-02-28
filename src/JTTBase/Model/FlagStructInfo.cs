using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Model
{
    /// <summary>
    /// 位标识数据对应的结构体数据
    /// </summary>
    public class FlagStructInfo
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        /// <remarks>用来存储转换结构体数据的属性</remarks>
        public string Property { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string Assembly { get; set; }

        /// <summary>
        /// 实体
        /// </summary>
        public string TypeName { get; set; }
    }
}
