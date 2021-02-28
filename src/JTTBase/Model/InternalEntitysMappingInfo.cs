using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Model
{
    /// <summary>
    /// 内部结构映射实体信息
    /// </summary>
    public class InternalEntitysMappingInfo
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Assembly { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 字节长度
        /// </summary>
        /// <remarks>为空时表示可变长度</remarks>
        public int? Length { get; set; }
    }
}
