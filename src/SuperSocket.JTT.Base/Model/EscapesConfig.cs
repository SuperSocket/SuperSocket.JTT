using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT.Base.Model
{
    /// <summary>
    /// 转义配置
    /// </summary>
    public class EscapesConfig
    {
        /// <summary>
        /// 目标值
        /// </summary>
        public object Target { get; set; }

        /// <summary>
        /// 转换值
        /// </summary>
        /// <remarks>2节数</remarks>
        public object[] Trans { get; set; }

        /// <summary>
        /// 应该以何种方式编码Target值和Trans值
        /// </summary>
        public CodeInfo Encode { get; set; }
    }
}
