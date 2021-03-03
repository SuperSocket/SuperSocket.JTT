using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Model
{
    /// <summary>
    /// 定长字符的补位配置
    /// </summary>
    public class PaddingConfig
    {
        /// <summary>
        /// 应该以何种方式编码Value值
        /// </summary>
        public CodeInfo Encode { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
    }
}
