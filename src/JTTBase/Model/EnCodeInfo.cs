using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Model
{
    /// <summary>
    /// 编码信息
    /// </summary>
    public class EnCodeInfo
    {
        /// <summary>
        /// 编码类型
        /// </summary>
        public CodeType EncodeType { get; set; }

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
