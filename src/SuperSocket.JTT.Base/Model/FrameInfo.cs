using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Model
{
    /// <summary>
    /// 帧标识信息
    /// </summary>
    public class FlagInfo
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
