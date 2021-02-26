using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTTBase.Model
{
    /// <summary>
    /// 动态计算配置
    /// </summary>
    public class CompileConfig
    {
        /// <summary>
        /// 动态计算表达式
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 动态计算还原表达式
        /// </summary>
        /// <remarks>在发送数据时对其进行还原后再进行编码</remarks>
        public string RestoreExpression { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        /// <remarks>
        /// <para>多级使用.分隔</para>
        /// <para>用于存储替换值的属性</para>
        /// </remarks>
        public string Property { get; set; }
    }
}
