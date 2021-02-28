using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Model
{
    /// <summary>
    /// 匹配项
    /// </summary>
    public class MatcheInfo
    {
        /// <summary>
        /// 匹配值
        /// </summary>
        public object Matching { get; set; }

        /// <summary>
        /// 替换值
        /// </summary>
        public object Value { get; set; }
    }
}
